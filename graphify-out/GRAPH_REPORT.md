# Graph Report - Claude_Monitor_Usage_Statics  (2026-11-01)

## Corpus Check
- cluster-only mode — file stats not available

## Summary
- 632 nodes · 1256 edges · 29 communities (24 shown, 5 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 41 edges (avg confidence: 0.85)
- Token cost: 58,935 input · 1,135 output

## Graph Freshness
- Built from commit: `e4cf597c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Claude Web Sign-In
- Usage Color Palette
- Live Usage Limits
- Core App Services
- Usage Detail Display
- Widget Window Display
- Usage Aggregation Models
- App Startup and Lifecycle
- Settings and Startup Registration
- Window Maximize Handling
- About Dialog
- Glass Blur Effects
- Account Information Model
- Dashboard Summary Cards
- Expander Control Styling
- Main Window Buttons
- Dashboard Charts and Lists
- Login WebView Window
- Data Tables Panel
- Project Build Configuration
- Usage Data Grids
- Dashboard Grid Layout
- Account and Totals Layout
- Window Shell Layout
- Account Expander Panel
- App Logo Image
- Main Scroll Area
- Refresh Label Text
- Third Column Layout

## God Nodes (most connected - your core abstractions)
1. `Window` - 101 edges
2. `TextBlock` - 47 edges
3. `MonitorHub` - 43 edges
4. `MainWindow` - 40 edges
5. `Window` - 30 edges
6. `App` - 25 edges
7. `WidgetWindow` - 23 edges
8. `UsageSummary` - 22 edges
9. `ClaudeUsageMonitor.Services` - 22 edges
10. `ClaudeUsageMonitor.Models` - 21 edges

## Surprising Connections (you probably didn't know these)
- `CopiedText` --references--> `TextBlock`  [EXTRACTED]
  ClaudeUsageMonitor/AboutWindow.xaml → ClaudeUsageMonitor/App.xaml
- `DescriptionText` --references--> `TextBlock`  [EXTRACTED]
  ClaudeUsageMonitor/AboutWindow.xaml → ClaudeUsageMonitor/App.xaml
- `LicenseText` --references--> `TextBlock`  [EXTRACTED]
  ClaudeUsageMonitor/AboutWindow.xaml → ClaudeUsageMonitor/App.xaml
- `LinkedInLink` --references--> `TextBlock`  [EXTRACTED]
  ClaudeUsageMonitor/AboutWindow.xaml → ClaudeUsageMonitor/App.xaml
- `VersionLine` --references--> `TextBlock`  [EXTRACTED]
  ClaudeUsageMonitor/AboutWindow.xaml → ClaudeUsageMonitor/App.xaml

## Import Cycles
- None detected.

## Communities (29 total, 5 thin omitted)

### Community 0 - "Claude Web Sign-In"
Cohesion: 0.06
Nodes (20): LoginWindow, LiveUsageResult, ClaudeWebSession, UserDataFolder, DebugLog, FilePath, LiveUsageService, MonitorHub (+12 more)

### Community 1 - "Usage Color Palette"
Cohesion: 0.05
Nodes (25): Palette, Accent, CacheRead, CacheWrite, Critical, CriticalText, Haiku, Input (+17 more)

### Community 2 - "Live Usage Limits"
Cohesion: 0.10
Nodes (17): LimitRow, ExtraUsageInfo, LimitUsage, LiveUsage, LiveUsageStatus, Error, Ok, RuntimeMissing (+9 more)

### Community 3 - "Core App Services"
Cohesion: 0.09
Nodes (8): ClaudePaths, AccountFile, ClaudeHome, CredentialsFile, ClaudeUsageMonitor.Services, ClaudeUsageMonitor.Controls, ClaudeUsageMonitor, ClaudeUsageMonitor.Models

### Community 4 - "Usage Detail Display"
Cohesion: 0.06
Nodes (50): Cost, Day, Detail, Label, Model, ProjectFolder, Requests, Reset.Text (+42 more)

### Community 5 - "Widget Window Display"
Cohesion: 0.06
Nodes (26): Reset.Short, ShortLabel, ElapsedPercent, PaceText, Percent, RingBrush, UsageText, Bd (+18 more)

### Community 6 - "Usage Aggregation Models"
Cohesion: 0.07
Nodes (30): DailyRow, UsageRecord, TotalTokens, DailyUsage, ModelUsage, ProjectUsage, UsageSummary, AllTime (+22 more)

### Community 7 - "App Startup and Lifecycle"
Cohesion: 0.07
Nodes (17): App, Current, Hub, IsWidgetOpen, Settings, AppSettings, DashboardOpacity, RefreshSeconds (+9 more)

### Community 8 - "Settings and Startup Registration"
Cohesion: 0.09
Nodes (16): StartupRegistration, BlurCheck, DashboardOpacitySlider, DashboardOpacityValueText, GlassDialog, OpacitySlider, OpacityValueText, RefreshCombo (+8 more)

### Community 9 - "Window Maximize Handling"
Cohesion: 0.09
Nodes (13): MaximizeFix, MinMaxInfo, MonitorInfo, Point, Rect, RingGauge, InnerBrush, InnerValue (+5 more)

### Community 10 - "About Dialog"
Cohesion: 0.10
Nodes (17): BuildGrid, CopiedText, DescriptionText, GlassDialog, LicenseText, LinkedInLink, VersionLine, AboutWindow (+9 more)

### Community 11 - "Glass Blur Effects"
Cohesion: 0.11
Nodes (8): GlassDialog, GlassBackground, AccentPolicy, AccentState, BlurBehind, Disabled, CompositionAttributeData, GlassEffect

### Community 12 - "Account Information Model"
Cohesion: 0.12
Nodes (16): AccountInfo, AccountCreatedAt, BillingType, CredentialsExpireAt, Email, ExtraUsageEnabled, FullName, OrganizationId (+8 more)

### Community 13 - "Dashboard Summary Cards"
Cohesion: 0.15
Nodes (13): AccountCard, AllCard, DailyCard, HourCard, LiveCard, MixCard, ModelCard, MonthCard (+5 more)

### Community 14 - "Expander Control Styling"
Cohesion: 0.18
Nodes (10): Header, IsExpanded, Application, Bd, Body, Chevron, PART_Track, Border (+2 more)

### Community 15 - "Main Window Buttons"
Cohesion: 0.22
Nodes (7): Text, MaximizeButton, RefreshButton, SignInButton, StatusText, WidgetButton, Button

### Community 16 - "Dashboard Charts and Lists"
Cohesion: 0.25
Nodes (8): DailyChart, HourChart, LimitsList, ModelShareList, TokenMixLegend, ItemsControl, GaugeList, MiniChart

### Community 17 - "Login WebView Window"
Cohesion: 0.40
Nodes (4): Browser, HintText, Window, WebView2

### Community 18 - "Data Tables Panel"
Cohesion: 0.40
Nodes (5): ActionBar, DailyTable, ModelTable, ProjectTable, StackPanel

### Community 19 - "Project Build Configuration"
Cohesion: 0.50
Nodes (3): net8.0-windows10.0.17763.0, Microsoft.Web.WebView2 (1.0.4314.35), Microsoft.NET.Sdk

### Community 20 - "Usage Data Grids"
Cohesion: 0.50
Nodes (4): DailyGrid, ModelGrid, ProjectGrid, DataGrid

### Community 21 - "Dashboard Grid Layout"
Cohesion: 0.50
Nodes (4): DashboardGrid, TablesGrid, TokenMixBar, Grid

### Community 22 - "Account and Totals Layout"
Cohesion: 0.67
Nodes (3): AccountGrid, TotalsGrid, UniformGrid

### Community 23 - "Window Shell Layout"
Cohesion: 0.67
Nodes (3): Shell, TopBar, DockPanel

## Knowledge Gaps
- **146 isolated node(s):** `UserDataFolder`, `FilePath`, `Account`, `Interval`, `IsLoading` (+141 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 199 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **5 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `Usage Color Palette` to `Claude Web Sign-In`, `Core App Services`, `Usage Detail Display`, `Widget Window Display`, `App Startup and Lifecycle`, `Glass Blur Effects`, `Main Window Buttons`?**
  _High betweenness centrality (0.343) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `MainWindow` (e.g. with `App.ShowDashboard()` and `MonitorHub.RefreshAsync()`) actually correct?**
  _`MainWindow` has 2 INFERRED edges - model-reasoned connections that need verification._
- **What connects `UserDataFolder`, `FilePath`, `Account` to the rest of the system?**
  _146 weakly-connected nodes found - possible missing edges._
- **Should `Claude Web Sign-In` be split into smaller, more focused modules?**
  _Cohesion score 0.058384547848990345 - nodes in this community are weakly interconnected._
- **Why does `Window` connect `Usage Detail Display` to `Usage Color Palette`, `Widget Window Display`, `Dashboard Summary Cards`, `Main Window Buttons`, `Dashboard Charts and Lists`, `Data Tables Panel`, `Usage Data Grids`, `Dashboard Grid Layout`, `Account and Totals Layout`, `Window Shell Layout`, `Account Expander Panel`, `App Logo Image`, `Main Scroll Area`, `Refresh Label Text`, `Third Column Layout`?**
  _High betweenness centrality (0.299) - this node is a cross-community bridge._
- **Should `Usage Color Palette` be split into smaller, more focused modules?**
  _Cohesion score 0.05423728813559322 - nodes in this community are weakly interconnected._
- **Why does `MonitorHub` connect `Claude Web Sign-In` to `Usage Color Palette`, `Live Usage Limits`, `Core App Services`, `Widget Window Display`, `Usage Aggregation Models`, `App Startup and Lifecycle`, `Glass Blur Effects`, `Account Information Model`?**
  _High betweenness centrality (0.249) - this node is a cross-community bridge._