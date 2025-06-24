# Snake

A snake game built into command line made with dotnet.

For publish and installation steps below, set the location to snakeGame.Program directory as the root before execution.

## Publish

```
dotnet pack
```

## Installation

```
dotnet tool install --global --add-source .\nupkg snakegame.program
```

## Run

### Requirements

- dotnet sdk version 9.0
- text editor

### Options

```
width   = [-[w|-width] <width_value>]
height  = [-[h|-height] <height_value>]
```

### Inside Dev Environment

```
dotnet run -f net9.0 -- [options]
```

### Using Installed Binary

```
snake [-[w|width] <width_value>] [-[h|height] <height_value>]
```

### Example

```
snake -w 200 -h 100
```

## Uninstall

```
dotnet tool uninstall -g snakegame.program
```
