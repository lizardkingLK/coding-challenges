using Microsoft.Data.Sqlite;

namespace tetris.Core.Helpers;

public static class ScoresHelper
{
    private const string DBFileName = "tetris.data";

    private const string ConnectionStringFormat = "Data Source={0}";

    private const string CreateScoresQuery = """
    CREATE TABLE IF NOT EXISTS Scores (
        Id TEXT PRIMARY KEY,
        Username VARCHAR(100),
        GameMode BIT,
        PlayMode BIT,
        Time INTEGER,
        Score INTEGER
    );
    """;

    private const string InsertScoresQuery = """
    INSERT INTO Scores
    (`Id`, `Username`, `GameMode`, `PlayMode`, `Time`, `Score`)
    VALUES
    ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');
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
            SqliteConnection connection = new(_connectionString);

            connection.Open();
            _isAvailable = true;
            connection.Close();
        }
        catch (Exception)
        {
            _isAvailable = false;
        }
    }
}