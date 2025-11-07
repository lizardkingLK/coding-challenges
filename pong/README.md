# Pong

A pong game built into command line made with dotnet.

For publish and installation steps below, set the location to pong.Program directory as the root before execution.

## Publish

```
dotnet pack
```

## Installation

```
dotnet tool install --global --add-source .\nupkg pong.Program
```

## Run

### Requirements

- dotnet sdk version 9.0
- text editor

### Options

```
help          = -h    | --help
interactive   = -it   | --interactive

game-mode     = [-gm  | --game-mode]     <Game-Mode>
difficulty    = [-d   | --difficulty]    <Difficulty-Level>
player-side   = [-ps  | --player-side]   <Player-Side>
points-to-win = [-ptw | --points-to-win] <Points-To-Win>
```

#### Game Modes

```
-1  - Automatic
0   - Offline Single Player
1   - Offline Multi Player
2   - Online (TBA)
```

#### Difficulty Levels

```
-1 - Easy
0  - Medium
1  - Hard
```

#### Player Side

```
0 - Left Player Side
1 - Right Player Side
```

### Inside Dev Environment

```
dotnet run -f net9.0 -- [above_options]
```

### Using Installed Binary

```
pong [above_options]
```

### Example

```
pong -ptw 10 -gm 0 -d -1 -ps 1
```

```
@@@@@@@@@@@@@@@0@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@0@@@@@@@@@@@@@@@
                              |
                              |
                           O  |
                              |
                              |
                              |
#                             |                              #
#                             |                              #
#                             |                              #
#                             |                              #
#                             |                              #
                              |
                              |
@@@@@@@@@@@@@@CPU@@@@@@@@@@@@@@@@@@@@@@@@@@Player@@@@@@@@@@@@@

```

## Uninstall

```
dotnet tool uninstall -g pong.Program
```
