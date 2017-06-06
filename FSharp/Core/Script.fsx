#r "../build/Core.dll"

open SocialNetwork.CmdExec
open SocialNetwork.Parser
open SocialNetwork.Commands
open Core.Main
open Microsoft.FSharp.Reflection

let toString (x:'a) = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name


FSharpType.GetUnionCases typeof<Command>
|> Array.iter (fun case -> printfn "%s" case.Name)

// let functions =  ["Post", execPost; "Read", execRead] |> dict
let functions =  ["Post", post] |> dict


open System.Collections.Generic
let exec' (functions: IDictionary<string,(string -> string -> unit)>) cmdStr = 
     let cmd = cmdStr    
              |> parse    
              |> toString 
     //I hate this             
     functions.Item cmd

let exec = exec' functions

let postStr = "Alice -> Love"

exec postStr

post "Alice" "love"

