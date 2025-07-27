namespace snakeGame.Core.Abstractions;

public interface IPublish<T>
{
    void Publish(T @event);
	void Attach(ISubscribe<T> subscriber);
	void Detach(ISubscribe<T> subscriber);
}