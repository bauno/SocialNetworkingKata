using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Services.Interfaces;
using SocialNetwork.Core.Views;
using LanguageExt;

namespace SocialNetwork.Core.Commands
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
            _user = user;
            
        }

        public Option<Displayable> SendTo(ISocialNetwork socialNetwork)
        {
            var walls = new List<WallView>();
            var userWall = socialNetwork.ReadWall(_user);
            walls.Add(userWall);
            walls.AddRange(userWall.Follows.Select(socialNetwork.ReadWall));
            return new WallsDisplay(walls);
        }
    }
}