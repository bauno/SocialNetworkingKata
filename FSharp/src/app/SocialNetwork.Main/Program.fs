module SocialNetwork.Main

let rec readLine sendCommand =         
    printf "Enter a command (or 'q' to quit): "    
    System.Console.ReadLine()
    |> function
    | "q" -> exit 0
    | cmd -> cmd |> sendCommand
    readLine sendCommand

[<EntryPoint>]
let main argv =
    SocialNetwork.Core.init
    |> readLine 
    0 // return an integer exit code
