using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;

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

        public Displayable SendTo(SocialNetwork socialNetwork)
        {
            socialNetwork.SendMessage(_from, _to, _message);
            return new Nothing();
        }
    }
}