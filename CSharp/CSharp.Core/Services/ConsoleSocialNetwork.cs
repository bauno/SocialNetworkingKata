using System.IO;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services.Interfaces;
using LanguageExt;
using static LanguageExt.Prelude;

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

        public Option<string> Enter(string cmdString)
        {
            var x = _parser.Parse(cmdString);
            var y = x.Map(cmd => cmd.SendTo(_engine));
            var z = y.Bind<Unit>(option => option.IfSome(d => d.ShowOn(_display)));


            var w = z.Match(u => None, Some);
            return w;
        }
        
    }
}