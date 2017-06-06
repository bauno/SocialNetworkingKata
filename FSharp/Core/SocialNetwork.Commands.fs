module SocialNetwork.Commands
open SocialNetwork.Data

let postPattern = @"^(.*) -> (.*)$"
let readPattern = @"^(\w+)$"

type Command =
    | Post of string*string
    | Follows of string*string
    | Invalid of string
    | Read of string
    | Wall of string


type Result = 
    | Done
    | Continue of Command

let bind f result = 
    match result with
    | Done -> Done
    | Continue cmd -> cmd |> f


let inline (>>=) result f = bind f result