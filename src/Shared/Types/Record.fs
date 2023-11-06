module Shared.Record

open System
open Shared.Types

let create (title : string) (artists : Artist list) (year : int option) =
    {
        Id = Guid.NewGuid()
        Title = title
        Artists =  artists
        // Genre = genre
        Year = year
    }

let isValidYear (year : int option) =
    match year with
    | None -> true
    | Some y -> not (y > DateTime.Today.Year)

let isValidArtist (artists : Artist list) =
    match artists with
    | [] -> false
    | _ -> true