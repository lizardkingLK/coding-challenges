namespace wcTool.Core
{
    public class Result<T>
    {
#pragma warning disable IDE0290 // okay since multiple targets project 
        public Result(T? data, string? error)
        {
            Data = data;
            Error = error;
        }
#pragma warning restore IDE0290 // okay since multiple targets project 

        public T? Data { get; }

        public string? Error { get; }
    }
}