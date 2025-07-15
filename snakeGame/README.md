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
output  = [-[o|-output] [0-1]]
```

### Inside Dev Environment

```
dotnet run -f net9.0 -- [options]
```

### Using Installed Binary

```
snake <Options.width> <Options.height> <Options.output>
```

### Example

```
snake -w 20 -h 10 -o 0
```

```
********************
*                  *
*         O        *
*         O        *
*         O        *
*         0        *
*        +         *
*                  *
*                  *
********************
```

## Uninstall

```
dotnet tool uninstall -g snakegame.program
```
