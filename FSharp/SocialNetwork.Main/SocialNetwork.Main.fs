module SocialNetwork.Main

open SocialNetwork.CmdExec
open SocialNetwork.Commands
open SocialNetwork.Core
open SocialNetwork.Parser


[<EntryPoint>]
let main argv =
  
    let cmdStr = "Alice -> I love the weather today"
    printfn "Posting %s" cmdStr
    cmdStr |> parse |> exec

    System.Threading.Thread.Sleep(5000);
    let cmdStr = "Alice -> Love love love"
    printfn "Posting %s" cmdStr    
    cmdStr |> parse |> exec 

    let readStr = "Alice"
    System.Threading.Thread.Sleep(5000)
    printfn "Reading..."
    readStr |> parse |> exec

    
    0 // return an integer exit code
