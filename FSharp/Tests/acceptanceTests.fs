module AcceptanceTests

open NUnit.Framework
open Acceptance.Steps

[<SetUp>]
let init() =     
    lines.Clear()
    index <- 0
    SocialNetwork.Repository.clear()    

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
let ``I Can subscribe to a user's posts``() =
    ``Given`` "Alice" ``posted`` "I love the weather today" ``to the wall`` 5 ``minutes ago`` 
    ``And`` "Charlie" ``posted`` "I'm in New York today! Anyone wants to have a coffee?" ``to the wall`` 2 ``seconds ago``     
    ``And`` "Charlie" ``follows`` "Alice"
    ``When I enter`` "Charlie wall" 
    ``Then I can read`` "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)"
    ``And I can read`` "Alice - I love the weather today (5 minutes ago)"


[<Test>]
let ``I Can subscribe to a multiple users' posts``() =
    ``Given`` "Bob" ``posted`` "Damn! We lost!" ``to the wall`` 2 ``minutes ago`` 
    ``And`` "Bob" ``posted`` "Good game though" ``to the wall`` 1 ``minutes ago``     
    ``And`` "Alice" ``posted`` "I love the weather today" ``to the wall`` 15 ``minutes ago`` 
    ``And`` "Charlie" ``posted`` "I'm in New York today! Anyone wants to have a coffee?" ``to the wall`` 15 ``seconds ago``     
    ``And`` "Charlie" ``follows`` "Alice"
    ``And`` "Charlie" ``follows`` "Bob"
    ``When I enter`` "Charlie wall" 
    ``Then I can read`` "Charlie - I'm in New York today! Anyone wants to have a coffee? (15 seconds ago)"
    ``And I can read`` "Bob - Good game though (1 minutes ago)"
    ``And I can read`` "Bob - Damn! We lost! (2 minutes ago)"
    ``And I can read`` "Alice - I love the weather today (15 minutes ago)"
