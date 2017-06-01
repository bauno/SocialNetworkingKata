using System.Collections.Generic;
using CSharp.Core.Views;

namespace CSharp.Core.Services.Interfaces
{
    public interface Display
    {
        void Show(WallView wall);
        void Show(IEnumerable<WallView> walls);
    }
}