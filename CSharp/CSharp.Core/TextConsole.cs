using System;

namespace CSharp.Core
{
    public class TextConsole : ITextConsole
    {
        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}