using System.Collections.Generic;

namespace CSharp.Core
{
    public interface Display
    {
        void Show(WallView wall);
        void Show(IEnumerable<WallView> walls);
    }
}