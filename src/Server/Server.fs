module Server
//
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn
open Giraffe

open ServerProject
open Shared

let genresApi =
    let db = Db.createContext @"Data Source=.\SQLEXPRESS;Initial Catalog=SafeMusicStore;Integrated Security=SSPI;"
    {
        getGenres = fun () -> Controller.getGenres db
    }

let artistsApi =
    let db = Db.createContext @"Data Source=.\SQLEXPRESS;Initial Catalog=SafeMusicStore;Integrated Security=SSPI;"
    {
        getArtists = fun () -> Controller.getArtists db
    }

let albumsApi =
    let db = Db.createContext @"Data Source=.\SQLEXPRESS;Initial Catalog=SafeMusicStore;Integrated Security=SSPI;"
    {
        getAlbumsForGenre = fun genre -> Controller.getAlbumsForGenre (Some genre) db
        getAlbumDetails = fun id -> Controller.getAlbumDetails id db
        getAlbumsDetails = fun () -> Controller.getAlbumsDetails db
        getAlbum = fun id -> Controller.getAlbum id db
        getAlbums = fun () -> Controller.getAlbums db
        getBestsellers = fun () -> Controller.getBestsellers db
        deleteAlbum = fun album -> Controller.deleteAlbum album db
        createAlbum = fun (artistId, genreId, price, title) -> Controller.createAlbum (artistId, genreId, price, title) db
        updateAlbum = fun album (artistId, genreId, price, title) -> Controller.updateAlbum album (artistId, genreId, price, title) db
    }

let cartsApi =
    let db = Db.createContext @"Data Source=.\SQLEXPRESS;Initial Catalog=SafeMusicStore;Integrated Security=SSPI;"
    {
        getCart = fun cartId albumId -> Controller.getCart cartId albumId db
        addToCart = fun cartId albumId -> Controller.addToCart cartId albumId db
        getCartDetails = fun cartId -> Controller.getCartDetails cartId db
        removeFromCart = fun cart -> Controller.removeFromCart cart db
        getCarts = fun cartId -> Controller.getCarts cartId db
        updateCarts = fun (cartId, username) -> Controller.updateCarts (cartId, username) db
    }

let usersApi =
    let db = Db.createContext @"Data Source=.\SQLEXPRESS;Initial Catalog=SafeMusicStore;Integrated Security=SSPI;"
    {
        validateUser = fun (username, password) -> Controller.validateUser (username, password) db
        getUser = fun username -> Controller.getUser username db
        newUser = fun (username, password, email) -> Controller.newUser (username, password, email) db
    }

let ordersApi =
    let db = Db.createContext @"Data Source=.\SQLEXPRESS;Initial Catalog=SafeMusicStore;Integrated Security=SSPI;"
    {
        placeOrder = fun username -> Controller.placeOrder username db
    }

let webApp =
    choose [
        Remoting.createApi ()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue genresApi
        |> Remoting.buildHttpHandler

        Remoting.createApi ()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue artistsApi
        |> Remoting.buildHttpHandler

        Remoting.createApi ()
        |> Remoting.withDiagnosticsLogger (printfn "%s")
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue albumsApi
        |> Remoting.buildHttpHandler

        Remoting.createApi ()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue cartsApi
        |> Remoting.buildHttpHandler

        Remoting.createApi ()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue ordersApi
        |> Remoting.buildHttpHandler

        Remoting.createApi ()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue usersApi
        |> Remoting.buildHttpHandler

    ]

let app =
    application {
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
    }


run app
