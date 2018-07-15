module Login

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Microsoft.FSharp.Collections
open Types
open Lib

let mutable players : List<string> = []

let playerListToString = fun l -> List.map (fun e ->  "\"" + e.ToString() + "\"") l

let loginMessage = fun b -> fun p -> """{ "gamestarted": """ + String.toLowerInvariant (b.ToString()) +
                                     """ , "players": """ + String.replace ";" "," ((playerListToString p).ToString()) + "}"

let createGameFile = fun (player1, player2) -> 
                    let name = player1 + "_" + player2 + "_game"
                    let newGame = {
                        Players = [|player1;player2|]
                        Turn = 0;
                        Grid = [||]
                    }
                    let wr = new System.IO.StreamWriter("data/" + name + ".json")
                    wr.Write (json<GameData> newGame)
                    wr.Close()
                    loginMessage true players |> OK

let startGame = fun (player1,player2) ->
        match player1, player2 with
        | player1, player2 when String.toLowerInvariant(player1) = "morgan" || String.toLowerInvariant(player2) = "morgan" -> "Morgan wins!" |> OK
        | _, _ -> createGameFile (player1, player2)

let logPlayer (name:string) :WebPart =
    if(List.exists(fun p -> p = name) players) then
        RequestErrors.BAD_REQUEST "A player with this name already exists!"
    elif(List.length players > 1) then
        RequestErrors.FORBIDDEN "2 players are already in game!"
    else
        players <- players @ [name]
        if (List.length players = 2) then
            startGame (players.[0], players.[1])
        else
            loginMessage false players |> OK

let login : WebPart = 
    path "/login" >=> choose [
      GET  >=> request (fun r ->
        match r.queryParam "name" with
        | Choice1Of2 name -> logPlayer name
        | Choice2Of2 message -> RequestErrors.BAD_REQUEST message)
      ]
      