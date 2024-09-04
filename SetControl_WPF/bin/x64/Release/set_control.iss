; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "set_control"
#define MyAppVersion "1.5"
#define MyAppPublisher "My Company, Inc."
#define MyAppURL "https://www.example.com/"
#define MyAppExeName "SetControl_WPF.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{F4A5118E-31A1-4ABD-940D-D5B802FF4E08}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
; "ArchitecturesAllowed=x64compatible" specifies that Setup cannot run
; on anything but x64 and Windows 11 on Arm.
ArchitecturesAllowed=x64compatible
; "ArchitecturesInstallIn64BitMode=x64compatible" requests that the
; install be done in "64-bit mode" on x64 or Windows 11 on Arm,
; meaning it should use the native 64-bit Program Files directory and
; the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64compatible
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\juan_\source\repos\Instalador
OutputBaseFilename=set_control
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\EasyModbus.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\EasyModbus.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\EntityFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\EntityFramework.SqlServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\EntityFramework.SqlServer.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\EntityFramework.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\Gma.System.MouseKeyHook.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\loginHistory.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\QRCoder.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\SetControl_WPF.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\SetControl_WPF.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Data.SQLite.EF6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Data.SQLite.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Data.SQLite.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Drawing.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Drawing.Common.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\System.Drawing.Common.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\Xceed.Wpf.AvalonDock.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\Xceed.Wpf.AvalonDock.Themes.Aero.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\Xceed.Wpf.AvalonDock.Themes.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\Xceed.Wpf.AvalonDock.Themes.VS2010.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\juan_\source\repos\SetControl_WPF\SetControl_WPF\bin\x64\Release\Xceed.Wpf.Toolkit.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
