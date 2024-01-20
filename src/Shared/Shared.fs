namespace Shared

open Microsoft.FSharp.Core
open Shared.Types

module Route =
    let builder typeName methodName =
        $"/api/%s{typeName}/%s{methodName}"

type IGenresApi =
    {
        getGenres : unit -> Async<Genre list>
    }

type IArtistsApi =
    {
        getArtists : unit -> Async<Artist list>
    }

type IAlbumsApi =
    {
        getAlbumsForGenre : string -> Async<Album list>
        getAlbumDetails : int -> Async<AlbumDetails option>
        getAlbumsDetails : unit -> Async<AlbumDetails list>
        getAlbum : int -> Async<Album option>
        getAlbums : unit -> Async<Album list>
        getBestsellers : unit -> Async<Bestseller list>
        deleteAlbum : int -> Async<unit>
        createAlbum : int * int * decimal * string * string -> Async<int>
        updateAlbum : int * string * decimal * string -> Async<unit>
    }

type ICartsApi =
    {
        getCart : string -> int -> Async<Cart option>
        addToCart : string -> int -> Async<unit>
        getCartDetails : string -> Async<CartDetails list>
        removeFromCart : Cart -> Async<unit>
        getCarts : string -> Async<Cart list>
        updateCarts : string * string -> Async<unit>
    }

type IUsersApi =
    {
        validateUser : string * string -> Async<User option>
        getUser : string -> Async<User option>
        newUser : string * string * string -> Async<User>
    }

type IOrdersApi =
    {
        placeOrder : string -> Async<unit>
    }