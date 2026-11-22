; Inno Setup script for Limitlight for Claude (per-user install, no admin rights).
; Build: dotnet publish Limitlight\Limitlight.csproj -c Release -r win-x64 --self-contained true -o publish\win-x64
;        ISCC.exe installer\Limitlight.iss   (version is read from the published exe)
; Output: publish\installer\LimitlightForClaude-Setup-<version>.exe

#define AppName "Limitlight for Claude"
#define AppExe "ClaudeUsageMonitor.exe"
#define PublishDir "..\publish\win-x64"
#define AppVersion GetVersionNumbersString(PublishDir + "\" + AppExe)
; Version is "0.5.0.0"; drop the last part for display and the file name.
#define ShortVersion Copy(AppVersion, 1, RPos(".", AppVersion) - 1)

[Setup]
; Same AppId as the Claude Usage Monitor installers, so this upgrades an existing install in place.
AppId={{7C1F4E2A-5B8D-4E36-9A1C-3D2B6F8E0A47}
AppName={#AppName}
AppVersion={#ShortVersion}
AppVerName={#AppName} {#ShortVersion}
AppPublisher=Akbar Goldoust
AppPublisherURL=https://www.goldoust.de/apps/claude_usage_monitor/
AppSupportURL=https://github.com/agoldust/Limitlight-for-Claude
AppCopyright=Copyright (c) 2026 Akbar Goldoust
; New installs go to %LOCALAPPDATA%\Programs\Limitlight for Claude; upgrades keep their old folder.
PrivilegesRequired=lowest
DefaultDirName={autopf}\{#AppName}
DisableProgramGroupPage=yes
UninstallDisplayName={#AppName}
UninstallDisplayIcon={app}\{#AppExe}
SetupIconFile=..\Limitlight\Assets\app.ico
LicenseFile=..\LICENSE
OutputDir=..\publish\installer
OutputBaseFilename=LimitlightForClaude-Setup-{#ShortVersion}
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0.17763
; Asks to close the running app before files are replaced.
AppMutex=ClaudeUsageMonitor.SingleInstance
CloseApplications=yes

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#PublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[InstallDelete]
; Shortcuts with the old name from 0.4.x installers.
Type: files; Name: "{autoprograms}\Claude Usage Monitor.lnk"
Type: files; Name: "{autodesktop}\Claude Usage Monitor.lnk"

[Icons]
; The AppUserModelID matches the running app, so a pinned taskbar icon groups with its windows.
Name: "{autoprograms}\{#AppName}"; Filename: "{app}\{#AppExe}"; AppUserModelID: "de.goldoust.limitlight"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExe}"; AppUserModelID: "de.goldoust.limitlight"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent
