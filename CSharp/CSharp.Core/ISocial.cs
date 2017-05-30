namespace CSharp.Core
{
    public interface ISocial
    {        
        void Post(string user, string message);
        Post ReadWall(string user);
    }
}