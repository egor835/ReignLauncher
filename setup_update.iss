#define MyAppName "ReignCraft Launcher"
#define MyAppVersion "1.1.1"
#define MyAppPublisher "ReignCraft"
#define MyAppExeName "RCRL.exe"

[Setup]
AppId={{B5BDF0B0-319E-43C9-8ECD-C276887C36C6}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\ReignCraft
DisableDirPage=yes
UninstallDisplayIcon={app}\{#MyAppExeName}
DisableProgramGroupPage=yes
OutputBaseFilename=rcl
SetupIconFile=C:\Users\Cunny\source\repos\ReignLauncher\minecraft.ico
SolidCompression=yes
Compression = zip
WizardStyle=modern

[Languages]
Name: "reign"; MessagesFile: "compiler:Languages\Reign.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Cunny\source\repos\ReignLauncher\bin\x86\Release\net6.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent