namespace CSharp.Core
{
    public interface PostRepository
    {
        void Save(Post post);
        Post ReadPostFrom(string user);
        void Save(IWall wall);
        IWall LoadOrCreateWallOf(string user);
        ReadWall ReadWallOf(string user);

    }
}