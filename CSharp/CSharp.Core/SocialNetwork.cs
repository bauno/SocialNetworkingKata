using System.Collections.Generic;

namespace CSharp.Core
{
    public interface SocialNetwork
    {        
        void Post(string user, string message);
//        IEnumerable<PostView> ReadWall(string user);
        WallView ReadWall(string user);
    }
}