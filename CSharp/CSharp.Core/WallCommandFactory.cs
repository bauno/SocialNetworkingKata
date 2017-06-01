using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace CSharp.Core
{
    public class WallCommandFactory : CommandFactory
    {
        private readonly Regex _wallRegex = new Regex(@"^(.*) wall$");
           

        public Command Parse(string cmdString)
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