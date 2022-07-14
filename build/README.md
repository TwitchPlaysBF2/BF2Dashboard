# How to manually create an app build

1. Compile the project "BF2Dashboard.WindowsApp" in "Release" mode
2. Ensure the binaries got created here:
   1. `\src\BF2Dashboard.WindowsApp\bin\Release\net6.0-windows`
   2. Or what ever path is configured in the `setup_script.iss` file below the [Files] section
3. (OPTIONAL: When creating a GitHub release)
   1. Raise version number in `setup_script.iss` file
   2. Commit & push to main/master branch (don't raise the `AutoUpdate.xml` version yet)
4. Assuming you have https://jrsoftware.org/isdl.php installed
   1. Open the .iss file with Inno Setup Compiler
   2. Hit CTRL+F9 to compile the installer file (or compile it in the menu)
   3. It should have created the installer file in: `\build\bin\`
5. (OPTIONAL: When creating a GitHub release)
   1. Create GitHub tag & release with the updated installer file
   2. Adjust to new version number in `AutoUpdate.xml` to notify users with old installed versions
   3. Commit & push to main/master branch