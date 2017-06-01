using System.Collections.Generic;

namespace CSharp.Core
{
    public class WallView
    {
        public string User { get; set; }
        public IEnumerable<PostView> Posts { get; set; }
        public IEnumerable<string> Follows { get; set; }
        
    }
}