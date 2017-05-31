﻿using System;
using System.CodeDom;
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
            _console.PrintLine(wall.User);
            wall.Posts.ToList()
                .ForEach(p => _console.PrintLine($"{p.Content} ({_formatter.NiceTs(TimeService.Now(), p.TimeStamp)})"));
        }
    }
}