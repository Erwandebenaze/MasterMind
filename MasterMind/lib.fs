module Lib

open System.IO
open System.Runtime.Serialization.Json
open System.Text

let json<'t> (myObj:'t) =   
        use ms = new MemoryStream() 
        (new DataContractJsonSerializer(typeof<'t>)).WriteObject(ms, myObj) 
        Encoding.Default.GetString(ms.ToArray())