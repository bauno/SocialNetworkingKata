module SocialNetwork.CmdExec

open SocialNetwork.Commands
open SocialNetwork.Parser
open SocialNetwork.Repository
open SocialNetwork.Data
open TimeService

let write message wall =  
  {wall with Posts = wall.Posts @ [{Content = message; TimeStamp = TimeService.Now()}]}  
    

let display line = 
    printfn "%s" line

let displayOn display wall =
    wall.Posts
    |> Seq.sortByDescending(fun p -> p.TimeStamp)
    |> Seq.map(fun p -> {Content = p.Content; NiceTime = p.TimeStamp |> TimeService.NiceTime})
    |> Seq.iter (fun p -> display (sprintf "%s (%s)" p.Content p.NiceTime))



let exec' rop cmd =
    rop cmd

let post' rop cmd =
    match cmd with
    | Post(user, message) -> rop message user    
                             Done
    | _ -> Continue cmd

let read' rop cmd = 
    match cmd with
    | Read user -> rop user
                   Done
    | _ -> Continue cmd

