module SocialNetwork.Main

open SocialNetwork.Commands
open SocialNetwork.Core


[<EntryPoint>]
let main argv =
    let enter = init
    let prompt() = printf "Enter a command (or 'q' to quit): "
    let rec readLine ()=         
        prompt()
        System.Console.ReadLine()
        |> function
        | "q" -> exit 0
        | cmd -> cmd |> enter
        readLine()
    readLine()
    0 // return an integer exit code
