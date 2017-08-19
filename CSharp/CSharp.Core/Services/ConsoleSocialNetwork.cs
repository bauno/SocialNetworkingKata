using System.Runtime.InteropServices;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using Optional;

namespace CSharp.Core.Services
{
    public class ConsoleSocialNetwork : IConsoleSocialNetwork
    {
        private readonly CommandParser _parser;
        private readonly SocialNetwork _engine;
        private readonly Display _display;

        public ConsoleSocialNetwork(CommandParser parser, SocialNetwork engine, Display display)
        {
            _parser = parser;
            _engine = engine;
            _display = display;
        }

        public void Enter(string cmdString)
        {
            var res = _parser.Parse(cmdString);
            var m = res.Match(c => Option.Some<Option<Displayable>, string>(c.SendTo(_engine)), 
                err => Option.None<Option<Displayable>,string>(err));
            var res2 = m.Match(d => d.MatchSome(displayable => displayable.ShowOn(_display)),
                err => Option.None<string>());









        }
        
    }
}