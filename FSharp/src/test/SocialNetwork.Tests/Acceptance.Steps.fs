module Acceptance.Steps

open System
open System.Collections.Generic
open SocialNetwork.Main
open FsUnit

let mutable now =  DateTime.Now
let testNow () =
    now




let mutable lines = new List<string>()
let mutable index = 0

let fakeDisplay line = 
    lines.Add(line)

let mutable enter = init'' (testNow,TimeService.niceTime') fakeDisplay

    
let ``Given`` (user) continuation =
    continuation (user)

let ``And`` (user) continuation = 
    continuation(user)    

let ``posted`` (user) message continuation =
    continuation (user,message)

let ``to the wall`` (user,message) minutes continuation =
    continuation(user,message,minutes)

let ``seconds ago`` (user, message, seconds) =
    let delta = seconds |> float
    let oldNow = now
    now <- oldNow.AddSeconds(-delta)    
    sprintf "%s -> %s" user message  
    |> enter
    now <- oldNow

let ``minutes ago``(user,message,minutes) =
  let delta = minutes |> float
  let oldNow = now
  now <- oldNow.AddMinutes(-delta)  
  sprintf "%s -> %s" user message 
  |> enter
  now <- oldNow


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
