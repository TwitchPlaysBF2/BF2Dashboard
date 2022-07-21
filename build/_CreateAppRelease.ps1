cd ..\src\BF2TV.WindowsApp\
dotnet publish /p:Configuration=Release /p:PublishProfile=Properties\PublishProfiles\PUBLISH
Write-Output "*********** done creating release ***********"