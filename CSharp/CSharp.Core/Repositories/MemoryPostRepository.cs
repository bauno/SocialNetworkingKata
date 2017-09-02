using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core.Dtos;
using CSharp.Core.Entities;
using CSharp.Core.Entities.Interfaces;
using CSharp.Core.Repositories.Interfaces;
using CSharp.Core.Values;
using CSharp.Core.Views;
using LanguageExt;

namespace CSharp.Core.Repositories
{
    public class MemoryPostRepository : PostRepository
    {
        private readonly Dictionary<string, WallDto> _walls;

        public MemoryPostRepository(Dictionary<string, WallDto> walls)
        {
            if (walls == null) throw new ArgumentNullException(nameof(walls));
            _walls = walls;
        }

        public MemoryPostRepository() : this(new Dictionary<string, WallDto>())
        {
            
        }

        public Unit Save(IWall wall)
        {
            var dto = ((Dto<WallDto, Wall>) wall).ToDto();
            if (_walls.ContainsKey(dto.User))
            {
                _walls[dto.User] = dto;
            }else _walls.Add(dto.User, dto);
            return Unit.Default;
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
            if (_walls.ContainsKey(user))
            {
                return new WallView
                {
                    User = user,
                    Posts = _walls[user].Posts.Select(p => new PostView {Content = p.Content, TimeStamp = p.TimeStamp}),
                    Follows = _walls[user].Follows
                };
            }
            return new WallView();

        }
    }
}