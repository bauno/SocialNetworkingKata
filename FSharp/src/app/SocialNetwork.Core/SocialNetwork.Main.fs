module SocialNetwork.Main

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
        let wallrop =
            loadOrCreateWallOf
            >> loadWalls' loadOrCreateWallOf
            >> showOn' display
        wall' wallrop cmd

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
            >>= invalid
        exec' execRop cmd |> ignore 
    parse >> exec        

let internal init'' (now,niceTime) display =               
    let follow cmd =
        let followRop followed =
            loadOrCreateWallOf
            >> addFollowed followed
            >> save
        follow' followRop cmd
    

    let wall cmd =
        let wallrop =
            loadOrCreateWallOf
            >> loadWalls' loadOrCreateWallOf
            >> showOn'' (niceTime now) display
        wall' wallrop cmd

    let post cmd = 
        let postRop message = 
            loadOrCreateWallOf
            >> write' now message
            >> save
        post' postRop cmd

    let read cmd = 
        let readRop =         
            loadOrCreateWallOf
            >> displayOn'' (niceTime now) display
        read' readRop cmd
    
    let exec cmd =
        let execRop cmd =
            cmd
            |> post
            >>= read
            >>= follow
            >>= wall
            >>= invalid
        exec' execRop cmd |> ignore 
    parse >> exec    

let init =
    init' display
