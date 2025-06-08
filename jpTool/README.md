# JP

A json parsing command line tool made with dotnet.

For publish and installation steps below, set the location to jpTool.Cli directory as the root before execution.

## Publish

```
dotnet pack 
```

## Installation

```
dotnet tool install --global --add-source .\nupkg jpTool.cli
```

## Run

### Requirements

- dotnet sdk version 8.0 or 9.0
- text editor

### Inside Dev Environment

```
dotnet run -f <your_current_dotnet_version> -- <path_to_a_json_file>
```

### Using Installed Binary

```
jp <path_to_a_json_file>
```

## Uninstall

```
dotnet tool uninstall -g jptool.cli
```