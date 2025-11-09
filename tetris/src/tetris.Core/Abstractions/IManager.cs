namespace tetris.Core.Abstractions;

public interface IManager
{
    public void New();
    public void Play();
    public void Pause();
    public void Reset();
    public void Quit();
}