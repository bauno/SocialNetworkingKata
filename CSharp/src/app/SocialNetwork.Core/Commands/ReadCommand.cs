using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using LanguageExt;

namespace CSharp.Core.Commands
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

        public Option<Displayable> SendTo(SocialNetwork socialNetwork)
        {
            return new WallDisplay(socialNetwork.ReadWall(_user));
        }
    }
}