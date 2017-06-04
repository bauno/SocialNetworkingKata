using System.Collections.Generic;
using CSharp.Core.Views;

namespace CSharp.Core.Services.Interfaces
{
    public interface Display
    {        
        void Show(IEnumerable<WallView> walls);
        void Show(WallView wall);
    }
}