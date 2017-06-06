module SocialNetwork.Repository

open System.Collections.Generic

open SocialNetwork.Data

let private data = new Dictionary<string, Wall>()

let save (wall: Wall) = 
    if data.ContainsKey(wall.User)
        then data.[wall.User] <- wall                          
    else data.Add(wall.User, wall)

let loadOrCreateWallOf user = 
    if data.ContainsKey(user)
    then data.[user]
    else {User = user; Follows = list.Empty; Posts = list.Empty}
