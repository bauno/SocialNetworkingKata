using System;
using System.Collections.Generic;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Commands
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

        public Displayable SendTo(SocialNetwork socialNetwork)
        {
            var walls = new List<WallView>();
            var userWall = socialNetwork.ReadWall(_user);
            walls.Add(userWall);
            foreach (var user in userWall.Follows)
            {
                var wall = socialNetwork.ReadWall(user);
                walls.Add(wall);
            }
            return new WallsDisplay(walls);
        }
    }
}