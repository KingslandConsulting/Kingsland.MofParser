@echo Off

set powershell=%windir%\System32\WindowsPowerShell\v1.0\powershell.exe
set dir=%~dp0%

echo powershell = %powershell%
echo dir        = %dir%

rem the following resets ERRORLEVEL to 0 prior to running powershell
verify > nul

echo ERRORLEVEL before = %ERRORLEVEL%

rem note - "%*" passes through any command line parameters in the call to build.bat
"%powershell%" -NoProfile -NonInteractive -ExecutionPolicy RemoteSigned -Command "& '%dir%Build.ps1' %*"

echo ERRORLEVEL after = %ERRORLEVEL%

if %ERRORLEVEL% == 0 goto end
echo ##teamcity[buildStatus status='FAILURE' text='{build.status.text} in execution']
exit /b %ERRORLEVEL%

:end