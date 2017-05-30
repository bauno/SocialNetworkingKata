using System.Collections.Generic;

namespace CSharp.Core
{
    public interface SocialNetwork
    {        
        void Post(string user, string message);
        IEnumerable<Post> ReadWall(string user);
    }
}