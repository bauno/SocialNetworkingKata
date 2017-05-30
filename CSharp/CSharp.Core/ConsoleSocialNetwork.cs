using System;

namespace CSharp.Core
{
    public class ConsoleSocialNetwork
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
            var res = _parser.Parse(cmdString)
                    .SendTo(_engine);

            foreach (var post in res)
            {
                _display.Show(post.Content);                
            }
        }
    }
}