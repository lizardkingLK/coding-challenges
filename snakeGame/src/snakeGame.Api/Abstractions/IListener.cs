namespace snakeGame.Api.Abstractions; 

public interface IListener<T>
{
    public Task<T> Wait();

    public void Notify(T item);

    public void Reset();
}