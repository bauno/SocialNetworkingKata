module internal SocialNetwork.CmdExec

open SocialNetwork.Commands
open SocialNetwork.Parser
open SocialNetwork.Repository
open SocialNetwork.Data
open TimeService

let write message wall = 
  {wall with Posts = wall.Posts @ [{Content = xMessage message; TimeStamp = TimeService.Now(); User = wall.User}]}  
    

let display line = 
    printfn "%s" line

let displayOn' display wall =
    wall.Posts
    |> Seq.sortByDescending(fun p -> p.TimeStamp)
    |> Seq.map(fun p -> {Content = p.Content; NiceTime = p.TimeStamp |> TimeService.NiceTime; User = wall.User |> xUser})
    |> Seq.iter (fun p -> display (sprintf "%s (%s)" p.Content p.NiceTime))


let addFollowed followed wall =        
    {wall with Follows = wall.Follows @ [followed]}


let exec' rop cmd =
    rop cmd

let post' rop cmd =
    match cmd with
    | Post(user, message) -> rop message user    
                             Done
    | _ -> Continue cmd

let invalid cmd = 
    match cmd with
    | Invalid cmd -> printfn "The command '%s' is invalid" cmd
                     Done
    |_ -> Continue cmd                 



let read' rop cmd = 
    match cmd with
    | Read user -> rop user
                   Done
    | _ -> Continue cmd

let follow' rop cmd =
    cmd
    |> function
    | Follows (user, whoToFollow) -> rop whoToFollow user
                                     Done
    | otherCommand -> Continue otherCommand    


let wall' rop cmd = 
    match cmd with
    | Wall user -> rop user
                   Done
    |_ -> Continue cmd                      

let loadWalls' loadWall (user: User) = 
    let wall = loadWall user
    let others = wall.Follows                     
                 |> Seq.map (xFollowed >> User >> loadWall)
                 |> Seq.toList
    [wall]@others    


let showOn' display walls = 

    walls
    |> List.collect (fun wall -> wall.Posts)
    |> List.sortByDescending (fun p -> p.TimeStamp)
    |> List.map(fun p -> {Content=p.Content; NiceTime = TimeService.NiceTime(p.TimeStamp); User = p.User |> xUser} 
                         |> fun p -> (sprintf "%s - %s (%s)" p.User p.Content p.NiceTime))    
    |> List.iter display