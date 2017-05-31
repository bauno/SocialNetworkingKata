using System;

namespace CSharp.Core
{
    public class ConsoleDisplay : Display
    {
        public void Show(string line)
        {
            Console.WriteLine(line);
        }

        public void Show(WallView wall)
        {
            throw new NotImplementedException();
        }
    }
}