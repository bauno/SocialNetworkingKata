using System.Collections.Generic;

namespace CSharp.Core
{
    public class WallView
    {
        public WallView()
        {
            Posts = new List<PostView>();
            Follows = new List<string>();
        }
        public string User { get; set; }
        public IEnumerable<PostView> Posts { get; set; }
        public IEnumerable<string> Follows { get; set; }
        
    }
}