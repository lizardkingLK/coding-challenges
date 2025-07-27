using snakeGame.Api.Abstractions;

namespace snakeGame.Api.Services;

public class ListenerService<T> : IListener<T>
{
    private TaskCompletionSource<T> _taskCompletionSource = new();

    public Task<T> Wait()
    {
        return _taskCompletionSource.Task;
    }

    public void Notify(T item)
    {
        _taskCompletionSource.TrySetResult(item);
    }

    public void Reset()
    {
        _taskCompletionSource = new();
    }
}
