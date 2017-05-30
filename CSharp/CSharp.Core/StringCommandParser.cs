using System.Text.RegularExpressions;
using System.Threading;

namespace CSharp.Core
{
    public class StringCommandParser : CommandParser
    {
        private Regex _postCmd = new Regex(@"^(.*) -> (.*)$");
        private Regex _readCmd = new Regex(@"^(\w+)$");
        
        public Command Parse(string cmdString)
        {
            if (_postCmd.IsMatch(cmdString))
            {
                var matches = _postCmd.Matches(cmdString);
                var user = matches[0].Groups[1].Value;
                var message = matches[0].Groups[2].Value;
                return new PostCommand(user, message);
            }
            if (_readCmd.IsMatch(cmdString))
            {
                var matches = _readCmd.Matches(cmdString);
                var user = matches[0].Groups[1].Value;
                return new ReadCommand(user);
            }
            throw new InvalidCommandException(cmdString);
        }
    }
}