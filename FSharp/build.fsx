// #r "paket:
// nuget Fake.Core.Target
// nuget Fake.IO.FileSystem
// nuget Fake.DotNet
// nuget Fake.DotNet.MSBuild //"
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
Fake.Core.Target.create "Clean" (fun _ ->
    Fake.IO.Shell.cleanDirs [buildDir; deployDir]
)

// Fake.Core.Target.create "Build" (fun _ ->
//     Fake.DotNet.MSBuild.AssemblyInfoFile.createFSharp "./Core/Properties/AssemblyInfo.fs"
//         [Fake.DotNet.AssemblyInfo.InternalsVisibleTo "Tests" ]
//     !! "/**/*.fsproj"
//     // compile all projects below src/app/
//     |> Fake.DotNet.MSBuild.runDebug id buildDir "Build"
//     |> Fake.Core.Trace.logItems "AppBuild-Output: "
// )

Fake.Core.Target.create "Build" (fun _ ->
    !! "./**/*.fsproj"
    |> Fake.DotNet.MSBuild.runDebug id buildDir "Build"
    |> Fake.Core.Trace.logItems "AppBuild-Output: "
)

// Fake.Core.Target.create "Test" (fun _ ->
//     !! "/**/build/Tests.dll"
//     |> Fake.DotNet.Testing.NUnit3.NUnit3Defaults (id)
// )

// Fake.Core.Target.create "Deploy" (fun _ ->
//     !! (buildDir + "/**/*.*")
//         -- "*.zip"
//         |> Fake.IO.Zip.createZip buildDir (deployDir + "ApplicationName." + version + ".zip")
// )

// Build order
"Clean"
  ==> "Build"
//   ==> "Test"
//   ==> "Deploy"

// start build
Fake.Core.Target.runOrDefault "Build"
