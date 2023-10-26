module Shared.Album

open System
open Shared.Types

let create (title : string) (records : Record list) (mainArtist : Artist) (year : int option) =
    {
        Id = Guid.NewGuid()
        Title = title
        Records = records
        MainArtist = mainArtist
        Year = year
    }
let isValidYear (year : int option) =
    match year with
    | None -> true
    | Some y -> not (y > DateTime.Today.Year)

let isValidRecords (records : Record list) =
    match records with
    | [] -> false
    | _ -> true