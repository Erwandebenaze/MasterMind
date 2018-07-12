module Start

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open System.Linq
open Newtonsoft.Json.Linq

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

let jsonTest = @"{
    'players': ['playerA', 'playerB' ],
    'turn': 5,
    'grid' : [ 3, 4, 8, 7, 3 ]
}"

type game = {
    players : array<string>
    turn: int
    grid: array<int>
}

let testGame = JObject.Parse(jsonTest).["players"].Select(fun a -> a.ToString())
printfn "%s" (testGame.ToString())

//printfn "%A" testGame2

//let json : WebPart = 
//    path "/json" >=> choose [
//        GET >=> 
//    ]