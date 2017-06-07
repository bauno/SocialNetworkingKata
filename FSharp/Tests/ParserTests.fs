module Tests

open Xunit
open FsUnit.Xunit
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

[<Fact>]
let ``Can parse wall command``() = 
  let cmdString = "Alice wall"
  cmdString
  |> parse
  |> function
  | Wall u -> u |> should equal "Alice"
  |_ -> failwith "Error"

[<Fact>]
let ``Can parse follow command``() = 
  let cmdString = "Alice follows Bob"
  cmdString
  |> parse
  |> function
  | Follows(u,w) -> u |> should equal "Alice"
                    w |> should equal "Bob"
  |_ -> failwith "Error"  