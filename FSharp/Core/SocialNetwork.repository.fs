module internal SocialNetwork.Repository

open System.Collections.Generic

open SocialNetwork.Data

let private data = new Dictionary<string, Wall>()

let save (wall: Wall) =
    if data.ContainsKey(wall.User)
        then data.[wall.User] <- wall                          
    else data.Add(wall.User, wall)

let loadOrCreateWallOf user = 
    let (User u) = user
    if data.ContainsKey(u)
    then data.[u]
    else {User = u; Follows = list.Empty; Posts = list.Empty}

let internal clear () = data.Clear()