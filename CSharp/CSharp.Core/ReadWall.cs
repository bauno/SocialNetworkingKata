using System.Collections.Generic;

namespace CSharp.Core
{
    public class ReadWall
    {
        public string User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}