using SocialNetwork.Core.Values;

namespace SocialNetwork.Core.Entities.Interfaces
{
    public interface IWall
    {
        void AddPost(Post post);
        void Follow(string whoToFollow);
        void SendMessage(UserMessage message);
    }
}