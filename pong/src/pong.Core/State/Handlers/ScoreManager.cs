using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Game;
using static pong.Core.Enums.PlayerSideEnum;
using static pong.Core.Shared.Constants;

namespace pong.Core.State.Handlers;

public class ScoreManager(Output output)
{
    private readonly Output _output = output;

    private readonly HashMap<PlayerSideEnum, int> _scores
    = new(new(PlayerLeft, 0), new(PlayerRight, 0));

    private readonly HashMap<PlayerSideEnum, Position> _positions = new(
        new(PlayerLeft, new(0, output.Width / 4)),
        new(PlayerRight, new(0, 3 * output.Width / 4)));

    private HashMap<PlayerSideEnum, (Position, string)>? _labels;

    private readonly ConsoleColor _scoreColor = ConsoleColor.Cyan;
    private readonly ConsoleColor _playerColor = ConsoleColor.DarkCyan;

    public void Label(string[] labelsLeftToRight)
    {
        _labels = new(
            new(PlayerLeft,
            (new Position(
                _output.Height - 1,
                _output.Width / 4 - labelsLeftToRight[0].Length / 2),
                labelsLeftToRight[0])),
            new(PlayerRight,
            (new Position(
                _output.Height - 1,
                3 * _output.Width / 4 - labelsLeftToRight[1].Length / 2),
                labelsLeftToRight[1])));
    }

    public int Score(PlayerSideEnum playerSide)
    {
        int score = ++_scores[playerSide];
        _output.Draw(_positions[playerSide], score.ToString(), _scoreColor);

        return score;
    }

    public void Output()
    {
        _output.Draw(_labels![PlayerLeft].Item1, _labels![PlayerLeft].Item2, _playerColor);
        _output.Draw(_labels![PlayerRight].Item1, _labels![PlayerRight].Item2, _playerColor);

        _output.Draw(_positions[PlayerLeft], InitialScore, _scoreColor);
        _output.Draw(_positions[PlayerRight], InitialScore, _scoreColor);
    }

    public void Win(PlayerSideEnum playerSide)
    {
        string content = string.Format(FormatGameOver, _labels![playerSide].Item2);
        Position position = new(_output.Height / 2, _output.Width / 2 - content.Length / 2);
        _output.Draw(position, content, ConsoleColor.Green);
    }
}