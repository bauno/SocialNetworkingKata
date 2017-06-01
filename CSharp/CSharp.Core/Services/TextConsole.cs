using System;
using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Services
{
    public class TextConsole : ITextConsole
    {
        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}