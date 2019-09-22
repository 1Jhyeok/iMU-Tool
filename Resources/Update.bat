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
echo 업데이트가 완료되었습니다. 이 창은 닫으셔도 됩니다.
"iMU Tool.exe"
exit