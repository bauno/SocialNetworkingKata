using CSharp.Core.Entities.Interfaces;
using CSharp.Core.Views;
using LanguageExt;

namespace CSharp.Core.Repositories.Interfaces
{
    public interface PostRepository
    {
        Unit Save(IWall wall);
        IWall LoadOrCreateWallOf(string user);
        WallView ReadWallOf(string user);

    }
}