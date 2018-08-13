 using System.Collections.Generic;
using SocialNetwork.Core.Views;

namespace SocialNetwork.Core.Services.Interfaces
{
    public interface Display
    {        
        void Show(IEnumerable<WallView> walls);
        void Show(WallView wall);
    }
}