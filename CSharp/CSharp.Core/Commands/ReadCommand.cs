using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Commands
{
    public class ReadCommand : Command
    {
        private readonly string _user;
        private WallView _wall;

        public ReadCommand(string user)
        {
            _user = user;
        }

        public override string ToString()
        {
            return $"Type: Read; User: {_user}";
        }

        public Command SendTo(SocialNetwork socialNetwork)
        {
            _wall = socialNetwork.ReadWall(_user);
            return this;
        }

        public void ShowOn(Display display)
        {
            display.Show(_wall);
        }
    }
}