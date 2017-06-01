﻿using System;

namespace CSharp.Core
{
    public class FollowCommand : Command
    {
        private readonly string _user;
        private readonly string _who;

        public FollowCommand(string user, string who)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (who == null) throw new ArgumentNullException(nameof(who));
            _user = user;
            _who = who;
        }

        public override string ToString()
        {
            return $"Type: Follow; User: {_user}; Who: {_who}";
        }

        public Command SendTo(SocialNetwork socialNetwork)
        {
            socialNetwork.Follow(_user, _who);
            return this;
        }

        public void ShowOn(Display display)
        {
            
        }
    }
}