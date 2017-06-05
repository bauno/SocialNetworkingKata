module TimeService

open System

let mutable internal testNow: DateTime option = None

let Now () = 
    testNow 
    |> function
    | None -> DateTime.Now
    | Some t -> t    





