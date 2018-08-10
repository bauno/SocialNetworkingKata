using System.Text.RegularExpressions;
using SocialNetwork.Core.Commands;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Factories.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Factories
{
    public class PostCommandFactory : CommandFactory
    {
        private readonly Regex _postCmd = new Regex(@"^(.*) -> (.*)$");
        
        public Option<Command> Parse(string cmdString)
        {
            if (_postCmd.IsMatch(cmdString))
            {
                var matches = _postCmd.Matches(cmdString);
                var user = matches[0].Groups[1].Value;
                var message = matches[0].Groups[2].Value;
                return new PostCommand(user, message);
            }
            return null;
        }
    }
}