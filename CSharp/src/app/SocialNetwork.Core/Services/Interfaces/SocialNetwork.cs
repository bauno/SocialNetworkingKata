using SocialNetwork.Core.Views;
using LanguageExt;

namespace SocialNetwork.Core.Services.Interfaces
{
    public interface ISocialNetwork
    {        
        void Post(string user, string message);
        Unit Follow(string user, string whoToFollow);
        WallView ReadWall(string user);
        void SendMessage(string sender, string to, string message);
    }
}