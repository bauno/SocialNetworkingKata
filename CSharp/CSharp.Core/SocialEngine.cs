﻿using System;

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
            _repository.Save(new Post{User = user, Content = message});
        }

        public Post ReadWall(string user)
        {
            return _repository.ReadPostFrom(user);
        }
    }
}