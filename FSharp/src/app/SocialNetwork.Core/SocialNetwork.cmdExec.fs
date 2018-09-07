module internal SocialNetwork.CmdExec

open SocialNetwork.Commands
open SocialNetwork.Data

let write' now message wall =
  {wall with Posts = wall.Posts @ [{Content = message; TimeStamp = now(); User = wall.User}]}

let display line =
    printfn "%s" line

let displayOn'' niceTime display wall = 
    wall.Posts
    |> Seq.sortByDescending(fun p -> p.TimeStamp)
    |> Seq.map(fun p -> {Content = p.Content |> xm ; NiceTime = p.TimeStamp |> niceTime; User = wall.User |> xu})
    |> Seq.iter (fun p -> display (sprintf "%s (%s)" p.Content p.NiceTime))

let addFollowed followed wall =
    {wall with Follows = wall.Follows @ [followed]}

let exec' rop cmd =
    rop cmd

let post' rop =
    function
    | Post(user, message) -> rop message user
                             Done
    | cmd -> Continue cmd

let invalid =
    function
    | Invalid cmd -> printfn "The command '%s' is invalid" cmd
                     Done
    |cmd -> Continue cmd



let read' rop =
    function
    | Read user -> rop user
                   Done
    | cmd -> Continue cmd

let follow' rop =
    function
    | Follows (user, whoToFollow) -> rop whoToFollow user
                                     Done
    | otherCommand -> Continue otherCommand

let wall' rop  =
    function
    | Wall user -> rop user
                   Done
    |cmd -> Continue cmd

let loadWalls' loadWall wall =
    let others = wall.Follows
                 |> Seq.map (xf >> loadWall)
                 |> Seq.toList
    [wall]@others

let showOn'' niceTime display walls =

    walls
    |> List.collect (fun wall -> wall.Posts)
    |> List.sortByDescending (fun p -> p.TimeStamp)
    |> List.map(fun p -> {Content= xm p.Content; NiceTime = p.TimeStamp |> niceTime; User = p.User |> xu}
                         |> fun p -> (sprintf "%s - %s (%s)" p.User p.Content p.NiceTime))
    |> List.iter display
