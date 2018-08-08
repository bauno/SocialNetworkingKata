@echo off
cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe restore
.paket\paket.exe update
if errorlevel 1 (
  exit /b %errorlevel%
)

FAKE run build.fsx %*
