using System;
using System.Collections.Generic;

namespace CSharp.Core
{
    public class MemoryPostRepository : PostRepository
    {
        private readonly Dictionary<string, Post> _data;

        public MemoryPostRepository(Dictionary<string, Post> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            _data = data;
            
        }

        public void Save(Post post)
        {
            _data.Add(post.User, post);
        }
    }
}