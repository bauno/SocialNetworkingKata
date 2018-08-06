#r "paket:
nuget FSharp.Core
nuget FAKE
nuget Fake.Core.Target
nuget Fake.IO.FileSystem
nuget Fake.IO.Zip
nuget Fake.DotNet
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.AssemblyInfoFile
nuget Fake.DotNet.Testing.NUnit
nuget fsunit //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.DotNet    
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet.Testing

    // Directories
let buildDir  = "./build/"
let testDir = "./build/"
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

Target.create "Test" (fun _ ->
    !! (testDir + "/Tests.dll")
      |> NUnit3.run (fun p ->
          {p with
                ShadowCopy = false })
)

Target.create "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
        -- "*.zip"
        |> Zip.createZip buildDir (deployDir + "SocialNetworkingKata." + version + ".zip") "Pippo" 3 false 
)

// Build order
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Deploy"
  

// start build
Target.runOrDefault "Test"
