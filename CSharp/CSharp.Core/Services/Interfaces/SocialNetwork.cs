using CSharp.Core.Views;

namespace CSharp.Core.Services.Interfaces
{
    public interface SocialNetwork
    {        
        void Post(string user, string message);
        void Follow(string user, string whoToFollow);
        WallView ReadWall(string user);
        void SendMessage(string sender, string to, string message);
    }
}