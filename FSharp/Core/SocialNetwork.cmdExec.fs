module SocialNetwork.CmdExec

open SocialNetwork.Commands
open SocialNetwork.Parser
open SocialNetwork.Repository
open SocialNetwork.Data
open TimeService

let write message wall =
  //wall.Posts.Add({Content = message; TimeStamp = TimeService.Now}  
  {wall with Posts = wall.Posts @ [{Content = message; TimeStamp = TimeService.Now()}]}  

let post user message =
    user
    |> loadWall    
    |> write message
    |> save


let rop message =
    loadWall
    >> write message
    >> save

let post' message user =
    user
    |> rop message

let read user = 
  user
  |> loadWall
  |> printfn "%A" 
  true

let display line = 
    printfn "%s" line

let displayOn display wall =
    wall.Posts
    |> Seq.sortBy (fun p -> p.TimeStamp)
    |> Seq.iter (fun p -> display "%s" p.Content)    



let exec cmdStr = 
    cmdStr
    |> parse
    |> function
    | Post (user, message) -> user |> loadWall |> write message |> save |> ignore
    | Read user -> user |> loadWall |> printfn "%A"
    | _ -> failwith "Invalid command!"

