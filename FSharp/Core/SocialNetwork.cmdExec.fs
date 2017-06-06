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


let exec'' parser post cmdStr = 
    cmdStr
    |> parser
    |> function
    | Post (user, message) -> post user message    
    | Read user -> user |> loadOrCreateWallOf |> displayOn display
    | _ -> failwith "Invalid command!"


let exec' post read cmd =
    cmd
    |> function
    | Post(user, message) -> post user message    
    | Read user -> read user
    | _ -> failwith "Error"