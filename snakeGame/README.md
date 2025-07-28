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
width       = [-[w|-width] [10-40]]
height      = [-[h|-height] [10-20]]
output      = [-[o|-output] [0-1]]
game-mode   = [-[gm|-game-mode] [0-1]]
difficulty  = [-[d|-difficulty] [0-2]]
```

#### Game Modes

```
0 - Automatic
1 - Manual
```

#### Output Types

```
0 - Default Console uses terminal
1 - Text File Output may use vscode
```

#### Difficulty Levels

```
0 - Easy
1 - Medium
2 - Hard
```

### Inside Dev Environment

```
dotnet run -f net9.0 -- [above_options]
```

### Using Installed Binary

```
snake [above_options]
```

### Example

```
snake -w 10 -h 10 -o 1
```

```
&&&&&&&&&&
&        &
&     +  &
&        &
&  0     &
&  O     &
&  O     &
&  O     &
&        &
&&&&&&&&&&
```

## Uninstall

```
dotnet tool uninstall -g snakegame.program
```
