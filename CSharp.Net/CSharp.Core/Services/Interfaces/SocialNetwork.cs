using CSharp.Core.Views;
using LanguageExt;

namespace CSharp.Core.Services.Interfaces
{
    public interface SocialNetwork
    {        
        void Post(string user, string message);
        Unit Follow(string user, string whoToFollow);
        WallView ReadWall(string user);
        void SendMessage(string sender, string to, string message);
    }
}