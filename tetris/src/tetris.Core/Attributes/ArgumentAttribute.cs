using tetris.Core.Enums.Game;

namespace tetris.Core.Attributes;

[AttributeUsage(AttributeTargets.Enum, AllowMultiple = true)]
public class ArgumentAttribute : Attribute
{
    public required string Prefix { get; init; }
    public required string Name { get; init; }
    public ArgumentTypeEnum Type { get; init; }
    public bool IsSwitch { get; init; }
}