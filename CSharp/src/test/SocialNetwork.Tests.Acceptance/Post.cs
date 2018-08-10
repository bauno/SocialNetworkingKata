using Xbehave;
using System;

namespace SocialNetwork.Tests.Acceptance
{
    public class AliceCanPost
    {
        [Scenario]
        public void Can_Read_Alice_Posts(){
            "Given Alice has posted 'I love the weather today' to her wall"
            .x(() => Console.WriteLine("Pippo"));            

            "When someone enters the command 'Alice'"
            .x(() => Console.WriteLine("Pluto"));

            "Then he can read 'I love the weather today (5 minutes ago)'"
            .x(() => Console.WriteLine("Topolino"));
        }
    }
}