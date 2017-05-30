using System.Collections;
using System.Collections.Generic;

namespace CSharp.Core
{
    public class PostCommand : Command
    {
        private readonly string _user;
        private readonly string _message;

        public PostCommand(string user, string message)
        {
            _user = user;
            _message = message;
        }
        
        public override string ToString()
        {
            return $"Type: Post; User: {_user}; Post: {_message}";
        }

        public IEnumerable<Post> SendTo(SocialNetwork socialNetwork)
        {
            socialNetwork.Post(_user, _message);
            return new List<Post>();

        }
    }
}