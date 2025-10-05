@echo off
setlocal enabledelayedexpansion

cd /d "%~dp0"
for /f %%a in ('echo prompt $E ^| cmd') do set "ESC=%%a"

set "RED=%ESC%[31m"
set "GREEN=%ESC%[32m"
set "CYAN=%ESC%[96m"
set "BLUE=%ESC%[94m"
set "ORANGE=%ESC%[33m"
set "RESET=%ESC%[0m"

set "ERROR=[%RED%ERROR%RESET%]"
set "INFO=[%BLUE%INFO%RESET%]"
set "OK=[%GREEN%OK%RESET%]"
set "WARNING=[%ORANGE%WARNING%RESET%]"

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 1]%RESET% Checking if dotnet exists
echo %BLUE%=========================================%RESET%
where dotnet >nul 2>nul
if errorlevel 1 (
    echo %ERROR% 'dotnet' not found.
    @echo off
    echo Do you want to install .NET SDK using winget? [Y/N]?

    choice /c YN /n /m ""
    if errorlevel 2 goto dotnet_no
    if errorlevel 1 goto dotnet_yes

    :dotnet_yes
    echo == %CYAN%[STEP 1.1]%RESET% Checking if winget exists ==
    where winget >nul 2>nul
    if errorlevel 1 (
        echo %ERROR% 'winget' not found. Please install "App Installer" from Microsoft Store.
        echo Press any key to exit ...
        pause >nul
        exit /b 0
    ) else (
        for /f %%i in ('winget --version') do echo %OK% winget found %ORANGE%%%i%RESET%
            echo %INFO% Installing .NET SDK...
            winget install Microsoft.DotNet.SDK.8 -e --accept-package-agreements --accept-source-agreements
            echo %INFO% .NET SDK installation finished
        goto dotnet_end
    )

    :dotnet_no
    echo %ERROR% Cannot run app without dotnet installed
    echo Press any key to close this window...
    pause >nul
    exit /b 0

    :dotnet_end 
    echo %OK% Done.
) else (
    for /f %%i in ('dotnet --version') do echo %OK% dotnet found %ORANGE%v%%i%RESET%
)

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 2]%RESET% Checking if npm exists
echo %BLUE%=========================================%RESET%
where npm >nul 2>nul
if errorlevel 1 (
    echo %ERROR% 'npm' not found.
    @echo off
    echo Do you want to install Node.js LTS using winget? [Y/N]?

    choice /c YN /n /m ""
    if errorlevel 2 goto npm_no
    if errorlevel 1 goto npm_yes

    :npm_yes
    echo == %CYAN%[STEP 2.1]%RESET% Checking if winget exists ==
    where winget >nul 2>nul
    if errorlevel 1 (
        echo %ERROR% 'winget' not found. Please install "App Installer" from Microsoft Store.
        echo Press any key to exit ...
        pause >nul
        exit /b 0
    ) else (
        for /f %%i in ('winget --version') do echo %OK% winget found %ORANGE%%%i%RESET%
        echo %INFO% Installing Node.js...
        winget install OpenJS.NodeJS.LTS -e --accept-package-agreements --accept-source-agreements
        goto npm_end
    )

    :npm_no
    echo %ERROR% Cannot run app without npm installed
    echo Press any key to close this window...
    pause >nul
    exit /b 0

    :npm_end 
    echo %OK% Done.
) else (
    for /f %%i in ('npm --version') do echo %OK% npm found %ORANGE%v%%i%RESET%
)

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 3]%RESET% Checking if vite exists
echo %BLUE%=========================================%RESET%
where vite >nul 2>nul
if errorlevel 1 (
    echo %ERROR% 'vite' not found.
    @echo off
    echo Do you want to install Vite globally? [Y/N]

    choice /c YN /n /m ""
    if errorlevel 2 goto vite_no
    if errorlevel 1 goto vite_yes

    :vite_yes
    echo %INFO% Installing Vite...
    start "" /wait cmd /c "npm install -g vite"
    echo %INFO% Installing Vite completed.
    goto vite_end

    :vite_no
    echo %ERROR% Cannot run app without vite installed
    echo Press any key to close this window...
    pause >nul
    exit /b 0

    :vite_end 
    echo %OK% Done.
) else (
    for /f %%i in ('vite --version') do echo %OK% vite found %ORANGE%%%i%RESET%
)

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 4]%RESET% Checking directories
echo %BLUE%=========================================%RESET%
if not exist "EventFlowAPI\EventFlowAPI" (
    echo %EROR% Folder 'EventFlowAPI\EventFlowAPI' does not exist.
    echo Press any key to close this window...
    pause >nul
    exit /b 0
)
if not exist "EventFlowClient" (
    echo %ERROR% Folder 'EventFlowClient' does not exist.
    echo Press any key to close this window...
    pause >nul
    exit /b 0
)
echo %OK% Required directories were found.

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 5]%RESET% Restoring API dependencies and applying migrations
echo %BLUE%=========================================%RESET%
cd EventFlowAPI\EventFlowAPI

if not exist "obj" (
    echo %INFO% Restoring .NET dependencies...
    dotnet restore
    echo %OK% Restoring completed.
) else (
    echo %OK% Dependencies are already restored.
)

cd ..\EventFlowAPI.DB
where dotnet-ef >nul 2>nul
if errorlevel 1 (
    echo %ERROR% EF Core not found.
    @echo off
    echo Do you want to install Entity Framework Core? [Y/N]

    choice /c YN /n /m ""
    if errorlevel 2 goto efcore_no
    if errorlevel 1 goto efcore_yes

    :efcore_yes
    echo == %CYAN%[STEP 5.1]%RESET% Installing Entity Framework Core ==%RESET%
    echo %INFO% Installing EF Core...
    dotnet tool install --global dotnet-ef
    echo %INFO% Installing EF Core completed.
    goto efcore_end
    
    :efcore_no
    echo %ERROR% Cannot run app without EF Core installed
    echo Press any key to close this window...
    pause >nul
    exit /b 0

    :efcore_end 
    echo %OK% Done.
) else (
    for /f %%i in ('dotnet ef --version') do echo %OK% EF Core found %ORANGE%v%%i%RESET%
    @echo off
    echo Do you want to apply migrations? [Y/N]
    
    choice /c YN /n /m ""
    if errorlevel 2 goto migrations_no
    if errorlevel 1 goto migrations_yes

    :migrations_yes
    echo %INFO% Applying EF Core migrations...
    dotnet ef database update
    echo %OK% Applying migrations completed.
    
    :migrations_no
    goto efcore_end

    :efcore_end 
    echo %OK% Done.
)

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 6]%RESET% Checking Client dependencies
echo %BLUE%=========================================%RESET%
cd ..\..\EventFlowClient
if exist "node_modules" (
    echo %OK% Dependencies already installed.
) else (
    echo %INFO% Installing dependencies...
    if exist "package-lock.json" (
        del package-lock.json
    )   
    start "" /wait cmd /c "npm cache clean --force && npm install"
    echo %OK% Dependencies installed successfully. 
)
cd ..

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 7]%RESET% Starting WEB servers
echo %BLUE%=========================================%RESET%
start "EventFlow API" cmd /k "cd EventFlowAPI\EventFlowAPI && dotnet run --launch-profile api"
if errorlevel 1 (
    echo %ERROR% Failed to start API Web server.
    echo Press any key to continue...
    pause >nul
    exit /b 0
)
echo %OK% API Web server started successfully.

start "EventFlow Client" cmd /k "cd EventFlowClient && npm run dev"
if errorlevel 1 (
    echo %ERROR% Failed to start Client Web server.
    echo Press any key to continue...
    pause >nul
    exit /b 0
)
echo %OK% Client Web server started successfully.

echo.
echo %BLUE%=========================================%RESET%
echo %CYAN%[STEP 8]%RESET% Opening browser
echo %BLUE%=========================================%RESET%
start "" "http://localhost:5173" || (
    echo %WARNING% Could not open browser. Please open manually: http://localhost:5173
    echo Press any key to close this window...
    pause >nul
    exit /b 0
)
echo %OK% Browser opened successfully.

endlocal
exit /b 0