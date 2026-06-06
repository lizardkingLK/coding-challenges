namespace ccct.Core.State.Common;

public record Result<T>(T? DataContent = default, string? ErrorContent = null)
{
    public T Data => DataContent!;
    public string Errors => ErrorContent!;
    public bool HasErrors => Errors != null;
}