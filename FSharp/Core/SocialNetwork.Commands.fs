module SocialNetwork.Commands

let postPattern = @"^(.*) -> (.*)$"
let readPattern = @"^(\w+)$"

type Command =
    | Post of string*string
    | Wall of string
    | Invalid of string    