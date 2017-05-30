namespace CSharp.Core
{
    public interface PostRepository
    {
        void Save(IWall wall);
        IWall LoadOrCreateWallOf(string user);
        ReadWall ReadWallOf(string user);

    }
}