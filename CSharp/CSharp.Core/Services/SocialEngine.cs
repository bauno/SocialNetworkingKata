using System;
using CSharp.Core.Repositories.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Values;
using CSharp.Core.Views;

namespace CSharp.Core.Services
{
    public class SocialEngine : SocialNetwork
    {
        private readonly PostRepository _repository;

        public SocialEngine(PostRepository repository)
        {            
            _repository = repository;
            
        }

        public void Post(string user, string message)
        {
            var wall = _repository.LoadOrCreateWallOf(user);
            wall.AddPost(new Post(message, TimeService.Now()));
            _repository.Save(wall);
            
        }

        public void Follow(string user, string whoToFollow)
        {
            var wall = _repository.LoadOrCreateWallOf(user);
            wall.Follow(whoToFollow);
            _repository.Save(wall);
        }

        public WallView ReadWall(string user)
        {
            return _repository.ReadWallOf(user);
        }

        public void SendMessage(string sender, string to, string message)
        {
            throw new NotImplementedException();
        }
    }
}