namespace CSharp.Core
{
    public interface IWall
    {
        void AddPost(Post post);
        void Follow(string whoToFollow);
    }
}