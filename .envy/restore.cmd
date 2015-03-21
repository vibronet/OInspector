@echo off

@rem Restore all NuGet packages first
@echo *** Restoring packages from NuGet ***
@call nuget restore -PackagesDirectory "%NUGETROOT%"
@echo.