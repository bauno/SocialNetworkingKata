using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Core
{
    public class Wall : Dto<WallDto, Wall>, IWall
    {
        private readonly string _user;
        private IList<Post> _posts;

        public Wall(string user)
        {
            if (string.IsNullOrEmpty(user))
                throw new ArgumentNullException(nameof(user));
            _user = user;
            _posts = new List<Post>();
        }
        
        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public void Follow(string whoToFollow)
        {
            throw new NotImplementedException();
        }


        WallDto Dto<WallDto, Wall>.ToDto()
        {
            return new WallDto
            {
                User = _user,
                Posts = _posts.Select(p => new PostDto {Content = p.Content, TimeStamp = p.TimeStamp})
            };
        }

        void Dto<WallDto, Wall>.Load(WallDto dto)
        {
            _posts = dto.Posts.Select(p => new Post(p.Content, p.TimeStamp))
                        .ToList();
        }

        
    }
}