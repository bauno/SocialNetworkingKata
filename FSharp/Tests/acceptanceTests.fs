module AcceptanceTests

open FsUnit.Xunit
open Xunit
open System
open System.Collections.Generic
open NUnit.Framework
open SocialNetwork.Core


// Scenario: Reading Alice's posts
// Given Alice posted "I love the weather today" to her wall 5 minutes ago
// When someone enters the command "Alice"
// Then he can read "I love the weather today (5 minutes ago)"

let mutable lines = new List<string>()

let fakeDisplay line = lines.Add(line)

let ``Given`` (user:string) continuation =
    printfn "%s" user
    continuation (user)

let ``posted`` (user) message continuation =
    printfn "Posted"
    continuation (user,message)

let ``to her wall`` (user,message:string) minutes continuation =
    printfn "wall"
    continuation(user,message,minutes)

let ``minutes ago``(user,message,minutes:int) continuation =
  printfn "minutes"
  let now = DateTime.Now
  TimeService.testNow <- now.AddMinutes((float)(-minutes)) |> Some
  enter (sprintf "%s -> %s" user message )
  continuation

let ``When I enter``(user) continuation =
    enter user
    continuation()

let ``Then I can read`` displayedMessage continuation = 
    displayedMessage |> should equal "I love the weather today (5 minutes ago)"

// [<Test>]
let ``I Can read Alice posts``() =
    ``Given`` "Alice" ``posted`` "I love the weather today" ``to her wall`` 5 ``minutes ago``
    // ``When I enter`` "Alice"
    // ``Then I can read`` "I love the weather today (5 minutes ago)"

let ``Given I am dumb`` (times) continuation =
    continuation(times)

let ``Then I can be more dumb``(times) =
    times |> should equal 5

let ``bucks`` (times) continuation =
    continuation(times)

[<Test>]
let ``I can be dumb`` () =
    ``Given I am dumb`` 5 ``bucks`` ``Then I can be more dumb``