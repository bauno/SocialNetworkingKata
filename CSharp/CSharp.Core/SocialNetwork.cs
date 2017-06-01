using System.Collections.Generic;

namespace CSharp.Core
{
    public interface SocialNetwork
    {        
        void Post(string user, string message);
        void Follow(string user, string whoToFollow);

        WallView ReadWall(string user);


    }
}