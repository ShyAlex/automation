@echo off
cd /D %~dp0
set msbuild=%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
if [%1]==[] (set version=v4.0) else (set version=%1)
if [%2]==[] (set config=Debug) else (set config=%2)
if [%3]==[] (set platform=Any CPU) else (set platform=%3)
%msbuild% ..\Automation\Automation.sln /t:Rebuild /p:Configuration=%config%;Platform="%platform%";TargetFrameworkVersion=%version%
exit /b %%errorlevel%%
