using System.Text;
using Microsoft.Data.Sqlite;
using tetris.Core.Enums.Arguments;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Helpers;

public static class ScoresHelper
{
    private const string DBFileName = "tetris.data";
    private const string ConnectionStringFormat = "Data Source={0}";
    private const string DatabaseNotAvailable = "error. database not available";
    private const double MillisecondsToHour = 3.6e6;

    private const string CreateScoresQuery = """
    CREATE TABLE IF NOT EXISTS Scores (
        Id TEXT PRIMARY KEY,
        Username VARCHAR(255),
        GameMode INTEGER,
        PlayMode INTEGER,
        Time INTEGER,
        Score INTEGER
    );
    """;

    private const string Id = "@Id";
    private const string Username = "@Username";
    private const string GameMode = "@GameMode";
    private const string PlayMode = "@PlayMode";
    private const string Time = "@Time";
    private const string Score = "@Score";

    private const string InsertScoresQuery = $"""
    INSERT INTO Scores
    (Id, Username, GameMode, PlayMode, Time, Score)
    VALUES
    ({Id}, {Username}, {GameMode}, {PlayMode}, {Time}, {Score});
    """;

    private const string SelectTopScoresQuery = """
    SELECT * 
    FROM Scores
    ORDER BY Score DESC, GameMode
    LIMIT 10;
    """;

    private static readonly bool _isAvailable;
    private static readonly string _connectionString;

    static ScoresHelper()
    {
        _connectionString = string.Format(
            ConnectionStringFormat,
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                DBFileName));
        try
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            CreateTable();

            _isAvailable = true;
        }
        catch (Exception)
        {
            _isAvailable = false;
        }
    }

    public static Result<bool> Insert(Arguments arguments, long time, int score)
    {
        if (!_isAvailable)
        {
            return new(false, DatabaseNotAvailable);
        }

        try
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            using SqliteCommand command = new(InsertScoresQuery, connection);
            command.Parameters.AddWithValue(Id, Guid.NewGuid());
            command.Parameters.AddWithValue(Username, Environment.UserName);
            command.Parameters.AddWithValue(GameMode, arguments.GameMode);
            command.Parameters.AddWithValue(PlayMode, arguments.PlayMode);
            command.Parameters.AddWithValue(Time, time);
            command.Parameters.AddWithValue(Score, score);
            command.ExecuteNonQuery();

            return new(true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Result<DynamicallyAllocatedArray<DynamicallyAllocatedArray<string>>> Select()
    {
        try
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            using SqliteCommand command = new(SelectTopScoresQuery, connection);
            SqliteDataReader reader = command.ExecuteReader();

            DynamicallyAllocatedArray<DynamicallyAllocatedArray<string>> scores = [];
            while (reader.Read())
            {
                scores.AddRange(new DynamicallyAllocatedArray<string>
                (
                    reader.GetString(1),
                    ((GameModeEnum)reader.GetInt32(2)).ToString(),
                    ((PlayModeEnum)reader.GetInt32(3)).ToString(),
                    GetDurationString(reader.GetInt64(4)),
                    reader.GetInt32(5).ToString()
                ));
            }

            return new(scores);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string GetDurationString(long totalMilliseconds)
    {
        double millisecondsPerUnit = MillisecondsToHour;
        StringBuilder timeBuilder = new();

        double hours = totalMilliseconds / millisecondsPerUnit;
        double remainder = totalMilliseconds % millisecondsPerUnit;
        if (hours >= 1)
        {
            timeBuilder
            .Append((int)hours)
            .Append(SymbolHours);
        }

        millisecondsPerUnit /= 60;
        double minutes = remainder / millisecondsPerUnit;
        remainder %= millisecondsPerUnit;
        if (minutes >= 1)
        {
            timeBuilder
            .Append(SymbolSpaceBlock)
            .Append((int)minutes)
            .Append(SymbolMinutes);
        }

        millisecondsPerUnit /= 60;
        double seconds = remainder / millisecondsPerUnit;
        remainder %= millisecondsPerUnit;
        if (seconds >= 1)
        {
            timeBuilder
            .Append(SymbolSpaceBlock)
            .Append((int)seconds)
            .Append(SymbolSeconds);
        }

        millisecondsPerUnit /= 1000;
        double milliseconds = remainder / millisecondsPerUnit;
        if (milliseconds >= 1)
        {
            timeBuilder
            .Append(SymbolSpaceBlock)
            .Append((int)milliseconds)
            .Append(SymbolMilliseconds);
        }

        return timeBuilder.ToString();
    }

    private static void CreateTable()
    {
        try
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            using SqliteCommand command = new(CreateScoresQuery, connection);
            command.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
    }
}