namespace jpTool.Core
{
    public class Result<T>
    {
        public Result(T? data, string? errors)
        {
            Data = data;
            Errors = errors;
        }

        public T? Data { get; }

        public string? Errors { get; }
    }
}