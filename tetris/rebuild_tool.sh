# allow execution access
# chmod +x ./rebuild_tool.sh

#!/bin/bash

# set current location
currentDirectory=$(pwd)

# set tool name
toolName='tetris.Program'

# uninstall if tool contains in the system
containsTool=false
if dotnet tool list --global | grep -q ${toolName,,}; then
  containsTool=true
fi

# if exists uninstall
if [ "$containsTool" = true ]; then
  echo "Uninstalling existing global tool: $toolName"
  dotnet tool uninstall --global "$toolName"
fi

# go to cli root
cd "./src/$toolName" || { echo "error. invalid directory ./src/$toolName"; exit 1; }

# package the solution
dotnet pack || { echo "error. dotnet pack failed"; exit 1; }

# install the tool
echo "info. installing the tool globally from local package"
dotnet tool install --global --add-source ./nupkg "$toolName" || { echo "error. install failed."; exit 1; }

# set current directory as location
cd $currentDirectory || { echo "error. cannot return to original directory"; }