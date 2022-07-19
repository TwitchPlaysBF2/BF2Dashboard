cd ..\src\BF2Dashboard.WindowsApp\
dotnet publish /p:Configuration=Release /p:PublishProfile=Properties\PublishProfiles\PUBLISH
Write-Output "*********** done creating release ***********"