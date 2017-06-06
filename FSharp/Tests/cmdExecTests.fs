module CmdExec.Test

open SocialNetwork.Commands
open SocialNetwork.Data
open SocialNetwork.CmdExec

open Xunit
open FsUnit.Xunit

open System
open System.Collections.Generic
open System.Linq


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
let ``Can execute post command``() =
    let cmd = Post("Pippo", "Pluto")

    let mutable posted = false

    let post user message = 
       user |> should equal "Pippo"
       message |> should equal "Pluto"
       posted <- true

    cmd |> exec' post ignore

    posted |> should be True
           
[<Fact>]
let ``Can execute read command``() =
    let cmd = Read("Pippo")

    let mutable r = false

    let read user = 
       user |> should equal "Pippo"       
       r <- true
    
    let post user message = 
        failwith "Error"
    
    cmd |> exec' post read

    r |> should be True
           
