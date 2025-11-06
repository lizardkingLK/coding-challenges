namespace pong.Core.Shared;

public static class Constants
{
    #region Difficulty
    public const int DefaultSpeed = 4;
    public const int DefaultBallMoveInterval = 20;
    public const int DefaultBallSpawnTimeout = 500;
    public const int DefaultCPUWaitTimeout = 20;
    #endregion

    public const int DefaultPointsToWin = 10;
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