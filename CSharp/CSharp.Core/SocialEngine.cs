using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace CSharp.Core
{
    public class SocialEngine : SocialNetwork
    {
        private readonly PostRepository _repository;

        public SocialEngine(PostRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _repository = repository;
            
        }

        public void Post(string user, string message)
        {
            var wall = _repository.LoadOrCreateWallOf(user);
            wall.AddPost(new Post(message));
            _repository.Save(wall);
            
        }

        public IEnumerable<PostView> ReadWall(string user)
        {            
            return _repository.ReadWallOf(user).Posts;
        }
    }
}