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
    }

type IAuthorizedApi =
    {
        deleteAlbum : int -> Async<unit>
        createAlbum : int * int * decimal * string * string -> Async<int>
        updateAlbum : int * string * decimal * string -> Async<unit>
        getCart : string -> int -> Async<Cart option>
        addToCart : string -> int -> Async<unit>
        getCartDetails : string -> Async<CartDetails list>
        removeFromCart : Cart -> Async<unit>
        getCarts : string -> Async<Cart list>
        updateCarts : string * string -> Async<unit>
        placeOrder : string -> Async<unit>
    }