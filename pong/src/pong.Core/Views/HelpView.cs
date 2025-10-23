using pong.Core.Abstractions;

namespace pong.Core.Views;

public class HelpView : View
{
    public override string? Data => """
    Help is here
    """;

    public override string? Message => string.Empty;

    public override string? Error => string.Empty;
}