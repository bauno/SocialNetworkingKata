using System.Collections.Generic;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Commands
{
    public class ReadCommand : Query
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

        public IEnumerable<WallView> Exec(SocialNetwork socialNetwork)
        {
            return new[] {socialNetwork.ReadWall(_user)};
        }

        public MessageType Type => MessageType.Query;
    }
}