using System;
using System.Collections.Generic;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Commands
{
    public class WallCommand : Command
    {
        private readonly string _user;
        private List<WallView> _walls;


        public override string ToString()
        {
            return $"Type: Wall; User: {_user}";
        }

        public WallCommand(string user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _user = user;
            _walls = new List<WallView>();
            
        }

        public Command SendTo(SocialNetwork socialNetwork)
        {
            var userWall = socialNetwork.ReadWall(_user);
            _walls.Add(userWall);
            foreach (var user in userWall.Follows)
            {
                var wall = socialNetwork.ReadWall(user);
                _walls.Add(wall);
            }
            return this;
        }

        public void ShowOn(Display display)
        {
            display.Show(_walls);
        }
    }
}