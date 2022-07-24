Remove-Item 'bin/publish' -Recurse

cd ..\src\BF2TV.WindowsApp\
dotnet publish /p:Configuration=Release /p:PublishProfile=Properties\PublishProfiles\PUBLISH
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********** done creating release ***********"
Write-Output "********* press CTRL + F9 to build **********"
Write-Output "******* the exectuable in inno setup ********"
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********************************************"
Write-Output "*********************************************"

cd ../../build/bin

Start .
Start "C:\Program Files (x86)\Inno Setup 6\Compil32.exe"