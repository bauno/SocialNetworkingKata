using System;
using CSharp.Core;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CSharp.Tests.Acceptance
{
	[Binding]
	public class SocialNetwork
	{
		private ISocial _socialNetwork;
		private Post _post;
		
		[BeforeScenario]
		public void InitNetwork()
		{
			
			var repository = new MemoryPostRepository();
			_socialNetwork = new Social(repository);			
		}
			

		[Given("(.*) posted (.*) to her wall")]
		public void GivenAUserPosted(string user, string message)
		{
			_socialNetwork.Post(user, message);
		}

		[When(@"someone enters the command ""(.*)""")]
		public void WhenSomeoneEnters(string user)
		{
			_post = _socialNetwork.ReadWall(user);
		}

		[Then(@"he can read (.*)")]
		public void ThenHeCanRead(string message)
		{
			Assert.AreEqual(message, _post.Content);
		}
				
	}
}
		
