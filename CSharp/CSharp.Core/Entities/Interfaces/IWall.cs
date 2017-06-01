using CSharp.Core.Values;

namespace CSharp.Core.Entities.Interfaces
{
    public interface IWall
    {
        void AddPost(Post post);
        void Follow(string whoToFollow);
    }
}