using System;

namespace CSharp.Core
{
    public class WallCommand : Command
    {
        private readonly string _user;


        public override string ToString()
        {
            return $"Type: Wall; User: {_user}";
        }

        public WallCommand(string user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _user = user;
            
        }

        public Command SendTo(SocialNetwork socialNetwork)
        {
            throw new NotImplementedException();
        }

        public void ShowOn(Display display)
        {
            throw new NotImplementedException();
        }
    }
}