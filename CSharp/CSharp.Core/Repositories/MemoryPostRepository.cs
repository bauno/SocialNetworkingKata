using System.Linq;
using CSharp.Core.Dtos;
using CSharp.Core.Entities;
using CSharp.Core.Entities.Interfaces;
using CSharp.Core.Repositories.Interfaces;
using CSharp.Core.Views;
using LanguageExt;

namespace CSharp.Core.Repositories
{
    public class MemoryPostRepository : PostRepository
    {
        private Map<string, WallDto> _walls;

        public MemoryPostRepository(Map<string, WallDto> walls)
        {
            _walls = walls;
        }

        public MemoryPostRepository() : this(Map<string, WallDto>.Empty)
        {
            
        }

        public Unit Save(IWall wall)
        {
            var dto = ((Dto<WallDto, Wall>) wall).ToDto();
            _walls = _walls.AddOrUpdate(dto.User, dto);            
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