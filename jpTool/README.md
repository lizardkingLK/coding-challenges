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

- dotnet sdk version 6.0 or 9.0
- text editor

### Inside Dev Environment

```
dotnet run -f <your_current_dotnet_version> -- "-c" <path_to_a_text_file>
```

### Using Installed Binary

```
ccwc -c <path_to_a_text_file>
```

## Uninstall

```
dotnet tool uninstall -g wctool.cli
```