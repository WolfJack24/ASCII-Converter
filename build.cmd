@echo off
cls

dotnet build
if errorlevel 1 (
    exit /b %errorlevel%
)

dotnet run
if errorlevel 1 (
    exit /b %errorlevel%
)
