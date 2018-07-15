module Computation

open Suave

let OKCounter = 0

//let rec countOK = fun t -> 
//    match t with
//    | h1::t1, h2::t2, n when h1 = h2 -> let i = n + 1
//                                        countOK (t1, t2, i)
//    | _, _, n -> n

let countOK = fun t -> 
    let l1, l2 = t
    List.zip l1 l2 |> List.where (fun (c1, c2) -> c1 = c2)
                   |> List.length

let removeOK = fun t -> 
    let l1, l2 = t
    List.zip l1 l2 |> List.where (fun (c1, c2) -> c1 <> c2 && c1 <> null && c2 <> null)
                   |> List.unzip

let rec countColors = fun t ->
    let l1, l2 = removeOK t
    Set.count (Set.intersect (Set.ofList l1) (Set.ofList l2)) 

let rec exist l x = 
 match l with
   | [] -> false
   | head::tail -> 
       if x = head then true else exist tail x

let rec intersection = fun t ->
    let l1, l2 = t
    match l1 with
    | head::tail -> 
        let rest = intersection (tail, l2)
        if exist l2 head then head::rest
        else rest
    | [] -> []