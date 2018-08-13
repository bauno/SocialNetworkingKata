using System;

namespace CSharp.Core.Values
{
    public class Post
    {
        public Post(string content, DateTime timeStamp)
        {
            Content = content;
            TimeStamp = timeStamp;
        }

        public string Content { get; }
        public DateTime TimeStamp { get; }
    }
}