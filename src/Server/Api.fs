module ServerProject.Api

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.AspNetCore.Http

open ServerProject
open Shared

module ErrorHandling =
    let rec getRealException (ex: Exception) =
        match ex with
        | :? AggregateException as ex -> getRealException ex.InnerException
        | _ -> ex

    let errorHandler<'a> (ex: exn) (routeInfo: RouteInfo<HttpContext>) = Propagate(getRealException ex)

let connectionString = Db.TPConnectionString

let guestApi =
    let db = Db.createContext connectionString
    {
        getGenres = fun () -> Controller.getGenres db
        getArtists = fun () -> Controller.getArtists db
        getAlbumsForGenre = fun genre -> Controller.getAlbumsForGenre (genre) db
        getAlbumDetails = fun id -> Controller.getAlbumDetails id db
        getAlbumsDetails = fun () -> Controller.getAlbumsDetails db
        getAlbum = fun id -> Controller.getAlbum id db
        getAlbums = fun () -> Controller.getAlbums db
        getBestsellers = fun () -> Controller.getBestsellers db
        validateUser = fun (username, password) -> Controller.validateUser (username, password) db
        getUser = fun username -> Controller.getUser username db
        newUser = fun (username, password, email) -> Controller.newUser (username, password, email) db
        login = fun (username, password) -> Controller.login (username, password) db
        addToCart = fun (cartId, albumId) -> Controller.addToCart cartId albumId db
        getCartDetails = fun cartId -> Controller.getCartDetails cartId db
        removeFromCart = fun (cartId, albumId) -> Controller.removeFromCart cartId albumId db
    }

let authorizedApi =
    let db = Db.createContext connectionString
    {
        placeOrder = fun (username, firstName, lastName, address, promoCode) ->
            Controller.placeOrder (username, firstName, lastName, address, promoCode) db
        updateCarts = fun (cartId, username) -> Controller.updateCarts (cartId, username) db
    }

let adminApi =
    let db = Db.createContext connectionString
    {
        deleteAlbum = fun album -> Controller.deleteAlbum album db
        createAlbum = fun (artistId, genreId, price, title, thumbnail) ->
            Controller.createAlbum (artistId, genreId, price, title, thumbnail) db
        updateAlbum = fun (albumId, title, price, thumbnail) ->
            Controller.updateAlbum albumId (title, price, thumbnail) db
    }