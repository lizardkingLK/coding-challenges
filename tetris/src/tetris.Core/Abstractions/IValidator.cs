using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;

namespace tetris.Core.Abstractions;

public interface IValidator<TKey, TOutput> where TKey : notnull
{
    public TOutput Value { get; init; }

    public HashMap<TKey, string> Values { get; init; }

    public IValidator<TKey, TOutput>? Next { get; init; }

    public Result<TOutput> Validate();
}