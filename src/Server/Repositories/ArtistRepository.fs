module Server.Repositories.ArtistRepository

open System
open Shared

let artists = ResizeArray<Types.Artist>()

let getArtistById (id : Guid) =
    artists |> Seq.tryFind (fun a -> a.Id = id)

let getArtistByName (name : string) (surname : string) =
    artists
    |> Seq.tryFind (fun a -> a.Name = name && a.Surname = surname)

let getArtistIdByName (name : string) (surname : string) =
    match getArtistByName name surname with
    | Some artist ->
        Ok(artist.Id)
    | None -> Error "Artist does not exist"

let addArtist (artist : Types.Artist) =
    match getArtistByName artist.Name artist.Surname with
    | Some _ -> Error "Artist already exists"
    | None ->
        artist
        |> artists.Add
        Ok()

let replaceArtist (id : Guid) (newArtist : Types.Artist) =
    match getArtistById id with
    | Some artist ->
        artist |> artists.Remove |> ignore
        Ok(newArtist |> addArtist)
    | None -> Error "Artist does not exist"

let editDateOfBirth (id : Guid) (date : DateOnly) =
    match getArtistById id with
    | Some oldArtist ->
        replaceArtist id {oldArtist with DoB = Some date }
    | None -> Error "Artist does not exist"

let editDateOfDeath (id : Guid) (date : DateOnly) =
    match getArtistById id with
    | Some oldArtist ->
        replaceArtist id {oldArtist with DoD = Some date }
    | None -> Error "Artist does not exist"

let removeArtist (id : Guid) =
    match getArtistById id with
    | Some artist ->
        Ok(artist |> artists.Remove)
    | None -> Error "Artist does not exist"

do
    addArtist (Artist.create "Chris" "Tomlin" None None) |> ignore
    let date : DateOnly = new DateOnly(2015, 10, 21)
    match getArtistIdByName "Chris" "Tomlin" with
    | Ok id ->
        editDateOfBirth (id) date |> ignore
    | Error _ -> None |> ignore