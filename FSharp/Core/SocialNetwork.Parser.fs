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

let parse cmdString = 
    
    let (|PostStr|InvalidStr|) input =
        if Regex.IsMatch(input, postPattern) then PostStr(input) 
        else InvalidStr(input)
    
    match cmdString with
    | PostStr input -> parsePostCommand input;
    | _ -> Invalid(cmdString)

    