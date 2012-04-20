@echo off
cd /D %~dp0
set msbuild=%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
if [%1]==[] (set config=Debug) else (set config=%1)
if [%2]==[] (set platform=Any CPU) else (set platform=%2)
%msbuild% ..\Automation\Automation.sln /t:Rebuild /p:Configuration=%config%;Platform="%platform%"
exit /b %%errorlevel%%
