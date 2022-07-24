# How to manually build the BF2.TV App

1. Publish the project `BF2TV.WindowsApp` using the existing "PUBLISH" profile or run the script `_CreateAppRelease.ps1`
2. Ensure the binaries got created here:
   1. `\build\bin\publish\`
3. Ensure the same path is configured in the `setup_script.iss` file below the [Files] section
4. (OPTIONAL: When creating a GitHub release)
   1. Raise version number in `setup_script.iss` file
   2. Raise version number in `src\BF2TV.WindowsApp\BF2TV.WindowsApp.csproj` file
   3. Commit & push to main/master branch (don't raise the `AutoUpdate.xml` version yet)
5. Assuming you have https://jrsoftware.org/isdl.php installed
   1. Open the .iss file with Inno Setup Compiler
   2. Hit CTRL+F9 to compile the installer file (or compile it in the menu)
   3. It should have created the installer file in: `\build\bin\`
6. (OPTIONAL: When creating a GitHub release)
   1. Create GitHub tag & release with the updated installer file
   2. Adjust to new version number in `AutoUpdate.xml` to notify users with old installed versions
   3. Commit & push to main/master branch