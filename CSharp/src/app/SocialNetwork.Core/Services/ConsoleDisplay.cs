using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Core.Services.Interfaces;
using SocialNetwork.Core.Views;

namespace SocialNetwork.Core.Services
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
            wall.Posts
                .OrderByDescending(p => p.TimeStamp)
                .ToList()
                .ForEach(p => _console.PrintLine($"{p.Content} ({_formatter.NiceTs(TimeService.Now(), p.TimeStamp)})"));
        }

        public void Show(IEnumerable<WallView> walls)
        {
            walls.SelectMany(wall => wall.Posts, (w, p) => new {w.User, p.Content, p.TimeStamp})
                .OrderByDescending(post => post.TimeStamp)
                .ToList().ForEach(post => _console.PrintLine(
                    $"{post.User} - {post.Content} ({_formatter.NiceTs(TimeService.Now(), post.TimeStamp)})"));                        
        }        
    }
}