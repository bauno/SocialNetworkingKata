using System.Linq;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Entities.Interfaces;
using SocialNetwork.Core.Repositories.Interfaces;
using SocialNetwork.Core.Views;
using LanguageExt;

namespace SocialNetwork.Core.Repositories
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