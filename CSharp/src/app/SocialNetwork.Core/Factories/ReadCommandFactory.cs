using System.Text.RegularExpressions;
using SocialNetwork.Core.Commands;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Factories.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Factories
{
    public class ReadCommandFactory : CommandFactory
    {
        private readonly Regex _readCmd = new Regex(@"^(\w+)$");
        public Option<Command> Parse(string cmdString)
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