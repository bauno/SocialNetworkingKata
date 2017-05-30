using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Core
{
    public class Wall : Dto<WallDto, Wall>
    {
        private readonly string _user;
        private IEnumerable<Post> _posts;

        public Wall(string user)
        {
            if (string.IsNullOrEmpty(user))
                throw new ArgumentNullException(nameof(user));
            _user = user;
            _posts = new List<Post>();
        }
        

        WallDto Dto<WallDto, Wall>.ToDto()
        {
            return new WallDto
            {
                User = _user,
                Posts = _posts.Select(p => new PostDto {Content = p.Content})
            };
        }

        void Dto<WallDto, Wall>.Load(WallDto dto)
        {
            _posts = dto.Posts.Select(p => new Post {Content = p.Content});
        }
    }
}