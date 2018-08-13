using System;
using System.Collections.Generic;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Factories;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Factories.Interfaces;
using SocialNetwork.Core.Services.Interfaces;
using Xunit;

namespace SocialNetwork.Tests.Acceptance
{
    public partial class SocialNetworkTests    
    {
        private ConsoleSocialNetwork _socialNetwork;
		private FakeConsole _console;
		private int _count = 0;
		private DateTime _now;

        public SocialNetworkTests()
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

        private void Follows(string user, string who)
		{
			var cmdStr = $"{user} follows {who}";
			_socialNetwork.Enter(cmdStr);
        }

        private void WhenSomeoneEnters(string cmd)
		{
			var cmdStr = cmd;
			_socialNetwork.Enter(cmdStr);

        }
    }
}