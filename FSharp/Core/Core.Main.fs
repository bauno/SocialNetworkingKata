module SocialNetwork.Core

open SocialNetwork.CmdExec
open SocialNetwork.Repository
open SocialNetwork.Commands
open SocialNetwork.Parser

let post cmd = 
    let postRop message = 
        loadOrCreateWallOf
        >> write message
        >> save
    post' postRop cmd

let read cmd = 
    let readRop =         
        loadOrCreateWallOf
        >> displayOn display
    read' readRop cmd

let exec cmd =
    let execRop cmd =
        cmd
        |> post
        |> (bind read)
    exec' execRop cmd    