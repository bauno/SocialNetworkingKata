using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services.Interfaces;
using LanguageExt;

namespace CSharp.Core.Commands
{
    public class MesssageCommand : Command
    {
        private readonly string _from;
        private readonly string _to;
        private readonly string _message;

        public MesssageCommand(string sender, string to, string message)
        {
            _from = sender;
            _to = to;
            _message = message;
        }

        public Option<Displayable> SendTo(SocialNetwork socialNetwork)
        {
            socialNetwork.SendMessage(_from, _to, _message);
            return null;
        }
    }
}