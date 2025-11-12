using tetris.Core.Enums.Commands;
using tetris.Core.Handlers;
using tetris.Core.Shared;

namespace tetris.Core.Abstractions;

public interface IManager
{
    public MapManager? MapManager { get; set; }
    public IOutput? Output { get; set; }

    public void Input(InputTypeEnum inputType);
    public Result<bool> New();
    public Result<bool> Play();
    public void Pause();
    public void Quit();
}