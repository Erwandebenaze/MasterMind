module Start

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open FSharp.Data

let sample : WebPart = 
    path "/hello" >=> choose [
      GET  >=> OK "GET hello"
      POST >=> OK "POST hello"
      RequestErrors.NOT_FOUND "Found no handlers" ]

let other : WebPart = 
    path "/" >=> choose [
        GET >=> OK "GET Other"
        POST >=> OK "POST Other"
    ]

let test : WebPart = 
    path "/test" >=> choose [
        GET >=> OK "fiez"
    ]

type GameData = {
    Players: string[];
    Turn: int;
    Grid: int[]
}

//let wr = new System.IO.StreamWriter("data/tristan_letrou_game_updated.json")
//wr.Write (json<Game2> updatedGame)
//wr.Close()

let nextPlayer = fun [|a;b|] ->
    [|b;a|]

let gameData = JsonProvider<"data/tristan_letrou_game.json">.GetSample()

let currentGame = {
    Players = gameData.Players;
    Turn = gameData.Turn;
    Grid = gameData.Grid
}

let updatedGame = {
    currentGame with 
    Players = (*Array.rev currentGame.Players*) nextPlayer currentGame.Players
    Turn = currentGame.Turn + 1; 
    //Grid = testGame.Grid
}

printfn "%A" updatedGame