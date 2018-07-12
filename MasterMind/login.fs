module login

open Suave
open Suave.Filters
open Suave.Successful
open Suave.Operators
open Microsoft.FSharp.Collections

let mutable players : List<string> = []

let logPlayer (name:string) :WebPart =
    if(List.exists(fun p -> p = name) players) then
        RequestErrors.BAD_REQUEST "A player with this name already exists!"
    elif(List.length players > 1) then
        RequestErrors.FORBIDDEN "2 players are already in game!"
    else
        players <- players @ [name]
        OK ("Player " + name + " logged")

let login : WebPart = 
    path "/login" >=> choose [
      GET  >=> request (fun r ->
        match r.queryParam "name" with
        | Choice1Of2 name -> logPlayer name
        | Choice2Of2 message -> RequestErrors.BAD_REQUEST message)
      RequestErrors.NOT_FOUND "Found no handlers" ]
      