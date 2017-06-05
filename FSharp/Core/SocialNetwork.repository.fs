module SocialNetwork.Repository

open System.Collections.Generic

open SocialNetwork.Data

let private data = Dictionary<string, Wall>()

let save wall = 
    if data.ContainsKey(wall.User)
        then data.[wall.User] = wall             
    else data.Add(wall.User, wall)
         true

let loadWall user = 
    if data.ContainsKey(user)
    then data.[user]
    else {User = user; Follows = list.Empty; Posts = list.Empty}
