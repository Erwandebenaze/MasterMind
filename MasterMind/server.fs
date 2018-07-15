module Server

open System
open System.Threading
open Suave
open Suave.Operators
open System.IO
open Suave.Filters
open Login
open Game
Directory.SetCurrentDirectory("../../../")

[<EntryPoint>]


let main argv = 
  let cts = new CancellationTokenSource()
  let conf = { defaultConfig with cancellationToken = cts.Token }

  // Combine all the routing parts from other features into a single webpart
  let app : WebPart =
      choose [
        GET >=> path "/" >=> Files.file "public/index.html"
        login
        solution
        RequestErrors.NOT_FOUND "Page not found"
      ]
  
  let listening, server = startWebServerAsync conf app
    
  Async.Start(server, cts.Token)
  // Stop on any keypress
  Console.ReadKey true |> ignore
  cts.Cancel()
  0 // return an integer exit code
