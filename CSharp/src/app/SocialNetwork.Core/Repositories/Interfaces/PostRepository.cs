using SocialNetwork.Core.Entities.Interfaces;
using SocialNetwork.Core.Views;
using LanguageExt;

namespace SocialNetwork.Core.Repositories.Interfaces
{
    public interface PostRepository
    {
        Unit Save(IWall wall);
        IWall LoadOrCreateWallOf(string user);
        WallView ReadWallOf(string user);

    }
}