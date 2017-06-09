module SocialNetwork.Commands
open SocialNetwork.Data

let postPattern = @"^(\w+) -> (.*)$"
let readPattern = @"^(\w+)$"

let wallPattern = @"^(\w+) wall$"
let followPattern = @"^(\w+) follows (\w+)$"

type Command =
    | Post of User*Message
    | Follows of User*Followed
    | Invalid of string
    | Read of User
    | Wall of User


type Result = 
    | Done
    | Continue of Command


let inline (>>=) result f =
    match result with
    | Done -> Done
    | Continue cmd -> f cmd