# Tetris

A tetris game built into command line. Made with dotnet.

For publish and installation steps below, set the location to tetris.Program directory as the root before execution.

## Publish

```
dotnet pack
```

## Installation

```
dotnet tool install --global --add-source .\nupkg tetris.Program
```

## Run

### Requirements

- dotnet sdk version 9.0
- text editor

### Options

```
help           = -h    | --help
scores         = -s    | --scores
interactive    = -it   | --interactive

difficulty     = [-d   | --difficulty] <Difficulty-Level>
game-mode      = [-gm  | --game-mode]  <Game-Mode>
map-size       = [-ms  | --map-size]   <Map-Size>
play-mode      = [-pm  | --play-mode]  <Points-To-Win>
```

#### Game Modes

```
0  - Classic
1  - Timed
```

#### Difficulty Levels

```
-1 - Easy
0  - Medium
1  - Hard
```

### Inside Dev Environment

```
dotnet run -f net9.0 -- [above_options]
```

### Using Installed Binary

```
tetris [above_options]
```

### Example

```
pong -ptw 10 -gm 0 -d -1 -ps 1
```

```
@@@@@@@@@@@@
@          @
@   ####   @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@          @
@@@@@@@@@@@@
```

## Uninstall

```
dotnet tool uninstall -g tetris.Program
```
