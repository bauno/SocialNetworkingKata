module TimeServiceTests

open System


open NUnit.Framework
open FsUnit

[<TestCase(2.1)>]
[<TestCase(20.3)>]
[<TestCase(30.3)>]
[<TestCase(40.8)>]
[<TestCase(59.2)>]
let ``Timeservice can give a nice Time for seconds`` (seconds: float) =
    let now = DateTime.Now
    TimeService.testNow <- (now |> Some)
    let postTs = now.AddSeconds(-seconds);    
    TimeService.NiceTime(postTs) |> should equal (sprintf "%i seconds ago" ((int)seconds))

[<TestCase(126.9, 2)>]
[<TestCase(300.0, 5)>]
[<TestCase(299.2, 4)>]
[<TestCase(601.2, 10)>]
let ``Timeservice can give a nice Time for minutes`` (seconds: float) (minutes: int) =
    let now = DateTime.Now
    TimeService.testNow <- (now |> Some)
    let postTs = now.AddSeconds(-seconds);    
    TimeService.NiceTime(postTs) |> should equal (sprintf "%i minutes ago" minutes)
