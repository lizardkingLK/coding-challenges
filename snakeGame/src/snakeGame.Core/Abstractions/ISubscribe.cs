namespace snakeGame.Core.Abstractions;

public interface ISubscribe<T>
{
    public void Notify(T state);
}