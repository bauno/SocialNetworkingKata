namespace SocialNetwork.Tests.Acceptance
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
}