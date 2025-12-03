# allow execution access
# chmod +x ./rebuild_tool.sh

#!/bin/bash

# set current location
currentDirectory=$(pwd)

# set tool name
toolName='tetris.Program'

# uninstall if tool contains in the system
if dotnet tool list --global $toolName | grep -q "$toolName"; then
  installPlease=$(dotnet tool uninstall --global $toolName)
fi

# go to cli root
cd ./src/$toolName

# package the solution
packaged=$(dotnet pack)

# install the tool
installed=$(dotnet tool install --global --add-source ./nupkg $toolName)

# set current directory as location
cd $currentDirectory