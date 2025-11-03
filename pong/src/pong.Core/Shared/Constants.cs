namespace pong.Core.Shared;

public static class Constants
{
    public const int DefaultSpeed = 2;
    public const int BallMoveInterval = 50;
    public const int BallSpawnTimeout = 500;
    public const int CPUWaitTimeout = 200;
    public const int GameEndTimeout = 5000;


    public const char NetBlockSymbol = '|';
    public const char SpaceBlockSymbol = ' ';
    public const char WallBlockSymbol = '@';
    public const char BallBlockSymbol = 'O';
    public const char RacketBlockSymbol = '#';
    public const char CharNewLine = '\n';
    public const string InitialScore = "0";
    public const string FormatGameOver = "{0} Won!";
    public const string Player = "Player";
    public const string CPU = "CPU";
}