# Limitlight for Claude

A Windows desktop app (WPF) that shows your Claude Code usage and, after you sign in to claude.ai, your live plan limits. It has a graphical dashboard and a small always-on-top glass widget. Formerly called Claude Usage Monitor.

## Download

Get the installer (version 0.5.0) from the [Releases page](https://github.com/agoldust/Limitlight-for-Claude/releases/latest) or the product page: [goldoust.de/apps/claude_usage_monitor](https://www.goldoust.de/apps/claude_usage_monitor/) ([Deutsch](https://www.goldoust.de/apps/claude_usage_monitor/de/)). Both list the installer's SHA-256 checksum. The installer is per user and needs no administrator rights, and it upgrades an existing Claude Usage Monitor install in place. It is not code-signed yet, so Windows SmartScreen may warn; choose More info, then Run anyway.

To build the installer yourself: publish with `dotnet publish Limitlight\Limitlight.csproj -c Release -r win-x64 --self-contained true -o publish\win-x64`, then compile [installer/Limitlight.iss](installer/Limitlight.iss) with Inno Setup 6. The output is `publish\installer\LimitlightForClaude-Setup-<version>.exe`.

## Requirements

- Windows 10 version 1809 or later
- .NET 11 SDK (to build)
- Microsoft Edge WebView2 Runtime (for sign-in and live limits; present on current Windows 10/11)
- Claude Code installed and used at least once, so its local files exist

## Build and run

```
dotnet build Limitlight\Limitlight.csproj -c Release
dotnet run --project Limitlight\Limitlight.csproj -c Release
```

Start only the widget with `--widget`.

## Features

- **Live plan limits.** Sign in to claude.ai from the app; limits are fetched from claude.ai's internal web endpoints (not a public API, so they may change without notice). Shown as ring gauges with a pace marker. Each limit appears only when claude.ai reports it, so the Opus and Sonnet weekly limits show up only on plans that have them. While extra usage is on, an "Extra usage (monthly)" gauge shows how much of the monthly spend limit is used, and the "Limit reached" line says when extra usage carries on. If the claude.ai sign-in is a different account from the one Claude Code uses, the dashboard says so.
- **Reset countdown.** Each live limit counts down to its reset every second. The dashboard shows "Next reset: <limit> in …", or a red "Limit reached · tokens available again in …" line while a limit is at 100%. The widget shows the red line only while a limit is used up. The app refreshes 5 seconds after a limit resets.
- **Tray icon.** Once live limits load, the notification-area icon becomes a ring filled to the most-used limit, with its percent inside, in the same colors as the gauges. Left-click opens the dashboard; right-click for Open dashboard, Show widget, Show small widget, Hide widget, Refresh now, Settings, About and Exit. The tooltip shows every live limit percentage.
- **Responsive dashboard.** Four layouts by window width: Compact for 360 to 859 px (phone-like: one column, shorter charts; Refresh shows only its icon below 440 px), Normal for 860 to 1539 px (one column), Wide for 1540 to 2299 px (two columns, taller charts, tables side by side), and Extra-wide for 2300 px and up (three columns). Content is capped at 3600 px. Minimum window size is 360x400. The top bar wraps to two rows below 1000 px. Widget, Settings and About are icon-only buttons; "Last refresh" is shown next to Refresh.
- **Graphical dashboard.** Ring gauges, a 14-day bar chart, model share, token mix, hour-of-day chart, and collapsible account details and tables. Press F5 to refresh.
- **Widget.** Always-on-top, translucent window with optional blur. Opacity defaults to 30%. A small widget (right-click, Compact) shows only the gauges, stacked in a narrow pill-shaped oval without buttons; switching sizes keeps its right edge in place. The widget refreshes when it opens and after the PC wakes from sleep.
- **Dark glass look.** The dashboard, widget, Settings and About share one dark glass theme with blur. The dashboard opacity defaults to 80%; Settings and About never go below 70% so their text stays readable.
- **Settings.** Refresh interval (30 seconds to 1 hour), widget and dashboard opacity, blur, always on top, start with Windows.
- **About.** Author, MIT license, version and build details (with a copy button for bug reports), a data and privacy summary, a disclaimer and credits.
- **Start with Windows.** Registers the exe in the current user's Run key. The path is the exe's location at the time you turn it on; turn it off and on again if you move the app. Optionally opens only the widget.
- **Single instance.** Starting the app again shows the dashboard. Closing all windows keeps the app running in the notification area; use Exit in the tray menu to quit.
- **Windows app identity.** The app registers as `de.goldoust.limitlight` (AppUserModelID), so Windows groups its taskbar button and notifications under its own icon. The installer's shortcuts use the same ID, so a pinned icon matches the running app.
- **Reliability.** Session logs are read while Claude Code writes them. claude.ai requests time out (20 s fetch, 30 s browser call and start) and the hidden browser restarts; errors are shown in plain language with the retry time.
- **Local usage.** Tokens, requests and estimated cost for today, 7 days, 30 days and all time, from Claude Code logs.
- **Debug log.** `%LOCALAPPDATA%\ClaudeUsageMonitor\logs\app.log`.

More: [USAGE_DATA.md](docs/USAGE_DATA.md), [ARCHITECTURE.md](docs/ARCHITECTURE.md), [DIAGRAMS.md](docs/DIAGRAMS.md), [ROADMAP.md](docs/ROADMAP.md).

## License

MIT. See [LICENSE](LICENSE).
