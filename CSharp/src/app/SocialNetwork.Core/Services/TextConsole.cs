using System;
using SocialNetwork.Core.Services.Interfaces;

namespace SocialNetwork.Core.Services
{
    public class TextConsole : ITextConsole
    {
        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}