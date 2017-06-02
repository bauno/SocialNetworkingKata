module SocialNetwork.Commands

let postPattern = @"^(.*) -> (.*)$"
let readPattern = @"^(\w+)$"

type Command =
    | Post of string*string
    | Read of string
    | Wall of string
    | Invalid of string
