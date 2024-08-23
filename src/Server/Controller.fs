module ServerProject.Controller

open System
open ServerProject.Db
open Shared.Types

let getGenres (ctx: DB.dataContext) =
    Repository.getGenres ctx

let getArtists (ctx : DB.dataContext) =
    Repository.getArtists ctx

let getAlbumsForGenre genreName (ctx : DB.dataContext) =
    try
        Repository.getAlbumsForGenre genreName ctx
    with
    | ex ->
        printfn $"An error occurred: %s{ex.Message}"
        raise ex

let getAlbumDetails id (ctx: DB.dataContext) =
    Repository.getAlbumDetails id ctx

let getAlbumsDetails (ctx : DB.dataContext) =
    Repository.getAlbumsDetails ctx

let getAlbum id (ctx : DB.dataContext) =
    Repository.getAlbum id ctx

let getAlbums (ctx : DB.dataContext) =
    Repository.getAlbums ctx

let getBestsellers (ctx : DB.dataContext) =
    Repository.getBestsellers ctx

let deleteAlbum (album : int) (ctx : DB.dataContext) =
    Repository.deleteAlbum album ctx

let createAlbum (artistId, genreId, price, title, thumbnail) (ctx : DB.dataContext) =
    Repository.createAlbum (artistId, genreId, price, title, thumbnail) ctx

let updateAlbum (albumId : int) (title, price, thumbnail) (ctx : DB.dataContext) =
    Repository.updateAlbum albumId (title, price, thumbnail) ctx

let getCart cartId albumId (ctx : DB.dataContext) =
    Repository.getCart cartId albumId ctx

let addToCart cartId albumId (ctx : DB.dataContext) =
    Repository.addToCart cartId albumId ctx

let getCartDetails cartId (ctx : DB.dataContext) =
    Repository.getCartDetails cartId ctx

let removeFromCart (cartId : string) (albumId : int) (ctx : DB.dataContext) =
    Repository.removeFromCart cartId albumId ctx

let getCarts cartId (ctx : DB.dataContext) =
    Repository.getCarts cartId ctx

let updateCarts (cartId : string, username :string) (ctx : DB.dataContext) =
    Repository.upgradeCarts (cartId, username) ctx

let validateUser (username, password) (ctx : DB.dataContext) =
    Repository.validateUserAsync (username, password) ctx

let login (username, password) (ctx : DB.dataContext) =
    match Repository.validateUser (username, password) ctx with
    | Some user ->
            async{return Authorize.login { UserName = user.Username; Password = user.Password}}
    | None ->
        failwith $"User '{username}' can't be logged in"

let getUser username (ctx : DB.dataContext) =
    Repository.getUser username ctx

let newUser (username, password, email) (ctx : DB.dataContext) =
    Repository.newUser (username, password, email) ctx

let placeOrder
    (username : string, firstName : string, lastName : string, address : string, promoCode : string)
    (ctx : DB.dataContext) =
        Repository.placeOrder (username, firstName, lastName, address, promoCode = "1234") ctx