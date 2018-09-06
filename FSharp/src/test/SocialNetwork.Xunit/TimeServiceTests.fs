module TimeServiceTests

open System


open Xunit
open FsUnit

let now () = DateTime.Now    

[<Theory>]
[<InlineData(2.1)>]
[<InlineData(20.3)>]
[<InlineData(30.3)>]
[<InlineData(40.8)>]
[<InlineData(59.2)>]
let ``Timeservice can give a nice Time for seconds`` (seconds: float) =
    now
    |> TimeService.niceTime'
    <| now().AddSeconds(-seconds)
    |> should equal (seconds |> int |> sprintf "%i seconds ago")

[<Theory>]
[<InlineData(126.9, 2)>]
[<InlineData(300.0, 5)>]
[<InlineData(299.2, 4)>]
[<InlineData(601.2, 10)>]
let ``Timeservice can give a nice Time for minutes`` (seconds: float) (minutes: int) =    
    now
    |>  TimeService.niceTime' 
    <| now().AddMinutes(-minutes |> float)
    |> should equal (minutes |> sprintf "%i minutes ago")
