using Xbehave;
using System;


namespace SocialNetwork.Tests.Acceptance
{
    public partial class SocialNetworkTests
    {        
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

        [Scenario]
        public void Subscriptions(){
            "Given Alice posted 'I love the weather today' to her wall 5 minutes ago"
            .x(() => PostToWall("Alice", "I love the weather today", 5, "minutes"));
        	
            "And Charlie posted 'I''m in New York today! Anyone wants to have a coffee?' to his wall 2 seconds ago"
            .x(() => PostToWall("Charlie", "I'm in New York today! Anyone wants to have a coffee?", 2, "seconds" ));

            "And Charlie follows Alice"
            .x(() => Follows("Charlie", "Alice"));

            "When someone enters the command 'Charlie wall'"
            .x(() => _socialNetwork.Enter("Charlie wall"));
            
            "Then he can read 'Charlie - I''m in New York today! Anyone wants to have a coffee? (2 seconds ago)'"
            .x(() => CheckMessage("Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)"));

            "And he can read 'Alice - I love the weather today (5 minutes ago)'"
            .x(() => CheckMessage("Alice - I love the weather today (5 minutes ago)"));
        }

        [Scenario]
        public void Multiple_Subscription(){
            "Given Bob posted 'Damn! We lost' to his wall 2 minutes ago"
            .x(() => PostToWall("Bob", "Damn! We lost!", 2, "minutes"));

            "And Bob posted 'Good game though' to his wall 1 minute ago"
            .x(() => PostToWall("Bob", "Good game though", 1, "minute"));

            "And Alice posted 'I love the weather today' to her wall 15 minutes ago"
            .x(() => PostToWall("Alice", "I love the weather today", 15, "minutes"));

            "And Charlie posted 'I'm in New York today! Anyone wants to have a coffee?' to his wall 15 seconds ago"
            .x(() => PostToWall("Charlie", "I'm in New York today! Anyone wants to have a coffee?", 15, "seconds" ));

            "And Charlie follows Alice"
            .x(() => Follows("Charlie", "Alice"));

            "And Charlie follows Bob"
            .x(() => Follows("Charlie", "Bob"));

            "When someone enters the command 'Charlie wall'"
            .x(() => _socialNetwork.Enter("Charlie wall"));

            "Then he can read 'Charlie - I''m in New York today! Anyone wants to have a coffee? (15 seconds ago)'"
            .x(() => CheckMessage("Charlie - I'm in New York today! Anyone wants to have a coffee? (15 seconds ago)"));

            "And he can read 'Bob - Good game though (1 minutes ago)'"
            .x(() => CheckMessage("Bob - Good game though (1 minutes ago)"));

            "And he can read 'Bob - Damn! We lost (2 minutes ago)'"
            .x(() => CheckMessage("Bob - Damn! We lost! (2 minutes ago)"));

            "And he can read 'Alice - I love the weather today (15 minutes ago)'"
            .x(() => CheckMessage("Alice - I love the weather today (15 minutes ago)"));

        }
    }
}