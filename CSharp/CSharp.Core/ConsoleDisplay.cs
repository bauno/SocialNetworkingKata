using System;
using System.Collections.Generic;
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
            wall.Posts                
                .OrderByDescending(p => p.TimeStamp)
                .ToList()
                .ForEach(p => _console.PrintLine($"{p.Content} ({_formatter.NiceTs(TimeService.Now(), p.TimeStamp)})"));
        }

        public void Show(IEnumerable<WallView> walls)
        {
            var posts = from wall in walls
                from post in wall.Posts
                orderby post.TimeStamp descending 
                select new {wall.User, post.Content, post.TimeStamp };

            foreach (var post in posts)
            {
                _console.PrintLine(
                    $"{post.User} - {post.Content} ({_formatter.NiceTs(TimeService.Now(), post.TimeStamp)})");
            }                                               
        }
    }
}