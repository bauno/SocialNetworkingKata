module SocialNetwork.Data

open System

type Post ={
    Content: string
    TimeStamp: DateTime
}


type Wall = {
    User: string
    Follows: string list
    Posts: Post list    
}

type PostView = {
    Content: string
    NiceTime: string    
}