using System.Collections;
using System.Collections.Generic;

namespace CSharp.Core
{
    public interface Command
    {
        IEnumerable<Post> SendTo(SocialNetwork socialNetwork);
    }
}