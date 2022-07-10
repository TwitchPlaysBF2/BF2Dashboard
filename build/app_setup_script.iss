; Inno Setup Script

#define MyAppName "BF2.TV"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "TwitchPlaysBF2"
#define MyAppURL "https://www.github.com/TwitchPlaysBF2"
#define MyAppExeName "BF2Dashboard.WindowsApp.exe"

[Setup]
AppId={{160D7F45-1026-4216-8CCD-217D7A40C1B2}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\BF2.TV
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=/bin
OutputBaseFilename=BF2.TV_App_Setup
SetupIconFile=..\src\BF2Dashboard.UI\wwwroot\favicon.ico
UninstallDisplayIcon={app}\{#MyAppName}.exe
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Icons]
Name: "{group}\My Program"; Filename: "{app}\MYPROG.EXE"; WorkingDir: "{app}"
Name: "{group}\Uninstall My Program"; Filename: "{uninstallexe}"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\src\BF2Dashboard.WindowsApp\bin\Release\net6.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

