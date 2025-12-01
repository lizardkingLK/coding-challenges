# allow execution access
# chmod +x ./rebuild_tool.sh

#!/bin/bash

# set current location
currentDirectory=$(pwd)

# set tool name
toolName='tetris.Program'

# if tool contains in the system
toolList=$(dotnet tool list --global $toolName)

# uninstall if installed
contains=${toolList[@]} | grep -q "$toolName"
if printf "%s\n" "${toolList[@]}" | grep -q "$toolName"; then
  $(dotnet tool uninstall --global $toolName)
fi

# go to cli root
cd ./src/$toolName

# package the solution
packaged=$(dotnet pack)

# install the tool
installed=$(dotnet tool install --global --add-source ./nupkg $toolName)

# set current directory as location
cd $currentDirectory