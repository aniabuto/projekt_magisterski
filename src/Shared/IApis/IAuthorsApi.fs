module Shared.IAuthorsApi

open System
open Shared.Types

type IAuthorsApi =
    {
        getArtists : unit -> Async<Artist list>
        addArtist : Artist -> Async<Artist>
        removeArtist : Guid -> Async<bool>
    }