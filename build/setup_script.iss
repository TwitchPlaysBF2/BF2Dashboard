; Inno Setup Script

#define MyAppName "BF2.TV"
#define MyAppVersion "0.0.2.0"
#define MyAppPublisher "TwitchPlaysBF2"
#define MyAppURL "https://www.github.com/TwitchPlaysBF2"
#define MyAppExeName "BF2TV.WindowsApp.exe"

[Setup]       
AppId={{160D7F45-1026-4216-8CCD-217D7A40C1B2}
AppName={#MyAppName}
VersionInfoVersion={#MyAppVersion}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} v{#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\BF2.TV
DisableProgramGroupPage=yes
OutputDir=/bin
OutputBaseFilename=BF2.TV_App_Setup
SetupIconFile=..\src\BF2TV.WindowsApp\Resources\favicon.ico
UninstallDisplayIcon={app}\Resources\favicon.ico
Compression=lzma2/max
SolidCompression=yes
WizardStyle=classic   
; Remove the SignTool line if you have no configured certificate for signing the binaries.
; If you have access to the BF2.TV PFX file then go to [Tools -> Configure Sign Tools] and configure this:
; Name: BF2TV_Cert
; Command: "C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\signtool.exe" sign /v /f "D:\Projects\BF2.TV\cert\BF2TV_Cert.pfx" /t http://timestamp.sectigo.com $f
SignTool=BF2TV_Cert     

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "bin\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "redistributables\*"; DestDir: "{app}\_external"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\Resources\favicon.ico"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\Resources\favicon.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\_external\joinme.click-launcher.exe"; Parameters: "-quiet"
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

