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
    
    let post = {Content = "Figa"; TimeStamp = now.AddSeconds(-20.0)}
    let wall = {User = "Bauno"; Follows = list.Empty; Posts = [post] }
    
    let writtenWAll = write "Cazzo" wall    
    let modWall = {wall with Posts = wall.Posts@[{Content = "Cazzo"; TimeStamp = now}]}

    writtenWAll |> should equal modWall

[<Fact>]
let ``Can display post on display``() =
    let now = DateTime.Now
    TimeService.testNow <- Some now
    let wall = {
        User = "pippo"; 
        Follows = list.Empty
        Posts = [
            {Content = "Cazzo"; TimeStamp = now.AddSeconds(-10.0)}
            {Content = "Figa"; TimeStamp = now.AddSeconds(-5.0)}
        ]
    }

    let mutable lines = List<string>()
    let display line = lines.Add(line)

    displayOn display wall

    lines.First() |> should equal "Figa (5 seconds ago)"
    lines.Last() |> should equal "Cazzo (10 seconds ago)"


           

[<Fact>]
let ``Can execute command`` () = 
    let mutable called = false
    let rop = fun cmd -> called <- true        

    exec' rop "pippo" 

    called |> should be True

[<Fact>]          
let ``Can execute post comand rop``() =
    let rop = (fun a b -> ())
    let cmd = Post("pippo","pluto")
    post' rop cmd |> should equal Done


[<Fact>]          
let ``Can execute read comand rop``() =
    let rop = ignore    
    let cmd = Read("pippo")
    read' rop cmd |> should equal Done




