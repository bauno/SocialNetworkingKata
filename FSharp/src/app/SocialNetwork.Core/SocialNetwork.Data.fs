module SocialNetwork.Data

open System

type User= User of string

let xu (User user) = user

type Followed = Followed of User

let xf (Followed f) = f 

type Message = Message of string

let xm (Message m) = m


type Post ={
    Content: Message
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