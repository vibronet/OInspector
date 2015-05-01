@echo off

@echo.
@echo *** Envy is getting ready your environment ***
@echo.

@echo *** Fixing up your PATH variable ***
@echo.
@REM %cd% is a dynamic variable that points to the current directory
@set "REPOROOT=%cd%"
@set "NUGETROOT=%REPOROOT%\packages"
@set "EXTERNAL=%REPOROOT%\.external"
@set "PATH=%REPOROOT%\.envy;%NUGETROOT%;%REPOROOT%\tools;%PATH%"

@REM TODO: Redirect build output into the specified location
@set "BUILD_OUTPUT=%REPOROOT%\out"

@rem Restore all NuGet packages first
@call restore

@echo *** Loading envy's aliases ***
@call doskey /macrofile=%REPOROOT%\.envy\aliases
@call type %REPOROOT%\.envy\aliases
@echo.
@echo.

@echo Enjoy! :-)