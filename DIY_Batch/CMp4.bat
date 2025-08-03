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

REM Generate the output file name with .webm extension
set "outputFile=%fileName%.mp4"

REM Run FFmpeg to convert the input file to WebM
ffmpeg -i "%inputFile%"  -c:v libx264 -preset slow -crf 18 -c:a aac -b:a 192k "%outputFile%"

REM Check if conversion was successful
if %errorlevel% equ 0 (
    echo Conversion successful. Output file: "%outputFile%"
) else (
    echo Conversion failed.
)

pause