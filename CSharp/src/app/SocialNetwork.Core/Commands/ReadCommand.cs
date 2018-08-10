using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Services.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Commands
{
    public class ReadCommand : Command
    {
        private readonly string _user;

        public ReadCommand(string user)
        {
            _user = user;
        }

        public override string ToString()
        {
            return $"Type: Read; User: {_user}";
        }

        public Option<Displayable> SendTo(ISocialNetwork socialNetwork)
        {
            return new WallDisplay(socialNetwork.ReadWall(_user));
        }
    }
}