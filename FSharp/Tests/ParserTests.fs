module Tests

open NUnit.Framework
open FsUnit
open SocialNetwork.Parser
open SocialNetwork.Commands
open SocialNetwork.Data


[<Test>]
let ``Can parse post command`` () =
    let cmdString = "Alice -> I love the wweather today!"
    cmdString 
    |> parse
    |> function
    | Post (u, m) -> u |> xUser |>  should equal "Alice"
                     m |> xMessage |> should equal "I love the wweather today!"
    | _ -> failwith "Error"

[<Test>]
let ``Can parse read command``() =
  let cmdString = "Alice"
  cmdString
  |> parse
  |> function
  | Read u -> u |> xUser |> should equal "Alice"
  |_ -> failwith "Error"

[<Test>]
let ``Can parse wall command``() = 
  let cmdString = "Alice wall"
  cmdString
  |> parse
  |> function
  | Wall u -> u |> xUser |> should equal "Alice"
  |_ -> failwith "Error"

[<Test>]
let ``Can parse follow command``() = 
  let cmdString = "Alice follows Bob"
  cmdString
  |> parse
  |> function
  | Follows(u,w) -> u |> xUser |> should equal "Alice"
                    w |> xFollowed |> should equal "Bob"
  |_ -> failwith "Error"  