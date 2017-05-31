using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CSharp.Core
{
    public class MemoryPostRepository : PostRepository
    {
        private readonly Dictionary<string, Post> _data;
        private readonly Dictionary<string, WallDto> _walls;

        public MemoryPostRepository(Dictionary<string, Post> data, Dictionary<string, WallDto> walls)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (walls == null) throw new ArgumentNullException(nameof(walls));
            _data = data;
            _walls = walls;
        }

        public MemoryPostRepository() : this(new Dictionary<string, Post>(), new Dictionary<string, WallDto>())
        {
            
        }

        public void Save(IWall wall)
        {
            var dto = ((Dto<WallDto, Wall>) wall).ToDto();
            if (_walls.ContainsKey(dto.User))
            {
                _walls[dto.User] = dto;
            }else _walls.Add(dto.User, dto);
        }

        public IWall LoadOrCreateWallOf(string user)
        {
            if (_walls.ContainsKey(user))
            {
                var wall = (Dto<WallDto, Wall>) new Wall(user);
                wall.Load(_walls[user]);
                return (IWall)wall;                
            }
            return new Wall(user);
            
        }

        public WallView ReadWallOf(string user)
        {
            return new WallView
            {
                User = user,
                Posts = _walls[user].Posts.Select(p => new PostView {Content = p.Content, TimeStamp = p.TimeStamp})
            };
        }
    }
}