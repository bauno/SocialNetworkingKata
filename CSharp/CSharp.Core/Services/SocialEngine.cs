using System;
using System.Runtime.Remoting.Messaging;
using CSharp.Core.Repositories.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Values;
using CSharp.Core.Views;
using LanguageExt;

namespace CSharp.Core.Services
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
            wall.AddPost(new Post(message, TimeService.Now()));
            _repository.Save(wall);
            
        }

        public Unit Follow(string user, string whoToFollow)
        {
            var wall = _repository.LoadOrCreateWallOf(user);
            wall.Follow(whoToFollow);
            return _repository.Save(wall)
                .Apply(u => Unit.Default);            
        }

        public WallView ReadWall(string user)
        {
            return _repository.ReadWallOf(user);
        }

        public void SendMessage(string sender, string to, string message)
        {
            var wall = _repository.LoadOrCreateWallOf(to);
            wall.SendMessage(new UserMessage{From = sender, Message = message});
            _repository.Save(wall);
        }
    }
}