#r "paket:
nuget FSharp.Core
nuget FAKE
nuget Fake.Core.Target
nuget Fake.IO.FileSystem
nuget Fake.IO.Zip
nuget Fake.DotNet
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.AssemblyInfoFile //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.DotNet    
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators
// open Fake.Testing

    // Directories
let buildDir  = "./build/"
let deployDir = "./deploy/"


// Filesets
let appReferences  =
    !! "/**/*.csproj"
      ++ "/**/*.fsproj"

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target.create "Clean" (fun _ ->
    Shell.cleanDirs [buildDir; deployDir]
)

Target.create "Build" (fun _ ->
    AssemblyInfoFile.createFSharp "./Core/Properties/AssemblyInfo.fs"
        [Fake.DotNet.AssemblyInfo.InternalsVisibleTo "Tests" ]
    !! "./**/*.fsproj"
    |> Fake.DotNet.MSBuild.runDebug id buildDir "Build"
    |> Fake.Core.Trace.logItems "AppBuild-Output: "
)

// Fake.Core.Target.create "Test" (fun _ ->
//     !! "/**/build/Tests.dll"
//     |> Fake.DotNet.Testing.NUnit3.NUnit3Defaults (id)
// )

Target.create "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
        -- "*.zip"
        |> Zip.createZip buildDir (deployDir + "SocialNetworkingKata." + version + ".zip") "Pippo" 3 false 
)

// Build order
"Clean"
  ==> "Build"
//   ==> "Test"
  ==> "Deploy"
  

// start build
Target.runOrDefault "Build"
