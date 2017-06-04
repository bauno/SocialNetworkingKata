using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Commands
{
    public class PostCommand : Command
    {
        private readonly string _user;
        private readonly string _message;

        public PostCommand(string user, string message)
        {
            _user = user;
            _message = message;
        }
        
        public override string ToString()
        {
            return $"Type: Post; User: {_user}; Post: {_message}";
        }

        public void SendTo(SocialNetwork socialNetwork)
        {
            socialNetwork.Post(_user, _message);            
        }

        public void ShowOn(Display display)
        {
            
        }

        public MessageType Type => MessageType.Command;
    }
}