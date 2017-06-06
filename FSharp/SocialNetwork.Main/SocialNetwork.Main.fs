module SocialNetwork.Main

open SocialNetwork.CmdExec
open SocialNetwork.Commands
open Core.Main


[<EntryPoint>]
let main argv =
  
    let cmdStr = "Alice -> I love the weather today"
    printfn "Posting..."
    exec cmdStr

    System.Threading.Thread.Sleep(5000);
    let cmdStr = "Alice -> Love love love"
    printfn "Posting..."
    exec cmdStr

    let readStr = "Alice"
    System.Threading.Thread.Sleep(5000)
    printfn "Reading..."
    exec readStr

    
    0 // return an integer exit code
