using System;
using System.Linq;

namespace CSharp.Core
{
    public class ConsoleDisplay : Display
    {
        private readonly PostTsFormatter _formatter;
        private readonly ITextConsole _console;

        public ConsoleDisplay(PostTsFormatter formatter, ITextConsole console)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (console == null) throw new ArgumentNullException(nameof(console));
            _formatter = formatter;
            _console = console;
        }

        public void Show(WallView wall)
        {            
            wall.Posts.ToList()
                .ForEach(p =>
                {                    
                    var niceTs = _formatter.NiceTs(TimeService.Now(), p.TimeStamp);
                    var line = string.Format("{0} ({1})", p.Content, niceTs);
                    _console.PrintLine(line);
//                    _console.PrintLine($"{p.Content} ({niceTs})");
                });
        }
    }
}