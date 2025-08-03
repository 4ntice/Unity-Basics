@echo off

REM Play sound notification
powershell -c "(New-Object Media.SoundPlayer 'C:\Windows\Media\notify.wav').PlaySync();"

pause >nul