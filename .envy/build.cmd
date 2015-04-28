@echo off
@call setlocal

@if exist "%ProgramFiles(x86)%\MSBuild\12.0" set "MSBUILD=%ProgramFiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe"

@if "%MSBUILD%" == "" (
    @echo "ERROR: Cannot find MSBuild on your machine in %ProgramFiles(x86)%\MSBuild\12.0\Bin\."
    @goto end
)

@set MSBUILD_FL=logfile=build.log

@rem Enable diagnostic verbosity for the current build run
@if "%~1" == "--diagnostic" (
    @set MSBUILD_FL=%MSBUILD_FL%;verbosity=diagnostic
)

@rem Restore all NuGet packages (to ensure the build has all the dependencies)
@call restore

@rem Build the sources
@call "%MSBUILD%" /fl /fl1 /fl2 /flp:%MSBUILD_FL% /flp1:logfile=errors.log;errorsonly /flp2:logfile=warnings.log;warningsonly

@rem Give some useful hints to the developer
@if "%~1" == "--diagnostic" (
    @echo.
    @echo envy [build] - Executed with diagnostic verbosity for troubleshooting purposes, see build.log for more info...
)

:end
@call endlocal