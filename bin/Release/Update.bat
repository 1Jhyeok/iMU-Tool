@echo off
taskkill /F /IM "iMU Tool.exe"
:loop
tasklist /FI "IMAGENAME eq iMU Tool.exe" 2>NUL | find /I /N "iMU Tool.exe">NUL
if "%ERRORLEVEL%"=="0" (
	echo running...
	goto :loop
)

del "iMU Tool.exe"
ren "Update.exe" "iMU Tool.exe"
echo ������Ʈ�� �Ϸ�Ǿ����ϴ�. �� â�� �����ŵ� �˴ϴ�.
"iMU Tool.exe"
exit