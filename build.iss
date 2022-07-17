#define MyAppName "AGG Productions"
#define MyAppVersion "1.4.0"
#define MyAppPublisher "AGG Productions"
#define MyAppExeName "AGG Productions.exe"

[Setup]
AppId={{AGG-Productions}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName=C:\Users\Public\{#MyAppName}
DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
OutputDir=./
OutputBaseFilename=AGG.Productions.Installer
SetupIconFile=.\Images\Icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce

[Files]
Source: ".\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\AGG Productions.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\AGG Productions.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\AGG Productions.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
