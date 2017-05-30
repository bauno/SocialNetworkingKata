using System.Collections.Generic;

namespace CSharp.Core
{
    public class WallDto
    {
        public string User { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
    }
}