using SocialNetwork.Core.Services.Interfaces;
using System.Collections.Generic;

namespace SocialNetwork.Tests.Acceptance
{
    public class FakeConsole : ITextConsole
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
}