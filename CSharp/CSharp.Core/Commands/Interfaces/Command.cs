using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using LanguageExt;

namespace CSharp.Core.Commands.Interfaces
{
    public enum CommandType
    {
        Invalid, Other        
    }
    
    public abstract class Command
    {
        public virtual CommandType Type => CommandType.Other;

        public abstract Option<Displayable> SendTo(SocialNetwork socialNetwork);
    }

    class InvalidCommand : Command
    {
        public override CommandType Type => CommandType.Invalid;
        
        public override Option<Displayable> SendTo(SocialNetwork socialNetwork)
        {
            throw new System.InvalidOperationException();
        }
    }
}