module AcceptanceTests

open NUnit.Framework
open Acceptance.Steps

// Scenario: Reading Alice's posts
// Given Alice posted "I love the weather today" to her wall 5 minutes ago
// When someone enters the command "Alice"
// Then he can read "I love the weather today (5 minutes ago)"

[<SetUp>]
let init() = 
    lines.Clear()
    SocialNetwork.Core.fakeDisplay <- Some fakeDisplay


[<Test>]
let ``I Can read Alice posts``() =
    ``Given`` "Alice" ``posted`` "I love the weather today" ``to the wall`` 5 ``minutes ago`` 
    ``When I enter`` "Alice" 
    ``Then I can read`` "I love the weather today (5 minutes ago)"

[<Test>]
let ``I Can read Bob's posts``() =
    ``Given`` "Bob" ``posted`` "Damn! We lost!" ``to the wall`` 2 ``minutes ago`` 
    ``And`` "Bob" ``posted`` "Good game though" ``to the wall`` 1 ``minutes ago``     
    ``When I enter`` "Bob" 
    ``Then I can read`` "Good game though (1 minutes ago)"
    ``And I can read`` "Damn! We lost! (2 minutes ago)"

[<Test>]
let ``I Can subscribe to posts``() =
    ``Given`` "Alice" ``posted`` "I love the weather today" ``to the wall`` 5 ``minutes ago`` 
    ``And`` "Charlie" ``posted`` "I'm in New York today! Anyone wants to have a coffee?" ``to the wall`` 2 ``seconds ago``     
    ``And`` "Charlie" ``follows`` "Alice"
    ``When I enter`` "Charlie wall" 
    ``Then I can read`` "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)"
    ``And I can read`` "Alice - I love the weather today (5 minutes ago)"
