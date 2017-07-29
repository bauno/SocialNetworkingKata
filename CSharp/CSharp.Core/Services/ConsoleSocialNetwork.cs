using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;

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

        public Result Enter(string cmdString)
        {
            return _parser.Parse(cmdString)
                .ToResult($"Cannot parse command: {cmdString}")
                .OnSuccess(cmd => cmd.SendTo(_engine).ShowOn(_display));
        }
        
    }
}