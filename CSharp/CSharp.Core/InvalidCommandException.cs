using System;

namespace CSharp.Core
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string cmdString) : base($"The command \"{cmdString}\" is invalid")
        {
        }
    }
}