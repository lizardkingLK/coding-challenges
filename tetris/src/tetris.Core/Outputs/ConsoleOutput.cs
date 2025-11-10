using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs;

public class ConsoleOutput : IOutput
{
    public int Height { get; set; }

    public int Width { get; set; }

    public Result<bool> Create(MapSizeEnum mapSize)
    {
        Height = Console.WindowHeight;
        Width = Console.WindowWidth;
        if (Height < DefaultHeight)
        {
            return new(false, "error. cannot create game. height lower than minimum height");
        }

        if (Width < DefaultWidth)
        {
            return new(false, "error. cannot create game. width lower than minimum width");
        }

        if (mapSize == MapSizeEnum.NormalMap)
        {
            Width = DefaultWidth;
            Height = DefaultHeight;
        }
        else if (mapSize == MapSizeEnum.HalfMap)
        {
            Height /= 2;
            Width = Height / 2;
        }
        else if (mapSize == MapSizeEnum.FullMap)
        {
            Width = Height / 2;
        }

        return new(true);
    }

    public void Listen()
    {
        throw new NotImplementedException();
    }

    public void Update(Block block)
    {
        throw new NotImplementedException();
    }
}