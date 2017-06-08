module AcceptanceTests

open FsUnit.Xunit
open Xunit
open System
open System.Collections.Generic
open System.Linq
open NUnit.Framework
open SocialNetwork.Core


// Scenario: Reading Alice's posts
// Given Alice posted "I love the weather today" to her wall 5 minutes ago
// When someone enters the command "Alice"
// Then he can read "I love the weather today (5 minutes ago)"

let mutable lines = new List<string>()

let fakeDisplay line = 
    lines.Add(line)

[<SetUp>]
let init() = 
    lines.Clear()
    SocialNetwork.Core.fakeDisplay <- Some fakeDisplay
    
let ``Given`` (user) continuation =
    continuation (user)

let ``And`` (user) continuation = 
    continuation(user)    

let ``posted`` (user) message continuation =
    continuation (user,message)

let ``to the wall`` (user,message) minutes continuation =
    continuation(user,message,minutes)

let ``minutes ago``(user,message,(minutes:int)) =
  let now = DateTime.Now
  let delta = (float)(-minutes)
  let postTs = now.AddMinutes(delta)
  TimeService.testNow <- Some postTs
  enter (sprintf "%s -> %s" user message )
  TimeService.testNow <- Some now

let ``When I enter``(user) =
    enter user    

let ``Then I can read`` displayedMessage =     
    lines.First() |> should equal displayedMessage
let ``And I can read`` displayedMessage =     
    lines.Last() |> should equal displayedMessage


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
