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
            var res = _parser.Parse(cmdString);

            var x = res.Map(command => command.SendTo(_engine));
            var y = x.IfRight(option => option.Some(o => o.ShowOn(_display)));
            
            var res1 = res.Bind(command => command.SendTo(_engine));
            var b = res1.IfRight(d => d.ShowOn(_display));

            var rs = from command in _parser.Parse(cmdString)
                     from d in command.SendTo(_engine) 
                select display.ShowOn(_display);
                
            
            
            var res2 = b.Bind(disp => disp.ShowOn(_display));
//                .Bind(cmd => cmd.SendTo(_engine));
//                .Bind(d => d.ShowOn(_display));



        }
        
    }
}