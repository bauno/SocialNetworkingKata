using System.Collections.Generic;

namespace SocialNetwork.Core.Dtos
{
    public class WallDto
    {
        public string User { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
        public IEnumerable<string> Follows { get; set; }
    }
}