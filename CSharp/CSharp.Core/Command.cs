using System.Collections;
using System.Collections.Generic;

namespace CSharp.Core
{
    public interface Command
    {
        Command SendTo(SocialNetwork socialNetwork);
        void ShowOn(Display display);
    }
}