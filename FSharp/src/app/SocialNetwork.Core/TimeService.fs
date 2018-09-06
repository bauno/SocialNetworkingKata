module TimeService

open System

let mutable internal testNow: DateTime option = None

let Now () = 
    testNow 
    |> function
    | None -> DateTime.Now
    | Some t -> t    

let now () = 
    DateTime.Now

let NiceTime' (now: unit -> DateTime) (timeStamp: DateTime) = 
  let delta = now().Subtract(timeStamp)
  if delta.TotalMinutes < 1.0
  then sprintf "%i seconds ago" (delta.TotalSeconds |> int)
  else sprintf "%i minutes ago" (delta.TotalMinutes |> int)

let NiceTime (timeStamp: DateTime) = 
  let delta = Now().Subtract(timeStamp)
  if delta.TotalMinutes < 1.0
  then sprintf "%i seconds ago" (delta.TotalSeconds |> int)
  else sprintf "%i minutes ago" (delta.TotalMinutes |> int)