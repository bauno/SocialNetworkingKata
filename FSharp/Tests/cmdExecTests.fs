module CmdExec.Test

open SocialNetwork.Commands
open SocialNetwork.Data
open SocialNetwork.CmdExec
open Microsoft.FSharp.Reflection

open NUnit.Framework
open FsUnit

open System
open System.Collections.Generic
open System.Linq

[<Test>]
let ``Can write to a wall`` () =
    
    let now = DateTime.Now
    TimeService.testNow <- Some now
    
    let post = {Content = "Quo"; TimeStamp = now.AddSeconds(-20.0); User = "Bauno"|> User}
    let wall = {User = "Bauno"|> User; Follows = list.Empty; Posts = [post] }
    
    let writtenWAll = write (Message("Qui")) wall    
    let modWall = {wall with Posts = wall.Posts@[{Content = "Qui"; TimeStamp = now; User = "Bauno"|> User}]}

    writtenWAll |> should equal modWall

[<Test>]
let ``Can display post on display``() =
    let now = DateTime.Now
    TimeService.testNow <- Some now
    let wall = {
        User = "pippo" |> User; 
        Follows = list.Empty
        Posts = [
                    {Content = "Qui"; TimeStamp = now.AddSeconds(-10.0); User = "pippo"|> User}
                    {Content = "Quo"; TimeStamp = now.AddSeconds(-5.0); User = "pippo"|> User}
        ]
    }

    let mutable lines = List<string>()
    let display line = lines.Add(line)

    displayOn' display wall

    lines.First() |> should equal "Quo (5 seconds ago)"
    lines.Last() |> should equal "Qui (10 seconds ago)"

[<Test>]
let ``Can display walls`` () =
    let now = DateTime.Now
    TimeService.testNow <- Some now
    let wall1 = {
        User = "pippo"|> User; 
        Follows = list.Empty
        Posts = [
                    {Content = "qui"; TimeStamp = now.AddSeconds(-10.0); User="pippo"|> User}
                    {Content = "quo"; TimeStamp = now.AddSeconds(-5.0); User="pippo"|> User}
        ]
    }
    let wall2 = {
        User = "pluto"|> User; 
        Follows = list.Empty
        Posts = [
                    {Content = "paperino"; TimeStamp = now.AddSeconds(-2.0); User = "pluto"|> User}
                    {Content = "topolino"; TimeStamp = now.AddSeconds(-1.0); User = "pluto"|> User}
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




[<Test>]    
let ``Can load my Wall and that of all my followers`` () =
    let pippo = {User = "pippo"|> User; Follows = ["pluto" |> Followed; "paperino" |> Followed]; Posts = list.Empty}
    let pluto = {User = "pluto"|> User; Follows = List.Empty; Posts = List.Empty}
    let paperino = {pluto with User = "paperino"|> User}

    let loadWall user = 
        let (User u) = user
        match u with
        | "pippo" -> pippo
        | "pluto" -> pluto
        | "paperino" -> paperino
        | _ -> failwith "error"
    

    loadWalls' loadWall pippo |> should equal [pippo; pluto; paperino]



[<Test>]
let ``Can follow other user``() =
    let user = "pippo"
    let followed = "pluto"
    let wall = {User = user|> User; Follows = ["paperino" |> Followed]; Posts = list.Empty}
    addFollowed (Followed(followed)) wall |> should equal {wall with Follows = ["paperino" |> Followed; "pluto" |> Followed]}
    

           

[<Test>]
let ``Can execute command`` () = 
    let mutable called = false
    let rop = fun cmd -> called <- true        

    exec' rop "pippo" 

    called |> should be True

[<Test>]          
let ``Can execute post comand rop``() =
    let mutable called = false
    let rop = (fun a b -> called <- true; b |> should equal (User("pippo")); a |> should equal (Message("pluto")))
    let cmd = Post(User("pippo"), Message("pluto"))
    post' rop cmd |> should equal Done
    called |> should be True


[<Test>]          
let ``Can execute read comand rop``() =
    let mutable called = false
    let rop = fun r -> (called <- true; r |> should equal (User("pippo")))
    let cmd = Read(User("pippo"))
    read' rop cmd |> should equal Done
    called |> should be True


[<Test>]
let ``Can Execute follow command``() =
    let mutable called = false
    let rop = fun a b -> called <-true
    let cmd = Follows(User("pippo"), Followed( "pluto"))
    follow' rop cmd |> should equal Done
    called |> should be True

[<Test>]
let ``Can execute wall Command``() =
    let mutable called = false
    let rop = fun a -> ( called <- true; a |> should equal (User("pippo")))
    let cmd = Wall(User("pippo"))
    wall' rop cmd |> should equal Done
    called |> should be True

