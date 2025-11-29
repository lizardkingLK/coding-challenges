using Microsoft.Data.Sqlite;
using tetris.Core.Enums.Arguments;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Helpers;

public static class ScoresHelper
{
    private const string DBFileName = "tetris.data";
    private const string ConnectionStringFormat = "Data Source={0}";
    private const string DatabaseNotAvailable = "error. database not available";

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
    ORDER BY Score
    LIMIT 10;
    """;

    private static readonly bool _isAvailable;
    private static readonly string _connectionString;

    static ScoresHelper()
    {
        _connectionString = string.Format(
            ConnectionStringFormat,
            Path.Combine(
                Directory.GetCurrentDirectory(),
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

    public static Result<bool> Insert(Arguments arguments, int time, int score)
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

    public static Result<DynamicallyAllocatedArray<GameScore>> Select()
    {
        try
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            using SqliteCommand command = new(SelectTopScoresQuery, connection);
            SqliteDataReader reader = command.ExecuteReader();

            DynamicallyAllocatedArray<GameScore> scores = [];
            while (reader.Read())
            {
                scores.Add(new GameScore
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    GameMode = ((GameModeEnum)reader.GetInt32(2)).ToString(),
                    PlayMode = ((PlayModeEnum)reader.GetInt32(3)).ToString(),
                    Time = (reader.GetInt32(4) / 1e3).ToString(),
                    Score = reader.GetInt32(5),
                });
            }

            return new(scores);
        }
        catch (Exception)
        {
            throw;
        }
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