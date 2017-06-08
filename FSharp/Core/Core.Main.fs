module SocialNetwork.Core

open SocialNetwork.CmdExec
open SocialNetwork.Repository
open SocialNetwork.Commands
open SocialNetwork.Parser

let internal init' display =               
    let follow cmd =
        let followRop followed =
            loadOrCreateWallOf
            >> addFollowed followed
            >> save
        follow' followRop cmd

    let wall cmd =     
        let wallRop =
            loadWalls' loadOrCreateWallOf
            >> showOn' display
        wall' wallRop cmd       

    let post cmd = 
        let postRop message = 
            loadOrCreateWallOf
            >> write message
            >> save
        post' postRop cmd

    let read cmd = 
        let readRop =         
            loadOrCreateWallOf
            >> displayOn' display
        read' readRop cmd
    
    let exec cmd =
        let execRop cmd =
            cmd
            |> post
            >>= read
            >>= follow
            >>= wall
        exec' execRop cmd |> ignore 
    parse >> exec        

let init =
    init' display
