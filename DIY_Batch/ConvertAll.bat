@echo off
set "default_dir=%cd%"
set /p "input_dir=Enter the directory containing the files (leave blank to use the current folder): "

rem Check if the input_dir is empty, and if so, use the current directory
if "%input_dir%"=="" (
    set /p "use_default=No directory entered. Use the current folder [%default_dir%]? (Y/N): "
    if /I "%use_default%"=="Y" (
        set "input_dir=%default_dir%"
    ) else (
        echo Exiting script.
        exit /b
    )
)

set /p "file_ext=Enter the file extension (e.g., .mp4): "

rem Loop through all files in the directory with the specified extension
for %%F in ("%input_dir%\*%file_ext%") do (
    rem Extract the file name without extension
    set "filename=%%~nF"
    set "input_file=%%F"
    set "output_file=%input_dir%\%filename%_output%file_ext%"

    rem Convert the file
    ffmpeg -i "%%F" -c:v libx265 -preset slow -crf 28 -c:a aac -strict experimental "%output_file%"

    rem Check if conversion was successful, then delete the original file
    if exist "%output_file%" (
        del "%%F"
        echo Deleted original file: %%F
    ) else (
        echo Conversion failed for file: %%F
    )
)

pause
