namespace Shared

open Microsoft.FSharp.Core
open Shared.Types

module Route =
    let builder typeName methodName =
        $"/api/%s{typeName}/%s{methodName}"

type IGuestApi =
    {
        getGenres : unit -> Async<Genre list>
        getArtists : unit -> Async<Artist list>
        getAlbumsForGenre : string -> Async<Album list>
        getAlbumDetails : int -> Async<AlbumDetails option>
        getAlbumsDetails : unit -> Async<AlbumDetails list>
        getAlbum : int -> Async<Album option>
        getAlbums : unit -> Async<Album list>
        getBestsellers : unit -> Async<Bestseller list>
        validateUser : string * string -> Async<User option>
        login : string * string -> Async<UserData>
        getUser : string -> Async<User option>
        newUser : string * string * string -> Async<User>
        addToCart : string * int -> Async<unit>
        getCartDetails : string -> Async<CartDetails list>
        removeFromCart : string * int -> Async<unit>
    }

type IAuthorizedApi =
    {
        updateCarts : string * string -> Async<unit>
        placeOrder : string * string * string * string * string -> Async<unit>
    }

type IAdminApi =
    {
        deleteAlbum : int -> Async<unit>
        createAlbum : int * int * decimal * string * string -> Async<int>
        updateAlbum : int * string * decimal * string -> Async<unit>
    }