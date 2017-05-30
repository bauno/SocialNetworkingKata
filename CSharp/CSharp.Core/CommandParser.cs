namespace CSharp.Core
{
    public interface CommandParser
    {
        Command Parse(string cmdString);
    }
}