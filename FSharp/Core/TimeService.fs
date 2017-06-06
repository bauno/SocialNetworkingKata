module internal TimeService

open System

let mutable internal testNow: DateTime option = None

let Now () = 
    testNow 
    |> function
    | None -> DateTime.Now
    | Some t -> t    



let NiceTime (timeStamp: DateTime) = 
  let delta = Now().Subtract(timeStamp)
  if delta.TotalMinutes < 1.0
  then sprintf "%i seconds ago" ((int)delta.TotalSeconds)
  else sprintf "%i minutes ago" ((int)delta.TotalMinutes)