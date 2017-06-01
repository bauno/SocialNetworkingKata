using System;
using System.Collections.Generic;
using CSharp.Core;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CSharp.Tests.Acceptance
{
	[Binding]
	public class SocialNetworkTests
	{
		private class FakeConsole : ITextConsole
		{
			public List<string> Display { get; }

			public FakeConsole()
			{
				Display = new List<string>();
			}
			

			public void PrintLine(string line)
			{
				Display.Add(line);
			}
		}
		
		private ConsoleSocialNetwork _socialNetwork;
		private FakeConsole _console;
		private int _count = 0;
		private DateTime _now;
		
		[BeforeScenario]
		public void InitNetwork()
		{
			_now = DateTime.Now;
			var readCommandFactory = new ReadCommandFactory();
			var postCommandFactory = new PostCommandFactory();
			var wallCommandFactory = new WallCommandFactory();
			var followCommandFactory = new WallCommandFactory();
			var repository = new MemoryPostRepository();
			var engine = new SocialEngine(repository);
			var parser = new StringCommandParser(new CommandFactory[]
				{readCommandFactory, postCommandFactory, wallCommandFactory, followCommandFactory});
			var formatter = new PostTsStringFormatter();
			_console = new FakeConsole();
			var display = new ConsoleDisplay(formatter, _console);
			_socialNetwork = new ConsoleSocialNetwork(parser, engine, display);
		}
			

		[Given("(.*) posted \"(.*)\" to (?:his|her) wall (.*) (minutes?|seconds) ago")]
		public void GivenAUserPosted(string user, string message, int delta, string unit)
		{
			TimeService.TestNow = unit == "seconds" ? _now.AddSeconds(-delta) : _now.AddMinutes(-delta);
			var cmdStr = $"{user} -> {message}";
			_socialNetwork.Enter(cmdStr);
			TimeService.TestNow = _now;

		}
				

		[When(@"someone enters the command ""(.*)""")]
		public void WhenSomeoneEnters(string user)
		{
			var cmdStr = user;
			_socialNetwork.Enter(cmdStr);
		}
		
				
		[Then(@"he can read ""(.*)""")]
		public void ThenHeCanRead(string message)
		{
			Assert.AreEqual(message, _console.Display[_count]);
			_count++;
		}
				
	}
}
		
