using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;
using CSharpFunctionalExtensions;
using LanguageExt;

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
            _user = user;
            
        }

        public override Option<Displayable> SendTo(SocialNetwork socialNetwork)
        {
            var walls = new List<WallView>();
            var userWall = socialNetwork.ReadWall(_user);
            walls.Add(userWall);
            walls.AddRange(userWall.Follows.Select(socialNetwork.ReadWall));
            return new WallsDisplay(walls);
        }
    }
}