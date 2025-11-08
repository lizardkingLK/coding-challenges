namespace tetris.Core.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class ArgumentAttribute<T> : Attribute
{
    public required string Prefix { get; init; }

    public required string Name { get; init; }

    public T? Default { get; init; }
}