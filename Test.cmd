@echo off

echo ScreenSaver - test task
echo.

echo Restoring nuget packages
nuget restore ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution (debug)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Debug /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  ScreenSaver.sln
if errorlevel 1 goto error

:success
echo.
echo Build successful
exit /b 0

:error
echo.
echo Build failed
exit /b 1