using System.Windows;
using ClaudeUsageMonitor.Models;
using ClaudeUsageMonitor.Services;
using Forms = System.Windows.Forms;

namespace ClaudeUsageMonitor;

/// <summary>
/// The icon in the Windows notification area, next to the clock. Its menu opens the dashboard, the widget,
/// settings and About, refreshes, and exits. The tooltip shows the live limits.
/// Must be created and used on the UI thread.
/// </summary>
public sealed class TrayIcon : IDisposable
{
    private const string AppName = AppInfo.Name;

    // NotifyIcon.Text is limited to 127 characters.
    private const int MaxTooltipLength = 127;

    private readonly MonitorHub _hub;
    private readonly Forms.NotifyIcon _icon;
    private readonly System.Drawing.Icon _image;
    private readonly Forms.ToolStripMenuItem _widgetItem;

    // The drawn percent icon, and what it shows, so it is only redrawn when the numbers change.
    private System.Drawing.Icon? _percentImage;
    private int _drawnPercent = -1;
    private System.Drawing.Color _drawnColor;

    /// <summary>Creates the icon and shows it.</summary>
    /// <param name="hub">The shared data source, used for Refresh and the tooltip.</param>
    public TrayIcon(MonitorHub hub)
    {
        _hub = hub;

        using (var stream = Application.GetResourceStream(new Uri("pack://application:,,,/Assets/app.ico")).Stream)
        {
            _image = new System.Drawing.Icon(stream, Forms.SystemInformation.SmallIconSize);
        }

        var dashboardItem = new Forms.ToolStripMenuItem("Open dashboard", null, (_, _) => Safe(App.Current.ShowDashboard));
        dashboardItem.Font = new System.Drawing.Font(dashboardItem.Font, System.Drawing.FontStyle.Bold);
        var showWidgetItem = new Forms.ToolStripMenuItem("Show widget", null, (_, _) => Safe(() => App.Current.ShowWidget(compact: false)));
        var showSmallItem = new Forms.ToolStripMenuItem("Show small widget", null, (_, _) => Safe(() => App.Current.ShowWidget(compact: true)));
        _widgetItem = new Forms.ToolStripMenuItem("Hide widget", null, (_, _) => Safe(App.Current.ToggleWidget));

        var menu = new Forms.ContextMenuStrip();
        menu.Items.AddRange(new Forms.ToolStripItem[]
        {
            dashboardItem,
            showWidgetItem,
            showSmallItem,
            _widgetItem,
            new Forms.ToolStripMenuItem("Refresh now", null, async (_, _) => await _hub.RefreshAsync()),
            new Forms.ToolStripSeparator(),
            new Forms.ToolStripMenuItem("Settings...", null, (_, _) => Safe(ShowDialog<SettingsWindow>)),
            new Forms.ToolStripMenuItem("About...", null, (_, _) => Safe(ShowDialog<AboutWindow>)),
            new Forms.ToolStripSeparator(),
            new Forms.ToolStripMenuItem("Exit", null, (_, _) => Safe(App.Current.ExitApp))
        });
        menu.Opening += (_, _) => _widgetItem.Visible = App.Current.IsWidgetOpen;

        _icon = new Forms.NotifyIcon
        {
            Icon = _image,
            Text = AppName,
            ContextMenuStrip = menu,
            Visible = true
        };
        _icon.MouseClick += (_, e) =>
        {
            if (e.Button == Forms.MouseButtons.Left)
            {
                Safe(App.Current.ShowDashboard);
            }
        };

        _hub.Changed += OnHubChanged;
        OnHubChanged(this, EventArgs.Empty);
    }

    /// <summary>
    /// Runs a menu action. These run in the Windows Forms message path, where an exception would show the
    /// Windows Forms error dialog or end the app, so failures are logged (type only) and ignored.
    /// </summary>
    private static void Safe(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            DebugLog.Write($"Tray menu action failed: {ex.GetType().Name}");
        }
    }

    private void OnHubChanged(object? sender, EventArgs e)
    {
        UpdateTooltip();
        UpdateImage();
    }

    /// <summary>
    /// Draws the most-used plan limit as a ring with its percent inside, in the widget's gauge color.
    /// Keeps the app icon until the first live numbers arrive.
    /// </summary>
    private void UpdateImage()
    {
        if (_hub.LastGoodLive is not { Limits.Count: > 0 } live)
        {
            return;
        }

        var row = live.Limits.Select(l => UsageFormatting.ToLimitRow(l, DateTimeOffset.Now)).MaxBy(r => r.Percent)!;
        var percent = (int)Math.Round(Math.Clamp(row.Percent, 0, 100));
        var wpf = ((System.Windows.Media.SolidColorBrush)row.RingBrush).Color;
        var color = System.Drawing.Color.FromArgb(wpf.R, wpf.G, wpf.B);
        if (percent == _drawnPercent && color == _drawnColor)
        {
            return;
        }

        var size = Forms.SystemInformation.SmallIconSize.Width;
        using var bitmap = new System.Drawing.Bitmap(size, size);
        using (var g = System.Drawing.Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            var thickness = Math.Max(2f, size / 8f);
            var rect = new System.Drawing.RectangleF(thickness / 2, thickness / 2, size - thickness, size - thickness);
            using var back = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0x1C, 0x1B, 0x19));
            using var track = new System.Drawing.Pen(System.Drawing.Color.FromArgb(0x55, 0x55, 0x55), thickness);
            using var arc = new System.Drawing.Pen(color, thickness);
            g.FillEllipse(back, rect);
            g.DrawEllipse(track, rect);
            g.DrawArc(arc, rect, -90, 360f * percent / 100);

            // "100" does not fit in a 16 px icon; a full red ring already says it.
            var text = percent >= 100 ? "!" : percent.ToString();
            using var font = new System.Drawing.Font("Segoe UI", size * (text.Length > 1 ? 0.36f : 0.45f), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            using var format = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center };
            g.DrawString(text, font, System.Drawing.Brushes.White, new System.Drawing.RectangleF(0, 0, size, size + 1), format);
        }

        // GetHicon makes an unmanaged handle; clone it into a managed icon and free the handle.
        var handle = bitmap.GetHicon();
        System.Drawing.Icon icon;
        using (var temp = System.Drawing.Icon.FromHandle(handle))
        {
            icon = (System.Drawing.Icon)temp.Clone();
        }

        DestroyIcon(handle);
        _icon.Icon = icon;
        _percentImage?.Dispose();
        _percentImage = icon;
        _drawnPercent = percent;
        _drawnColor = color;
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool DestroyIcon(IntPtr handle);

    /// <summary>Shows the app name and each live limit, such as "Current session (5-hour): 66%".</summary>
    private void UpdateTooltip()
    {
        var lines = new List<string> { AppName };
        if (_hub.LastGoodLive is { } live)
        {
            lines.AddRange(UsageFormatting.GaugeLimits(live).Select(l => $"{l.Label}: {l.Percent:0}%"));
        }
        else if (_hub.LiveResult?.Status == LiveUsageStatus.SignedOut)
        {
            lines.Add("Not signed in to claude.ai");
        }

        var text = string.Join("\n", lines);
        _icon.Text = text.Length <= MaxTooltipLength ? text : text[..(MaxTooltipLength - 3)] + "...";
    }

    /// <summary>
    /// Opens a dialog over the dashboard, opening the dashboard first if needed.
    /// If the dialog is already open, brings it to the front instead of opening a second one.
    /// </summary>
    private static void ShowDialog<T>() where T : Window, new()
    {
        if (Application.Current.Windows.OfType<T>().FirstOrDefault() is { } open)
        {
            open.Activate();
            return;
        }

        App.Current.ShowDashboard();
        new T { Owner = Application.Current.MainWindow }.ShowDialog();
    }

    /// <summary>Removes the icon from the notification area.</summary>
    public void Dispose()
    {
        _hub.Changed -= OnHubChanged;
        _icon.Visible = false;
        _icon.ContextMenuStrip?.Dispose();
        _icon.Dispose();
        _image.Dispose();
        _percentImage?.Dispose();
    }
}
