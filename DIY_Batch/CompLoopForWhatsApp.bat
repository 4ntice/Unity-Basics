@echo off
setlocal EnableDelayedExpansion

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

:: === CONFIGURATION ===
set input=%inputFile%
set output=%fileName%.mp4
set maxSizeBytes=16777216
set initialScale=1280
set scaleStep=160
set minScale=320
set crf=28

:: === Get input file size in bytes ===
for %%F in ("%input%") do set size=%%~zF

:: === If already under limit, just copy ===
if !size! LSS %maxSizeBytes% (
    echo File is already under 16MB. Copying without compression.
    copy "%input%" "%output%"
    goto :eof
)

:: === Start compression loop ===
set scale=%initialScale%

:compress_loop
echo Trying scale %scale% with CRF %crf%...

ffmpeg -y -i "%input%" -vf "scale=trunc(%scale%/2)*2:-2" -c:v libx264 -preset medium -crf %crf% -c:a aac -b:a 96k -movflags +faststart "%output%"

for %%F in ("%output%") do set newSize=%%~zF

if !newSize! LSS %maxSizeBytes% (
    echo Compression successful. Output is under 16MB.
    goto :eof
)

:: Decrease resolution
set /a scale-=scaleStep

if !scale! LSS %minScale% (
    echo Cannot reduce scale further. Compression failed to reach target size.
    goto :eof
)

goto compress_loop