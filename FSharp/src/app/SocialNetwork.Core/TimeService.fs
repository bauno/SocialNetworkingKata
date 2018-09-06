module TimeService

open System

let now () =
  DateTime.Now

let internal niceTime' (now: unit -> DateTime) (timeStamp: DateTime) = 
  let delta = now().Subtract(timeStamp)
  if delta.TotalMinutes < 1.0
  then sprintf "%i seconds ago" (delta.TotalSeconds |> int)
  else sprintf "%i minutes ago" (delta.TotalMinutes |> int)