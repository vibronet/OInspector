@echo off
@call setlocal

@rem Execute build with normal verbosity
@if "%~1" == "build" (
	@call build $*
)

@rem Execute build with diagnostic verbosity
@if "%~1" == "build-diag" (
	@call build --diagnostic
)

@rem Execute tests in the console
@if "%~1" == "test" (
	@call runtests
)

@call endlocal