@echo off
setlocal

REM Ask for the input file name (must be in the current folder)
set /p inputFile="Enter the name of the input file (with extension): "

REM Check if the input file exists
if not exist "%inputFile%" (
    echo File "%inputFile%" not found.
    exit /b
)

REM Extract the file name without the extension
for %%f in ("%inputFile%") do set "fileName=%%~nf"

REM Ask for the desired output format
echo.
echo Select output format:
echo 1. MP4
echo 2. WebM
echo 3. MOV
echo 4. MKV
set /p formatChoice="Enter the number corresponding to the format: "

REM Determine output extension and codec settings
set "videoCodec="
set "audioCodec="
set "outputExt="

if "%formatChoice%"=="1" (
    set "outputExt=mp4"
    set "videoCodec=libx264"
    set "audioCodec=aac"
) else if "%formatChoice%"=="2" (
    set "outputExt=webm"
    set "videoCodec=libvpx"
    set "audioCodec=libvorbis"
) else if "%formatChoice%"=="3" (
    set "outputExt=mov"
    set "videoCodec=libx264"
    set "audioCodec=aac"
) else if "%formatChoice%"=="4" (
    set "outputExt=mkv"
    set "videoCodec=libx264"
    set "audioCodec=aac"
) else (
    echo Invalid selection.
    exit /b
)

REM Generate the output file name
set "outputFile=%fileName%.%outputExt%"

REM Run FFmpeg to convert the input file
ffmpeg -i "%inputFile%" -c:v %videoCodec% -b:v 1M -c:a %audioCodec% "%outputFile%"

REM Check if conversion was successful
if %errorlevel% equ 0 (
    echo Conversion successful. Output file: "%outputFile%"
) else (
    echo Conversion failed.
)

pause