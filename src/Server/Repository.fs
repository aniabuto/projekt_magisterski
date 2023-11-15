module ServerProject.Repository

open FSharp.Data.Sql
open ServerProject.Db
open ServerProject.TypeConverter
open Shared.Types


let getGenres (ctx : DB.dataContext) =
    query {
        for genre in ctx.Dbo.Genres do
            select (genre |> genreEntityToType)
    } |> List.executeQueryAsync

let getAlbumsForGenre genreName (ctx : DB.dataContext) =
    query {
        for album in ctx.Dbo.Albums do
            join genre in ctx.Dbo.Genres on (album.Genreid = genre.Id)
            where (genre.Name = genreName)
            select (album |> albumEntityToType)
    }
    |> List.executeQueryAsync

let getAlbumDetails id (ctx: DB.dataContext) =
    query {
        for album in ctx.Dbo.Albumdetails do
            where (album.Id = id)
            select (album |> albumDetailsEntityToType)
    }
    |> Seq.tryHeadAsync

let getAlbumsDetails (ctx : DB.dataContext) =
    query {
        for album in ctx.Dbo.Albumdetails do
            select (album |> albumDetailsEntityToType)
    }
    |> List.executeQueryAsync

let getAlbum id (ctx : DB.dataContext) =
    query {
        for album in ctx.Dbo.Albums do
            where (album.Id = id)
            select (album |> albumEntityToType)
    }
    |> Seq.tryHeadAsync

let getAlbums (ctx : DB.dataContext) =
    query {
        for album in ctx.Dbo.Albums do
            select (album |> albumEntityToType)
    }
    |> List.executeQueryAsync

let getBestsellers (ctx : DB.dataContext) =
    query {
        for bestseller in ctx.Dbo.Bestsellers do
            select (bestseller |> bestsellerEntityToType)
    }
    |> List.executeQueryAsync

let deleteAlbum (album : Album) (ctx : DB.dataContext) =
    async {
        let foundAlbum = query {
            for a in ctx.Dbo.Albums do
                where (a.Id = album.Id)
                select (Some a)
                exactlyOneOrDefault
        }
        match foundAlbum with
        | Some foundAlbum ->
            foundAlbum.Delete |> ignore
            ctx.SubmitUpdates()
        | None -> ()
    }

let deleteCart (cart : Cart) (ctx : DB.dataContext) =
    let foundCart = query {
        for c in ctx.Dbo.Carts do
        where (c.Cartid = cart.CartId)
        select (Some c)
        exactlyOneOrDefault
    }
    match foundCart with
    | Some foundCart ->
        foundCart.Delete |> ignore
        ctx.SubmitUpdates()
    | None -> ()

let getArtists (ctx : DB.dataContext) =
    query {
        for artist in ctx.Dbo.Artists do
            select (artist |> artistEntityToType)
    }
    |> List.executeQueryAsync

let createAlbum (artistId, genreId, price, title) (ctx : DB.dataContext) =
    async {
        ctx.Dbo.Albums.Create(artistId, genreId, price, title) |> ignore
        ctx.SubmitUpdates()
    }

let updateAlbum (album : Album) (artistId, genreId, price, title) (ctx : DB.dataContext) =
    async {
        album.ArtistId <- artistId
        album.GenreId <- genreId
        album.Price <- price
        album.Title <- title
        ctx.SubmitUpdates()
    }

let validateUser (username, password) (ctx : DB.dataContext) =
    query {
        for user in ctx.Dbo.Users do
            where (user.Username = username && user.Password = password)
            select (user |> userEntityToType)
    }
    |> Seq.tryHeadAsync

let getCart cartId albumId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Dbo.Carts do
            where (cart.Cartid = cartId && cart.Albumid = albumId)
            select (cart |> cartEntityToType)
    }
    |> Seq.tryHeadAsync

let addToCart cartId albumId (ctx : DB.dataContext) =
    async {
        let! maybeCart = getCart cartId albumId ctx
        match maybeCart with
        | Some cart ->
            cart.Count <- cart.Count + 1
        | None ->
            ctx.Dbo.Carts.Create(albumId, cartId, 1, System.DateTime.UtcNow) |> ignore
        ctx.SubmitUpdates()
    }

let getCartDetails cartId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Dbo.Cartdetails do
            where (cart.Cartid = cartId)
            select (cart |> cartDetailsEntityToType)
    }
    |> List.executeQueryAsync

let removeFromCart (cart : Cart) (ctx : DB.dataContext) =
    async {
        cart.Count <- cart.Count - 1
        if cart.Count = 0 then
            deleteCart cart ctx
        ctx.SubmitUpdates()
    }

let getCarts cartId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Dbo.Carts do
            where (cart.Cartid = cartId)
            select (cart |> cartEntityToType)
    }
    |> List.executeQueryAsync

let upgradeCarts (cartId : string, username :string) (ctx : DB.dataContext) =
    async {
        let! maybeCarts = getCarts cartId ctx
        for cart in maybeCarts do
            let! maybeCart = getCart username cart.AlbumId ctx
            match maybeCart with
            | Some existing ->
                existing.Count <- existing.Count + cart.Count
                deleteCart cart ctx
            | None ->
                cart.CartId <- username
        ctx.SubmitUpdates()
    }

let getUser username (ctx : DB.dataContext) =
    query {
        for user in ctx.Dbo.Users do
            where (user.Username = username)
            select (user |> userEntityToType)
    }
    |> Seq.tryHeadAsync

let newUser (username, password, email) (ctx : DB.dataContext) =
    let user = ctx.Dbo.Users.Create(email, password, "user", username)
    ctx.SubmitUpdates()
    async {
        return (user |> userEntityToType)
    }

let placeOrder (username : string) (ctx : DB.dataContext) =
    async {
        let! maybeCarts = getCartDetails username ctx
        let total = maybeCarts |> List.sumBy (fun c -> decimal c.Count * c.Price)
        let order = ctx.Dbo.Orders.Create(System.DateTime.UtcNow, total)
        order.Username <- Some(username)
        ctx.SubmitUpdates()
        for cart in maybeCarts do
            ctx.Dbo.Orderdetails.Create(
                cart.AlbumId,
                order.Id,
                cart.Count,
                cart.Price) |> ignore
            let! maybeCart = getCart cart.CartId cart.AlbumId ctx
            maybeCart |> Option.iter (fun c -> deleteCart c ctx)
        ctx.SubmitUpdates()
    }