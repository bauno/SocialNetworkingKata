module Core.Main

open SocialNetwork.CmdExec
open SocialNetwork.Repository
open SocialNetwork.Commands
open SocialNetwork.Parser

let post user message = 
    let rop message = 
        loadOrCreateWallOf
        >> write message
        >> save
    user |> (rop message)
    
let exec cmdStr = exec'' parse post cmdStr