module Acceptance.Steps

open System
open System.Collections.Generic
open System.Linq
open SocialNetwork.Main
open NUnit.Framework
open FsUnit

let mutable lines = new List<string>()
let mutable index = 0

let fakeDisplay line = 
    lines.Add(line)

let mutable enter = init' fakeDisplay

    
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

let ``minutes ago``(user,message,minutes) =
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
    lines.[index] |> should equal displayedMessage
    index <- index + 1

let ``And I can read`` displayedMessage =     
    lines.[index] |> should equal displayedMessage
    index <- index + 1
