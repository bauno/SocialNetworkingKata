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

