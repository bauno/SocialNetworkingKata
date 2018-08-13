using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Services.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Commands
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

        public Option<Displayable> SendTo(ISocialNetwork socialNetwork)
        {
            socialNetwork.Post(_user, _message);
            return null;
        }
        
    }
}