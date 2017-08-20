using CSharp.Core.Services.Interfaces;
using LanguageExt;

namespace CSharp.Core.Commands.Interfaces
{
    class InvalidCommand : Command
    {
        public override CommandType Type => CommandType.Invalid;
        
        public override Option<Displayable> SendTo(SocialNetwork socialNetwork)
        {
            throw new System.InvalidOperationException("Error: trying to execute an InvalidCommand ");
        }
    }
}