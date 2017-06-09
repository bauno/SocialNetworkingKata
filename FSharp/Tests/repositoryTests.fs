module Repository.Test

open SocialNetwork.Repository
open SocialNetwork.Data

open FsUnit
open NUnit.Framework


[<Test>]
let ``Can crud wall`` () =
    let user = "pippo";
    let emptyWall = loadOrCreateWallOf user

    emptyWall.User |> should equal user    
    emptyWall.Follows |> should be Empty   
    emptyWall.Posts |> should be Empty

    let now = System.DateTime.Now
    TimeService.testNow <- Some now    
    let post = {Content = "topolino"; TimeStamp = now; User = "pippo"}
    let fullWall = {emptyWall with Follows = ["pluto"]; Posts = [post]}
    save fullWall
    let savedWall = loadOrCreateWallOf user

    savedWall.User |> should equal user
    savedWall.Follows |> should equal ["pluto"]
    savedWall.Posts |> should equal [post]

    let newPost = {Content = "clarabella"; TimeStamp = now.AddSeconds(-10.0); User = "pippo"}

    let updatedWall = {
        savedWall with 
                  Posts = savedWall.Posts @ [newPost]; 
                  Follows = savedWall.Follows @ ["paperino"]
                  }

    save updatedWall

    let lastWall = loadOrCreateWallOf user

    lastWall.User |> should equal user
    lastWall.Follows |> should equal ["pluto"; "paperino"]
    lastWall.Posts |> should equal [post; newPost]
     

