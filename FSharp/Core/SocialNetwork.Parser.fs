module SocialNetwork.Parser

open SocialNetwork.Commands

open System
open System.Text.RegularExpressions


let parsePostCommand cmdStr =
    if Regex.IsMatch(cmdStr, postPattern) then
        let matches = Regex.Match(cmdStr, postPattern)
        let user = matches.Groups.[1].Value
        let message = matches.Groups.[2].Value
        Post(user,message)
    else Invalid(cmdStr)

let parseReadCommand cmdStr =
  if Regex.IsMatch(cmdStr, readPattern) then
    let matches = Regex.Match(cmdStr, readPattern)
    let user = matches.Groups.[1].Value
    Read(user)
  else Invalid(cmdStr)

let (|PostStr|ReadStr|InvalidStr|) input =
        if Regex.IsMatch(input, postPattern) then PostStr(input)
        elif Regex.IsMatch(input, readPattern) then ReadStr(input)
        else InvalidStr(input)

let parse cmdString =

    match cmdString with
    | PostStr input -> parsePostCommand input
    | ReadStr input -> parseReadCommand input
    | _ -> Invalid(cmdString)
