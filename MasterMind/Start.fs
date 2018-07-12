module Start

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open FSharp.Data
open System.Linq
open Newtonsoft.Json.Linq
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

(*let jsonTest = """{
    "players": ["Tristan", "Letrou" ],
    "turn": 2,
    "grid" : [ 1, 1, 1, 1, 1 ]
}"""

let jsonTest2 = """[
    ["Tristan", "Letrou" ],
    2,
    [ 1, 1, 1, 1, 1 ]
]"""*)


type Game2 = {
    Players: string[];
    Turn: int;
    Grid: int[]
}

let gameData = JsonProvider<"data/tristan_letrou_game.json">.GetSample()
//let testGame = Game.GetSample()

let currentGame = {
    Players = gameData.Players;
    Turn = gameData.Turn;
    Grid = gameData.Grid
}

let updatedGame = {
    currentGame with 
    Players = currentGame.Players
    Turn = currentGame.Turn + 1; 
    //Grid = testGame.Grid
}

printfn "%A" updatedGame

//let testGame = JObject.Parse(jsonTest).Descendants()
//printfn "%A" (JObject.Parse(jsonTest).Descendants())
//printfn "%A" (JObject.Parse(jsonTest).Children())
//printfn "%A" (JObject.Parse(jsonTest).Properties())
//printfn "%A" (JObject.Parse(jsonTest).Value<string>( fun v -> v.ToString()))

//printfn "%A" testGame2

//let json : WebPart = 
//    path "/json" >=> choose [
//        GET >=> 
//    ]