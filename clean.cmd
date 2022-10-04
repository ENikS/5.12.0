@ECHO OFF

ECHO Deleting bin(s)
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
rem for /f %%F in ('dir /b /ad /s ^| findstr /iles "Bin"') do RMDIR /s /q "%%F"

ECHO Deleting obj(s)
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"
rem for /f %%F in ('dir /b /ad /s ^| findstr /iles "Obj"') do RMDIR /s /q "%%F"

ECHO Deleting TestResult(s)
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S TestResults') DO RMDIR /S /Q "%%G"
rem for /f %%F in ('dir /b /ad /s ^| findstr /iles "TestResults"') do RMDIR /s /q "%%F"

PAUSE