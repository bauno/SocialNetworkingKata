module Tests

open Xunit
open FsUnit.Xunit
open Chessie.ErrorHandling
open SocialNetwork.Parser
open SocialNetwork.Commands


[<Fact>]
let ``Can parse post command`` () =
    let cmdString = "Alice -> I love the wweather today!"
    cmdString 
    |> parse
    |> function
    | Post (u, m) -> u |> should equal "Alice"
                     m |> should equal "I love the wweather today!"
    | _ -> failwith "Error"

[<Fact>]
let ``Can parse read command``() =
  let cmdString = "Alice"
  cmdString
  |> parse
  |> function
  | Read u -> u |> should equal "Alice"
  |_ -> failwith "Error"
