using tetris.Core.Abstractions;
using tetris.Core.Playables;
using tetris.Core.State.Misc;

namespace tetris.Core.Helpers;

public static class PlayableHelper
{
    public static IPlayable GetPlayable(Arguments arguments)
    {
        IPlayable dropPlayable = new DropPlayable(arguments, null);
        IPlayable mapPlayable = new MapPlayable(arguments, dropPlayable);

        return mapPlayable;
    }
}