namespace Shared.Types

open System

type Genre =
    | Pop
    | Rock
    | Classical

type Artist = {
    Id : Guid
    Name : string
    Surname : string
    DoB : DateOnly option
    DoD : DateOnly option
}

type Record = {
    Id : Guid
    Title : string
    Artists : Artist list
    Genre : Genre option
    Year : int option
}

type Album = {
    Id : Guid
    Title : string
    Records : Record list
    MainArtist : Artist
    Year : int option
}