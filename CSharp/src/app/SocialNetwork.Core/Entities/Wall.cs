using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities.Interfaces;
using SocialNetwork.Core.Repositories.Interfaces;
using SocialNetwork.Core.Values;

namespace SocialNetwork.Core.Entities
{
    public class Wall : Dto<WallDto, Wall>, IWall
    {
        private readonly string _user;
        private IList<Post> _posts;
        private IList<string> _follows;

        public Wall(string user)
        {
            if (string.IsNullOrEmpty(user))
                throw new ArgumentNullException(nameof(user));
            _user = user;
            _posts = new List<Post>();
            _follows = new List<string>();
        }
        
        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public void Follow(string whoToFollow)
        {
            _follows.Add(whoToFollow);
        }

        public void SendMessage(UserMessage message)
        {
            throw new NotImplementedException();
        }


        WallDto Dto<WallDto, Wall>.ToDto()
        {
            return new WallDto
            {
                User = _user,
                Posts = _posts.Select(p => new PostDto {Content = p.Content, TimeStamp = p.TimeStamp}),
                Follows = _follows
            };
        }

        void Dto<WallDto, Wall>.Load(WallDto dto)
        {
            _posts = dto.Posts.Select(p => new Post(p.Content, p.TimeStamp))
                        .ToList();
            _follows = dto.Follows.ToList();
        }

        
    }
}