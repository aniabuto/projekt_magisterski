module Shared.IAlbumApi

open System
open Shared.Types

type IAlbumApi =
    {
        getAlbums : unit -> Async<Album list>
        addAlbum : Album -> Async<Album>
        addAlbumRecord : Record -> Async<Album>
        removeAlbumRecord : Record -> Async<Album>
        removeAlbum : Guid -> Async<bool>
    }