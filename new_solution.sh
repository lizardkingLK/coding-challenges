#!/usr/bin/bash

# chmod +x ./new_solution.sh

# function declarations
function write_error
{
	echo -e "\e[31m$1\e[0m"
}

# global variables
solution_directory=$1
solution_name=$2
project_name=$3

# validates solution directory
if [ ! -d "$solution_directory" ];
then
	write_error "error. path given $solution_directory is invalid"
	exit 1
fi

# validates solution name
if [ -z "$solution_name" ];
then
	write_error "error. name given solution name $solution_name is invalid"
	exit 1
fi

# validation project name
if [ -z "$project_name" ];
then
	write_error "error. name given project name $project_name is invalid"
	exit 1
fi

# current directory
current_directory=$(pwd)

# create directory path for solution
solution_path=$(realpath "$solution_directory/$solution_name")

# create directory for solution
mkdir $solution_path

# go to solution path
cd $solution_path

# create solution
dotnet new sln --force

# create classlib project name
classlib_name="$solution_name.$project_name"

# create classlib project path
classlib_path="$solution_path/src/$classlib_name"

# create classlib
dotnet new classlib --name $classlib_name --no-restore --output $classlib_path --force

# add classlib to solution
dotnet sln add "$classlib_path/$classlib_name.csproj"

# create console project name
console_name="$solution_name.Program"

# create console project path
console_path="$solution_path/src/$console_name"

# create console
dotnet new console --use-program-main --name $console_name --no-restore --output $console_path --force

# add console to solution
dotnet sln add "$console_path/$console_name.csproj"

# create test project name
test_name="$solution_name.$project_name.Tests"

# create test project path
test_path="$solution_path/tests/$test_name"

# create test
dotnet new xunit --name $test_name --no-restore --output $test_path --force

# add test to solution
dotnet sln add "$test_path/$test_name.csproj"

# add reference of classlib project to console project
dotnet add "$console_path/$console_name.csproj" reference "$classlib_path/$classlib_name.csproj"

# add reference of classlib project to tests project
dotnet add "$test_path/$test_name.csproj" reference "$classlib_path/$classlib_name.csproj"

# reset location to original/previous location
cd $current_directory

# open in vscode
code $solution_path

# open in neovim
# nvim $solution_path
