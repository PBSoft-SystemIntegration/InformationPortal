@echo off

REM Start første instans af InformationPortalServer med parameter 8081
start "InformationPortalServer1" cmd /k "cd /d InformationPortalServer && dotnet run -- 8081 server1"

REM Start anden instans af InformationPortalServer med parameter 8082
start "InformationPortalServer2" cmd /k "cd /d InformationPortalServer && dotnet run -- 8082 server2"

REM Start InformationPortal
start "InformationPortal" cmd /k "cd /d InformationPortal && dotnet run"

REM Start InformationClient
start "InformationClient" cmd /k "cd /d InformationPortalClient && dotnet run"
