using System;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Services
{
    public class ConsoleSocialNetwork : IConsoleSocialNetwork
    {
        private readonly CommandParser _parser;
        private readonly SocialNetwork _engine;
        private readonly Display _display;

        public ConsoleSocialNetwork(CommandParser parser, SocialNetwork engine, Display display)
        {            
            if (parser == null) throw new ArgumentNullException(nameof(parser));
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            if (display == null) throw new ArgumentNullException(nameof(display));

            _parser = parser;
            _engine = engine;
            _display = display;
        }

        public void Enter(string cmdString)
        {
            Action<Message, SocialNetwork> sendMessage = (m, s) => ((Command) m).SendTo(s);
            
            var message = _parser.Parse(cmdString);
            if (message.Type == MessageType.Command)
                ((Command) message).SendTo(_engine);
            else
            {
                var res = ((Query) message).Exec(_engine);
                _display.Show(res);
            }
        }
        
    }
}