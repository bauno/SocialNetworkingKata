module Tests

open Xunit
open FsUnit.Xunit
open Chessie.ErrorHandling
open SocialNetwork.Parser
open SocialNetwork.Commands



[<Fact>]
let ``Example Test`` () =
    1 |> should equal 1

[<Fact>]
let ``Another test``() =
    true |> should be True

[<Fact>]
let ``Can parse post command`` () =    
    let cmdString = "Alice -> I love the wweather today!"
    cmdString |> parse
    |> function    
    | Post (u, m) -> u |> should equal "Alice"
                     m |> should equal "I love the wweather today!"
    | _ -> failwith "Error"


