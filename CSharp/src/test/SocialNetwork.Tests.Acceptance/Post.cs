using Xbehave;
using System;
using Xunit;
using SocialNetwork.Core.Services;

namespace SocialNetwork.Tests.Acceptance
{
    public class AliceCanPost
    {

        private ConsoleSocialNetwork _socialNetwork;
		private FakeConsole _console;
		private int _count = 0;
		private DateTime _now;

        public AliceCanPost()
        {
            _now = DateTime.Now;
			var readCommandFactory = new ReadCommandFactory();
			var postCommandFactory = new PostCommandFactory();
			var wallCommandFactory = new WallCommandFactory();
			var followCommandFactory = new FollowCommandFactory();
			var repository = new MemoryPostRepository();
			var engine = new SocialEngine(repository);
			var parser = new StringCommandParser(new CommandFactory[]
				{readCommandFactory, postCommandFactory, wallCommandFactory, followCommandFactory});
			var formatter = new PostTsStringFormatter();
			_console = new FakeConsole();
			var display = new ConsoleDisplay(formatter, _console);
			_socialNetwork = new ConsoleSocialNetwork(parser, engine, display);            
        }

        [Scenario]
        public void Can_Read_Alice_Posts(){
            "Given Alice has posted 'I love the weather today' to her wall 5 minutes ago"
            .x(() => {
                //TimeService.TestNow = unit == "seconds" ? _now.AddSeconds(-delta) : _now.AddMinutes(-delta);
                TimeService.TestNow = _now.AddMinutes(-5);
			    var cmdStr = $"Alice -> I love the weather today";			
			    var res = _socialNetwork.Enter(cmdStr);
			    TimeService.TestNow = _now;
            });            

            "When someone enters the command 'Alice'"
            .x(() => _socialNetwork.Enter("Alice"));

            "Then he can read 'I love the weather today (5 minutes ago)'"
            .x(() => {
                Assert.AreEqual(message, _console.Display[_count]);
                _count++;
            });
        }
    }
}