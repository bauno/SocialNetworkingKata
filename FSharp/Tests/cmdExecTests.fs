module CmdExec.Test

open SocialNetwork.Commands

let execRead (cmd: Command) = 
    "Read!"

let execPost cmd = 
    "Post"

let execs = [Post.ToString(), execPost; Read.ToString(), execRead] |> dict
