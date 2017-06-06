module SocialNetwork.Main

open SocialNetwork.CmdExec
open SocialNetwork.Commands
open SocialNetwork.Core
open SocialNetwork.Parser


[<EntryPoint>]
let main argv =
  
    let cmdStr = "Alice -> I love the weather today"
    printfn "Posting %s" cmdStr
    cmdStr |> parse |> exec |> ignore

    // System.Threading.Thread.Sleep(5000);
    let cmdStr = "Alice -> Love love love"
    printfn "Posting %s" cmdStr    
    cmdStr |> parse |> exec |> ignore

    let readStr = "Alice"
    // System.Threading.Thread.Sleep(5000)
    printfn "Reading..."
    readStr |> parse |> exec |> ignore
    

    "Charlie -> Hate!" |> parse |> exec |> ignore

    "Alice follows Charlie" |> parse |> exec |> ignore

    "Alice wall" |> parse |> exec |> ignore    

    
    0 // return an integer exit code
