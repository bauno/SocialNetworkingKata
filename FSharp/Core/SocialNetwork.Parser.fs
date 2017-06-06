module SocialNetwork.Parser

open SocialNetwork.Commands

open System
open System.Text.RegularExpressions


let parsePostCommand cmdStr =
    if Regex.IsMatch(cmdStr, postPattern) then
        let matches = Regex.Match(cmdStr, postPattern)
        let follower = matches.Groups.[1].Value
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

let parseFollowCommand cmdStr = 
    if Regex.IsMatch(cmdStr, followPattern) then
        let matches = Regex.Match(cmdStr, followPattern)
        let follower = matches.Groups.[1].Value
        let followed = matches.Groups.[2].Value
        Follows(follower, followed)
    else Invalid(cmdStr)

let parseWallCommand cmdStr =     
    if Regex.IsMatch(cmdStr, wallPattern) then
        let matches = Regex.Match(cmdStr, wallPattern)
        let user = matches.Groups.[1].Value
        Wall(user)
    else Invalid(cmdStr)

let (|PostStr|ReadStr|WallStr|FollowStr|InvalidStr|) input =
        if Regex.IsMatch(input, postPattern) then PostStr(input)
        elif Regex.IsMatch(input, readPattern) then ReadStr(input)
        elif Regex.IsMatch(input, wallPattern) then WallStr(input)
        elif Regex.IsMatch(input, followPattern) then FollowStr(input)
        else InvalidStr(input)

let parse cmdString =
    match cmdString with
    | PostStr input -> parsePostCommand input
    | ReadStr input -> parseReadCommand input
    | FollowStr input -> parseFollowCommand input
    | WallStr input -> parseWallCommand input
    | _ -> Invalid(cmdString)
