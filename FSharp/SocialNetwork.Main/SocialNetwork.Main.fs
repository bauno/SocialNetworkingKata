module SocialNetwork.Main

open SocialNetwork.CmdExec
open SocialNetwork.Commands
open SocialNetwork.Core
open SocialNetwork.Parser


[<EntryPoint>]
let main argv =
    let prompt() = printf "Enter a command (or 'q' to quit): "
    let rec readLine ()=         
        prompt()
        System.Console.ReadLine()
        |> function
        | "q" -> exit(0)        
        | cmd -> cmd |> parse |> exec |> ignore          
        readLine()
    readLine()
    0 // return an integer exit code
