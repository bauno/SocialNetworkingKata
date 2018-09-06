#r "paket: nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target
nuget Fake.DotNet.AssemblyInfoFile //"
#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

let publishDir = "Publish"

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    ++ publishDir
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    AssemblyInfoFile.createFSharp "./src/app/SocialNetwork.Core/Properties/AssemblyInfo.fs"
        [AssemblyInfo.InternalsVisibleTo "SocialNetwork.Tests";
        AssemblyInfo.InternalsVisibleTo "SocialNetwork.Xunit" ]
    !! "src/app/**/*.fsproj"
    |> Seq.iter (DotNet.build id)
)

Target.create "Test" (fun _ ->
    !! "src/test/**/*.fsproj"
    |> Seq.iter (DotNet.test id)
)

Target.create "Publish" (fun _ ->
    "src/app/SocialNetwork.Main/SocialNetwork.Main.fsproj"
    |> DotNet.publish (fun options ->             
            {options with OutputPath =  Some(options.Common.WorkingDirectory + "/" + publishDir )}
        )
)

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Publish"
  ==> "All"

Target.runOrDefault "Test"
