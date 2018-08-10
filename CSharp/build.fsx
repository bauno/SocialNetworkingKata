#r "paket: nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target
nuget Fake.DotNet.Testing.NUnit
nuget Fake.DotNet.AssemblyInfoFile //"
#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    AssemblyInfoFile.createCSharp "./src/app/SocialNetwork.Core/Properties/AssemblyInfo.cs"
        [AssemblyInfo.InternalsVisibleTo "SocialNetwork.Tests.NUnit" ]
    !! "src/app/**/*.csproj"
    |> Seq.iter (DotNet.build id)
)

Target.create "Test" (fun _ ->
    !! "src/test/**/*.fsproj"
    |> Seq.iter (DotNet.test id)
)

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "All"

Target.runOrDefault "All"