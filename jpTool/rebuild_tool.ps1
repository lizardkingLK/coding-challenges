# set current location
$currentDirectory = $PWD

# set tool name
$toolName = 'jptool.cli'

# if tool contains in the system
$toolList = (& dotnet tool list --global $toolName)
$containsTool = ($toolList.Count -gt 2) -and ($toolList[2].Contains($toolName))

# uninstall installed tool
if ($containsTool) {
    & dotnet tool uninstall --global $toolName
}

# go to cli root
Set-Location ".\src\$toolName"

# package the solution
& dotnet pack

# install the tool
& dotnet tool install --global --add-source .\nupkg $toolName

# set current directory as location
Set-Location $currentDirectory