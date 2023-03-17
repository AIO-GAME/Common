@echo off&@setlocal enabledelayedexpansion
@chcp 65001&@color F&@cls&@cd /d %~dp0

@set sourcePath=%cd%\bin\Release\netcoreapp3.1
@set targetPath=%cd%\Dlls

@echo source : %sourcePath%
@echo target : %targetPath%

if exist %targetPath% (
	@del %targetPath% /S /Q
	@rmdir %targetPath% /S /Q
)
@mkdir %targetPath%

for /f %%i in (./Config.ini) do (
	@call:COPYDLLS %%i 
)

@goto :eof

:COPYDLLS
@set name=%1
@copy !sourcePath!\%name%.xml /v /y /l !targetPath!\%name%.xml
@copy "!sourcePath!\%name%.dll" /v /y /l "!targetPath!\%name%.dll"
@goto :eof
