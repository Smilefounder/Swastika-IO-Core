@echo off
:Ask
echo Would you like to OVERWRITE local files from GIT?(Y/N)
set INPUT=
set /P INPUT=Type input: %=%
If /I "%INPUT%"=="y" goto yes 
If /I "%INPUT%"=="n" goto no
echo Incorrect input & goto Ask
:yes
git fetch --all
git reset --hard origin/master
goto cont
:no
echo Cancel action. Nothing is done!
:cont
pause