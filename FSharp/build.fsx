#r "paket: nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target
nuget Fake.DotNet.Testing.NUnit
nuget Fake.DotNet.AssemblyInfoFile //"
#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.DotNet.Testing

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    AssemblyInfoFile.createFSharp "./src/SocialNetwork.Core/Properties/AssemblyInfo.fs"
        [AssemblyInfo.InternalsVisibleTo "SocialNetwork.Tests" ]
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

// Target.create "Test" (fun _ ->
//     !! ("./src/SocialNetwork.Tests" + "/bin/Release/netcoreapp2.0/SocialNetwork.Tests.dll")
//       |> DotNet (fun p ->
//         {p with
//             ShadowCopy = false;})
// )

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "All"

Target.runOrDefault "All"
