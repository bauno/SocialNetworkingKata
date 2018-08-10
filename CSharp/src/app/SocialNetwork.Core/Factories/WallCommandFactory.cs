using System.Text.RegularExpressions;
using SocialNetwork.Core.Commands;
using SocialNetwork.Core.Commands.Interfaces;
using SocialNetwork.Core.Factories.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Factories
{
    public class WallCommandFactory : CommandFactory
    {
        private readonly Regex _wallRegex = new Regex(@"^(.*) wall$");
           

        public Option<Command> Parse(string cmdString)
        {
            if (_wallRegex.IsMatch(cmdString))
            {
                var matches = _wallRegex.Matches(cmdString);
                var user = matches[0].Groups[1].Value;                
                return new WallCommand(user);
            }
            return null;
        }
    }
}