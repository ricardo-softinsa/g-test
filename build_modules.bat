@echo off

echo %1
cd Modules
cd %1
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" /p:Configuration=Release
cd %1
dir
