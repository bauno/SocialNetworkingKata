module SocialNetwork.Core

open SocialNetwork.CmdExec
open SocialNetwork.Repository
open SocialNetwork.Commands
open SocialNetwork.Parser

let mutable internal fakeDisplay: ((string -> unit) option)  = None

let display () = 
    match fakeDisplay with
    | Some d -> d
    | None -> SocialNetwork.CmdExec.display
              

let private follow cmd =
    let followRop followed =
        loadOrCreateWallOf
        >> addFollowed followed
        >> save
    follow' followRop cmd

let private wall cmd =     
    let wallRop =
        loadWalls' loadOrCreateWallOf
        >> showOn' (display())
    wall' wallRop cmd       

let private post cmd = 
    let postRop message = 
        loadOrCreateWallOf
        >> write message
        >> save
    post' postRop cmd

let private read cmd = 
    let readRop =         
        loadOrCreateWallOf
        >> displayOn' (display())
    read' readRop cmd
    
let private exec cmd =
    let execRop cmd =
        cmd
        |> post
        >>= read
        >>= follow
        >>= wall
    exec' execRop cmd |> ignore 

let enter =
    parse >> exec    