namespace CSharp.Core
{
    public interface SocialNetwork
    {        
        void Post(string user, string message);
        Post ReadWall(string user);
    }
}