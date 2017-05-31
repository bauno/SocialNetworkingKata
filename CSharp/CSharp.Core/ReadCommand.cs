using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp.Core
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

        public IEnumerable<PostView> SendTo(SocialNetwork socialNetwork)
        {
            return socialNetwork.ReadWall(_user);
        }
    }
}