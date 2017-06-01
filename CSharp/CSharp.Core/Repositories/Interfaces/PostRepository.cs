using CSharp.Core.Entities.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Repositories.Interfaces
{
    public interface PostRepository
    {
        void Save(IWall wall);
        IWall LoadOrCreateWallOf(string user);
        WallView ReadWallOf(string user);

    }
}