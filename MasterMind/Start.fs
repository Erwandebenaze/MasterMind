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

let jsonTest = """{
    "players": ["Tristan", "Letrou" ],
    "turn": 2,
    "grid" : [ 1, 1, 1, 1, 1 ]
}"""

type Game = JsonProvider<"data/tristan_letrou_game.json">
let testGame = Game.GetSample()

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