@echo off

echo ScreenSaver - build task
echo.

echo Restoring nuget packages
nuget restore ScreenSaver.sln
if errorlevel 1 goto error

echo Setting version number
Build\Packages\Veronique.1.2.0\tools\Veronique
if errorlevel 1 goto error

echo Building solution v4.5 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.5 /p:OutputPath="..\..\Build\Release\net45" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.5.1 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.5.1 /p:OutputPath="..\..\Build\Release\net451" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.5.2 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.5.2 /p:OutputPath="..\..\Build\Release\net452" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.6 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.6 /p:OutputPath="..\..\Build\Release\net46" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.6.1 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.6.1 /p:OutputPath="..\..\Build\Release\net461" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.6.2 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.6.2 /p:OutputPath="..\..\Build\Release\net462" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.7 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.7 /p:OutputPath="..\..\Build\Release\net47" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.7.1 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.7.1 /p:OutputPath="..\..\Build\Release\net471" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Building solution v4.7.2 (release)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Release /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  /p:TargetFrameworkVersion=v4.7.2 /p:OutputPath="..\..\Build\Release\net472" ^
  ScreenSaver.sln
if errorlevel 1 goto error

echo Builing package
nuget pack ScreenSaver.nuspec
if errorlevel 1 goto error

echo Cleaning up
git checkout -- ScreenSaver.nuspec
if errorlevel 1 goto error
git checkout -- ScreenSaver/ScreenSaver/Properties/AssemblyInfo.cs
if errorlevel 1 goto error
git checkout -- ScreenSaver/ScreenSaver.Test/Properties/AssemblyInfo.cs
if errorlevel 1 goto error

:success
echo.
echo Build successful
exit /b 0

:error
echo.
echo Build failed
exit /b 1