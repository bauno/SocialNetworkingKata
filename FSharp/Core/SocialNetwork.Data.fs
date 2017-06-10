module SocialNetwork.Data

open System

type User= User of string

let user (User user) = user

type Followed = Followed of string

let followed (Followed f) = f

type Message = Message of string

let message (Message m) = m


type Post ={
    Content: string
    TimeStamp: DateTime
    User: string
}


type Wall = {
    User: string
    Follows: string list
    Posts: Post list    
}

type PostView = {
    Content: string
    NiceTime: string    
    User: string
}