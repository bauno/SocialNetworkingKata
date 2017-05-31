using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using CSharp.Core;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CSharp.Tests.Acceptance
{
	[Binding]
	public class SocialNetworkTests
	{
		private class FakeDisplay : Display
		{
			public List<string> Display { get; }

			public FakeDisplay()
			{
				Display = new List<string>();
			}

			public void Show(WallView wall)
			{
				wall.Posts.ToList().ForEach(p => Display.Add(p.Content));
			}
		}
		
		private ConsoleSocialNetwork _socialNetwork;
		private FakeDisplay _display;
		private int _count = 0;
		
		[BeforeScenario]
		public void InitNetwork()
		{
			
			var repository = new MemoryPostRepository();
			var engine = new SocialEngine(repository);
			var parser = new StringCommandParser();
			_display = new FakeDisplay();
			_socialNetwork = new ConsoleSocialNetwork(parser, engine, _display);
		}
			

		[Given("(.*) posted (.*) to (?:his|her) wall (.*) minutes? ago")]
		public void GivenAUserPosted(string user, string message, int delta)
		{
			var cmdStr = $"{user} -> {message}";
			_socialNetwork.Enter(cmdStr);
			
		}
				

		[When(@"someone enters the command ""(.*)""")]
		public void WhenSomeoneEnters(string user)
		{
			var cmdStr = user;
			_socialNetwork.Enter(cmdStr);
		}

		[Then(@"he can read (.*)")]
		public void ThenHeCanRead(string message)
		{
			Assert.AreEqual(message, _display.Display[_count]);
			_count++;
		}
				
	}
}
		
