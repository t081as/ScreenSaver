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

echo Merging il code
.\Build\Packages\ILMerge.2.14.1208\tools\ILMerge.exe .\Build\Debug\ScreenSaver.Test.exe .\Build\Debug\ScreenSaver.dll /out:.\Build\Debug\ScreenSaver.Merged.exe

echo Renaming executable
move .\Build\Debug\ScreenSaver.Merged.exe .\Build\Debug\ScreenSaver.scr

:success
echo.
echo Build successful
exit /b 0

:error
echo.
echo Build failed
exit /b 1