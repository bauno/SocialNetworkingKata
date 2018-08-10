using Xbehave;
using System;
using Xunit;
using SocialNetwork.Core.Services;
using System.Collections.Generic;
using SocialNetwork.Core.Factories;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Factories.Interfaces;
using SocialNetwork.Core.Services.Interfaces;

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

        private void PostToWall(string user, string message, int delta, string minutesOrSeconds){
            TimeService.TestNow = minutesOrSeconds == "seconds" ? _now.AddSeconds(-delta) : _now.AddMinutes(-delta);
			var cmdStr = $"{user} -> {message}";			
			var res = _socialNetwork.Enter(cmdStr);
            TimeService.TestNow = _now;
        }

        private void CheckMessage(string message){
            Assert.Equal(message, _console.Display[_count]);
            _count++;
        }

        [Scenario]
        public void Can_Read_Alice_Posts(){
            "Given Alice has posted 'I love the weather today' to her wall 5 minutes ago"
            .x(() => {
                PostToWall("Alice", "I love the weather today", 5, "minutes");                
            });            

            "When someone enters the command 'Alice'"
            .x(() => _socialNetwork.Enter("Alice"));

            "Then he can read 'I love the weather today (5 minutes ago)'"
            .x(() => CheckMessage("I love the weather today (5 minutes ago)"));
        }

        [Scenario]
        public void Can_Read_Bob_Posts(){
            "Given Bob posted 'Damn! We lost' to his wall 2 minutes ago"
            .x(() => PostToWall("Bob", "Damn! We lost!", 2, "minutes"));

            "And Bob posted 'Good game though' to his wall 1 minute ago"
            .x(() => PostToWall("Bob", "Good game though", 1, "minute"));

            "When someone enters the command 'Bob'"
            .x(() => _socialNetwork.Enter("Bob"));

            "Then he can read 'Good game though (1 minutes ago)'"
            .x(() => CheckMessage("Good game though (1 minutes ago)"));

            "And he can read 'Damn! We lost! (2 minutes ago)'"
            .x(() => CheckMessage("Damn! We lost! (2 minutes ago)"));

        }
    }
}