using System.Text.RegularExpressions;

namespace CSharp.Core
{
    public class ReadCommandFactory : CommandFactory
    {
        private Regex _readCmd = new Regex(@"^(\w+)$");
        public Command Parse(string cmdString)
        {
            if (_readCmd.IsMatch(cmdString))
            {
                var matches = _readCmd.Matches(cmdString);
                var user = matches[0].Groups[1].Value;
                return new ReadCommand(user);
            }
            return null;
        }
    }
}