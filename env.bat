@echo OFF
title gacutil regasm prompt

goto check_Permissions

:check_Permissions
    net session >nul 2>&1
    if %errorLevel% == 0 (
        REM echo Success: Administrative permissions confirmed.
        goto go
    ) else (
        echo Please run as Administrator.
    )

    pause >nul

:go

doskey gacutil="C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\x64\gacutil.exe" $*
doskey RegAsm="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" $*
doskey k=C:\Windows\System32\taskkill.exe /im explorer.exe /f
cd C:\Users\Jeremy\Documents\ProjectDeskband\dotnetBandObjects_src
cd .\SampleBars\bin\Release

echo Run gacutil to install to GAC,
echo Run regasm to register COM
echo Run k to kill Explorer.exe