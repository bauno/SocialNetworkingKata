using System;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Services.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Commands
{
    public class FollowCommand : Command
    {
        private readonly string _user;
        private readonly string _who;

        public FollowCommand(string user, string who)
        {
            _user = user;
            _who = who;
        }

        public override string ToString()
        {
            return $"Type: Follow; User: {_user}; Who: {_who}";
        }

        public Option<Displayable> SendTo(ISocialNetwork socialNetwork)
        {
            socialNetwork.Follow(_user, _who);
            return null;
        }
       
    }
}