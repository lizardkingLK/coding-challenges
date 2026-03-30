namespace ccct.Core.Abstractions;

public interface IValidator
{
    public IValidator? Next { get; set; }
    void Validate();
}