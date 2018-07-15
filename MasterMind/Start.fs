module Start

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open FSharp.Data
open Computation

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
    Solution: string[]
    Grid: string[][]
}

//let wr = new System.IO.StreamWriter("data/tristan_letrou_game_updated.json")
//wr.Write (json<Game2> updatedGame)
//wr.Close()

let nextPlayer = fun (arr: string[]) -> [| arr.[1]; arr.[0] |]

let gameData = JsonProvider<"data/model.json">.GetSample()

let currentGame = {
    Players = gameData.Players;
    Turn = gameData.Turn;
    Solution = gameData.Solution;
    Grid = gameData.Grid
}

let updatedGame = { currentGame with Players = (*Array.rev currentGame.Players*) nextPlayer currentGame.Players; Turn = currentGame.Turn + 1 (*Grid = countOK (Solution, Grid[1], 0 testGame.Grid*) }

let nbOfOK = countOK (Array.toList currentGame.Solution, Array.toList currentGame.Grid.[currentGame.Grid.Length - 1])

//let nbOfColors = countColors (Array.toList currentGame.Solution, Array.toList currentGame.Grid.[currentGame.Grid.Length - 1])

let newList = removeOK (Array.toList currentGame.Solution, Array.toList currentGame.Grid.[currentGame.Grid.Length - 1])

let intersec = intersection (Array.toList currentGame.Grid.[currentGame.Grid.Length - 1], Array.toList currentGame.Solution)

let nbOfColors = intersec

printfn "%A" updatedGame
printfn "%d" nbOfOK