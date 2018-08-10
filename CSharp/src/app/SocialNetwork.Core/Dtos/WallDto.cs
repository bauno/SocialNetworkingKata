using System.Collections.Generic;

namespace CSharp.Core.Dtos
{
    public class WallDto
    {
        public string User { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
        public IEnumerable<string> Follows { get; set; }
    }
}