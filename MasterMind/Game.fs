module Game

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open FSharp.Data
open Lib

let handleSetSolution = fun s -> 
    let solution = String.split ',' s
    printfn "%A" solution
    OK ""

let solution = 
    path "/setsolution" >=> choose [
      POST  >=> request (fun r ->
            match r.formData "solution" with
            | Choice1Of2 s ->  handleSetSolution s
            | Choice2Of2 message -> RequestErrors.BAD_REQUEST message)
    ]
