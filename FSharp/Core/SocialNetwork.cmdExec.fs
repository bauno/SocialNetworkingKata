module SocialNetwork.CmdExec

open SocialNetwork.Commands
open SocialNetwork.Parser
open SocialNetwork.Repository
open SocialNetwork.Data
open TimeService

let write message wall =  
  {wall with Posts = wall.Posts @ [{Content = message; TimeStamp = TimeService.Now(); User = wall.User}]}  
    

let display line = 
    printfn "%s" line

let displayOn' display wall =
    wall.Posts
    |> Seq.sortByDescending(fun p -> p.TimeStamp)
    |> Seq.map(fun p -> {Content = p.Content; NiceTime = p.TimeStamp |> TimeService.NiceTime; User = wall.User})
    |> Seq.iter (fun p -> display (sprintf "%s (%s)" p.Content p.NiceTime))


let addFollowed followed (wall:Wall) =
    printfn "Adding %s to follower %s" followed wall.User
    {wall with Follows = wall.Follows @ [followed]}


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

let follow' rop cmd =
    match cmd with
    | Follows (user, whoToFollow) -> rop whoToFollow user
                                     Done
    |_ -> Continue cmd


let wall' rop cmd = 
    match cmd with
    | Wall user -> rop user
                   Done
    |_ -> Continue cmd                      

let loadWalls' loadWall user = 
    let wall = loadWall user
    let others = wall.Follows 
                 |> Seq.map loadWall
                 |> Seq.toList
    [wall]@others


let showOn' display walls = 

    walls
    |> List.collect (fun wall -> wall.Posts)
    |> List.sortByDescending (fun p -> p.TimeStamp)
    |> List.map(fun p -> {Content=p.Content; NiceTime = TimeService.NiceTime(p.TimeStamp); User = p.User} 
                         |> fun p -> (sprintf "%s - %s (%s)" p.User p.Content p.NiceTime))    
    |> List.iter display     