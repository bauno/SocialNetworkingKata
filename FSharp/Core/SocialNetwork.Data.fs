module SocialNetwork.Data

open System

type User= User of string

let xUser (User user) = user

type Followed = Followed of string

let xFollowed (Followed f) = f

type Message = Message of string

let xMessage (Message m) = m


type Post ={
    Content: string
    TimeStamp: DateTime
    User: User
}


type Wall = {
    User: User    
    Follows: Followed list
    Posts: Post list    
}

type PostView = {
    Content: string
    NiceTime: string    
    User: string
}