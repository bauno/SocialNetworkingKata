module Acceptance.Steps

open System
open System.Collections.Generic
open System.Linq
open SocialNetwork.Core
open NUnit.Framework
open FsUnit.Xunit

let mutable lines = new List<string>()

let fakeDisplay line = 
    lines.Add(line)
    
let ``Given`` (user) continuation =
    continuation (user)

let ``And`` (user) continuation = 
    continuation(user)    

let ``posted`` (user) message continuation =
    continuation (user,message)

let ``to the wall`` (user,message) minutes continuation =
    continuation(user,message,minutes)

let ``seconds ago`` (user, message, seconds) =
    let now = DateTime.Now
    let delta = (float)seconds
    let postTs = now.AddSeconds(-delta)
    TimeService.testNow <- Some postTs
    enter (sprintf "%s -> %s" user message )
    TimeService.testNow <- Some now

let ``minutes ago``(user,message,(minutes:int)) =
  let now = DateTime.Now
  let delta = (float)minutes
  let postTs = now.AddMinutes(-delta)
  TimeService.testNow <- Some postTs
  enter (sprintf "%s -> %s" user message )
  TimeService.testNow <- Some now

let ``follows``(user) followed = 
    let cmd = sprintf "%s follows %s" user followed
    enter cmd

let ``When I enter``(user) =
    enter user    

let ``Then I can read`` displayedMessage =     
    lines.First() |> should equal displayedMessage
let ``And I can read`` displayedMessage =     
    lines.Last() |> should equal displayedMessage
