namespace CSharp.Core
{
    public class Post
    {
        public Post(string content)
        {
            Content = content;
        }

        public string User { get; }
        public string Content { get; }
    }
}