using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Services
{
    public class ConsoleDisplay : Display
    {
        private readonly PostTsFormatter _formatter;
        private readonly ITextConsole _console;

        public ConsoleDisplay(PostTsFormatter formatter, ITextConsole console)
        {            
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
                    select new {wall.User, post.Content, post.TimeStamp};

                foreach (var post in posts)
                {
                    _console.PrintLine(
                        $"{post.User} - {post.Content} ({_formatter.NiceTs(TimeService.Now(), post.TimeStamp)})");
                }            
        }
    }
}