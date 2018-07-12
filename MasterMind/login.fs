module login

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Microsoft.FSharp.Collections
open Types
open Lib

let mutable players : List<string> = []

let createGameFile = fun (player1,player2) ->
    let name = player1 + "_" + player2 + "_game"
    let newGame = {
        Players = [|player1;player2|]
        Turn = 0;
        Grid = [||]
    }
    let wr = new System.IO.StreamWriter("data/" + name + ".json")
    wr.Write (json<GameData> newGame)
    wr.Close()
    OK ("Game started !")

let logPlayer (name:string) :WebPart =
    if(List.exists(fun p -> p = name) players) then
        RequestErrors.BAD_REQUEST "A player with this name already exists!"
    elif(List.length players > 1) then
        RequestErrors.FORBIDDEN "2 players are already in game!"
    else
        players <- players @ [name]
        if (List.length players = 2) then
            createGameFile (players.[0], players.[1])
        else
            OK ("Player " + name + " logged")

let login : WebPart = 
    path "/login" >=> choose [
      GET  >=> request (fun r ->
        match r.queryParam "name" with
        | Choice1Of2 name -> logPlayer name
        | Choice2Of2 message -> RequestErrors.BAD_REQUEST message)
      RequestErrors.NOT_FOUND "Found no handlers" ]
      