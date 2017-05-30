namespace CSharp.Core
{
    public interface PostRepository
    {
        void Save(Post post);
        Post ReadPostFrom(string pippo);
    }
}