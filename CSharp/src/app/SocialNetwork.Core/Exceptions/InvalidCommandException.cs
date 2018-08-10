using System;

namespace SocialNetwork.Core.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string cmdString) : base($"The command \"{cmdString}\" is invalid")
        {
        }
    }
}