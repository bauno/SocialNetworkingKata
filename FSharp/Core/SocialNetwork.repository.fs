module internal SocialNetwork.Repository

open System.Collections.Generic

open SocialNetwork.Data

let private data = new Dictionary<string, Wall>()

let save (wall: Wall) =
    let user = wall.User |> xUser
    if data.ContainsKey(user)
        then data.[user] <- wall                          
    else data.Add(user, wall)

let loadOrCreateWallOf user = 
    let u = xUser user 
    if data.ContainsKey(u)
    then data.[u]
    else {User = user; Follows = list.Empty; Posts = list.Empty}

let internal clear () = data.Clear()