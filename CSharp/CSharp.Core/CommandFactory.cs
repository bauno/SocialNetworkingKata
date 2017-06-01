namespace CSharp.Core
{
    public interface CommandFactory
    {
        Command Parse(string cmdString);
    }
}