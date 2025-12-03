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
play-mode      = [-pm  | --play-mode]  <Play-Mode>
```

#### Difficulty Levels

```
-1 - Easy
0  - Medium
1  - Hard
```

#### Game Modes

```
0  - Classic (Score multiplier on difficulty)
1  - Timed (Score multiplier on difficulty and time elapsed)
```

#### Map Size

```
0  - Normal (Normal Map Size)
1  - Scaled (Doubled Map Size)
```

#### Play Mode

```
0  - Drop (Tetrominoes drops down with input)
1  - Float (Tetrominoes waits for the input)
```

### Controls

Right/Down/Left Keys
- Moves Tetromino by one cordinate at respective direction

Z Key
- Rotates Tetromino according to super rotation system

Space Key
- Slams Tetromino down to the lowest possible place

Escape Key
- Pause Game

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
tetris -d -1 -gm 0 -ms 0 -pm 0
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

## Issues

Send your issues at my [GitHub](https://github.com/lizardkinglk/coding-challenges)
