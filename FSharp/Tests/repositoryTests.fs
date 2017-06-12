module Repository.Test

open SocialNetwork.Repository
open SocialNetwork.Data

open FsUnit
open NUnit.Framework


[<Test>]
let ``Can crud wall`` () =
    let user = User "pippo";
    let emptyWall = user  |>  loadOrCreateWallOf 

    emptyWall.User |>  should equal user    
    emptyWall.Follows |> should be Empty   
    emptyWall.Posts |> should be Empty

    let now = System.DateTime.Now
    TimeService.testNow <- Some now    
    let post = {Content = Message"topolino"; TimeStamp = now; User = "pippo" |> User}
    let fullWall = {emptyWall with Follows = ["pluto" |> Followed]; Posts = [post]}
    save fullWall
    let savedWall = user |> loadOrCreateWallOf     

    savedWall.User  |> should equal user
    savedWall.Follows |> should equal ["pluto" |> Followed]
    savedWall.Posts |> should equal [post]

    let newPost = {Content = Message"clarabella"; TimeStamp = now.AddSeconds(-10.0); User = "pippo" |> User }

    let updatedWall = {
        savedWall with 
                  Posts = savedWall.Posts @ [newPost]; 
                  Follows = savedWall.Follows @ ["paperino" |> Followed]
                  }

    save updatedWall

    let lastWall = loadOrCreateWallOf user

    lastWall.User  |> should equal user
    lastWall.Follows |> should equal ["pluto" |> Followed; "paperino" |> Followed]
    lastWall.Posts |> should equal [post; newPost]
     

