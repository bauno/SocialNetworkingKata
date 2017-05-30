namespace CSharp.Core
{
    public interface ISocial
    {
        string Read(string user);
        void Post(string user, string message);
    }
}