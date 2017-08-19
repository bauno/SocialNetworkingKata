using System.IO;
using System.Net.NetworkInformation;
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
            return _parser.Parse(cmdString)
                .Map(cmd => cmd.SendTo(_engine))
                .Bind<Unit>(option => option.IfSome(d => d.ShowOn(_display)))
                .Match(u => None, Some);
        }
        
    }
}