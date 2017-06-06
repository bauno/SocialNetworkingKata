module CmdExec.Test

open SocialNetwork.Commands
open SocialNetwork.Data
open SocialNetwork.CmdExec
open Microsoft.FSharp.Reflection


open Xunit
open FsUnit.Xunit

open System
open System.Collections.Generic
open System.Linq

[<Fact>]
let ``Can write to a wall`` () =
    
    let now = DateTime.Now
    TimeService.testNow <- Some now
    
    let post = {Content = "Figa"; TimeStamp = now.AddSeconds(-20.0); User = "Bauno"}
    let wall = {User = "Bauno"; Follows = list.Empty; Posts = [post] }
    
    let writtenWAll = write "Cazzo" wall    
    let modWall = {wall with Posts = wall.Posts@[{Content = "Cazzo"; TimeStamp = now; User = "Bauno"}]}

    writtenWAll |> should equal modWall

[<Fact>]
let ``Can display post on display``() =
    let now = DateTime.Now
    TimeService.testNow <- Some now
    let wall = {
        User = "pippo"; 
        Follows = list.Empty
        Posts = [
                    {Content = "Cazzo"; TimeStamp = now.AddSeconds(-10.0); User = "pippo"}
                    {Content = "Figa"; TimeStamp = now.AddSeconds(-5.0); User = "pippo"}
        ]
    }

    let mutable lines = List<string>()
    let display line = lines.Add(line)

    displayOn' display wall

    lines.First() |> should equal "Figa (5 seconds ago)"
    lines.Last() |> should equal "Cazzo (10 seconds ago)"

[<Fact>]
let ``Can display walls`` () =
    let now = DateTime.Now
    TimeService.testNow <- Some now
    let wall1 = {
        User = "pippo"; 
        Follows = list.Empty
        Posts = [
                    {Content = "qui"; TimeStamp = now.AddSeconds(-10.0); User="pippo"}
                    {Content = "quo"; TimeStamp = now.AddSeconds(-5.0); User="pippo"}
        ]
    }
    let wall2 = {
        User = "pluto"; 
        Follows = list.Empty
        Posts = [
                    {Content = "paperino"; TimeStamp = now.AddSeconds(-2.0); User = "pluto"}
                    {Content = "topolino"; TimeStamp = now.AddSeconds(-1.0); User = "pluto"}
        ]
    }
    let walls = [wall1; wall2]

    let mutable lines = List<string>()
    let display line = lines.Add(line)

    showOn' display walls

    lines.[0] |> should equal "pluto - topolino (1 seconds ago)"
    lines.[1] |> should equal "pluto - paperino (2 seconds ago)"
    lines.[2] |> should equal "pippo - quo (5 seconds ago)"
    lines.[3] |> should equal "pippo - qui (10 seconds ago)"




[<Fact>]    
let ``Can load my Wall and that of all my followers`` () =
    let pippo = {User = "pippo"; Follows = ["pluto"; "paperino"]; Posts = list.Empty}
    let pluto = {User = "pluto"; Follows = List.Empty; Posts = List.Empty}
    let paperino = {pluto with User = "paperino"}

    let loadWall user = 
        match user with
        | "pippo" -> pippo
        | "pluto" -> pluto
        | "paperino" -> paperino
        | _ -> failwith "error"
    

    loadWalls' loadWall "pippo" |> should equal [pippo; pluto; paperino]



[<Fact>]
let ``Can follow other user``() =
    let user = "pippo"
    let followed = "pluto"
    let wall = {User = user; Follows = ["paperino"]; Posts = list.Empty}
    addFollowed followed wall |> should equal {wall with Follows = ["paperino"; "pluto"]}
    

           

[<Fact>]
let ``Can execute command`` () = 
    let mutable called = false
    let rop = fun cmd -> called <- true        

    exec' rop "pippo" 

    called |> should be True

[<Fact>]          
let ``Can execute post comand rop``() =
    let mutable called = false
    let rop = (fun a b -> called <- true; b |> should equal "pippo"; a |> should equal "pluto")
    let cmd = Post("pippo","pluto")
    post' rop cmd |> should equal Done
    called |> should be True


[<Fact>]          
let ``Can execute read comand rop``() =
    let mutable called = false
    let rop = fun r -> (called <- true; r |> should equal "pippo")
    let cmd = Read("pippo")
    read' rop cmd |> should equal Done
    called |> should be True


[<Fact>]
let ``Can Execute follow command``() =
    let mutable called = false
    let rop = fun a b -> called <-true
    let cmd = Follows("pippo", "pluto")
    follow' rop cmd |> should equal Done
    called |> should be True

[<Fact>]
let ``Can execute wall Command``() =
    let mutable called = false
    let rop = fun a -> ( called <- true; a |> should equal "pippo")
    let cmd = Wall("pippo")    
    wall' rop cmd |> should equal Done
    called |> should be True

