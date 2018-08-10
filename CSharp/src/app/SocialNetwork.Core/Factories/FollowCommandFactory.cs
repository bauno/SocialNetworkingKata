using System.Text.RegularExpressions;
using SocialNetwork.Core.Commands;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Factories.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Factories
{
    public class FollowCommandFactory : CommandFactory
    {
        private readonly Regex _followRegex = new Regex(@"^(\w+) follows (\w+)$");
        
        public Option<Command> Parse(string cmdString)
        {
            if (_followRegex.IsMatch(cmdString))
            {
                var matches = _followRegex.Matches(cmdString);
                var user = matches[0].Groups[1].Value;
                var who = matches[0].Groups[2].Value;
                return new FollowCommand(user, who);
            }
            return null;
        }
    }
}