module ServerProject.Repository

open FSharp.Data.Sql
open ServerProject.Db
open ServerProject.TypeConverter
open Shared.Types


let getGenres (ctx : DB.dataContext) =
    query {
        for genre in ctx.Public.Genres do
            select (genre |> genreEntityToType)
    } |> List.executeQueryAsync

let getAlbumsForGenre genreName (ctx : DB.dataContext) =
    query {
        for album in ctx.Public.Albums do
            join genre in ctx.Public.Genres on (album.Genreid = genre.GenresId)
            where (genre.Name = genreName)
            select (album |> albumEntityToType)
    }
    |> List.executeQueryAsync

let getAlbumDetails id (ctx: DB.dataContext) =
    query {
        for album in ctx.Public.Albumdetails do
            where (album.Id = id)
            select (album |> albumDetailsEntityToType)
    }
    |> Seq.tryHeadAsync

let getAlbumsDetails (ctx : DB.dataContext) =
    query {
        for album in ctx.Public.Albumdetails do
            select (album |> albumDetailsEntityToType)
    }
    |> List.executeQueryAsync

let getAlbum id (ctx : DB.dataContext) =
    query {
        for album in ctx.Public.Albums do
            where (album.Id = id)
            select (album |> albumEntityToType)
    }
    |> Seq.tryHeadAsync

let getAlbumSync id (ctx : DB.dataContext) =
    query {
        for album in ctx.Public.Albums do
            where (album.Id = id)
            select (album |> albumEntityToType)
    }
    |> Seq.tryHead

let getAlbums (ctx : DB.dataContext) =
    query {
        for album in ctx.Public.Albums do
            select (album |> albumEntityToType)
    }
    |> List.executeQueryAsync

let getBestsellers (ctx : DB.dataContext) =
    query {
        for bestseller in ctx.Public.Bestsellers do
            select (bestseller |> bestsellerEntityToType)
    }
    |> List.executeQueryAsync

let deleteAlbum (id : int) (ctx : DB.dataContext) =
    async {
        let foundAlbum = query {
            for a in ctx.Public.Albums do
                where (a.Id = id)
                select (Some a)
                exactlyOneOrDefault
        }
        match foundAlbum with
        | Some foundAlbum ->
            foundAlbum.Delete()
            do! ctx.SubmitUpdatesAsync()
        | None -> ()
    }

let deleteCart (cart : Cart) (ctx : DB.dataContext) =
    let foundCart = query {
        for c in ctx.Public.Carts do
        where (c.CartId = cart.CartId)
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
        for artist in ctx.Public.Artists do
            select (artist |> artistEntityToType)
    }
    |> List.executeQueryAsync

let createAlbum (artistId, genreId, price, title) (ctx : DB.dataContext) =
    async {
        ctx.Public.Albums.Create(artistId, genreId, price, title) |> ignore
        ctx.SubmitUpdates()
    }

let updateAlbum (albumId : int) (title, price, thumbnail) (ctx : DB.dataContext) =
    async {
        let foundAlbum =
            query {
                for a in ctx.Public.Albums do
                    where (a.Id = albumId)
                    select a
            }|> Seq.tryHead
        match foundAlbum with
        | Some album ->
            album.Title <- title
            album.Price <- price
            album.Thumbnail <- thumbnail
            ctx.SubmitUpdates()
        | None -> ()
    }

let validateUser (username, password) (ctx : DB.dataContext) =
    query {
        for user in ctx.Public.Users do
            where (user.Username = username && user.Password = password)
            select (user |> userEntityToType)
    }
    |> Seq.tryHeadAsync

let getCart cartId albumId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Public.Carts do
            where (cart.CartId = cartId && cart.AlbumId = albumId)
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
            ctx.Public.Carts.Create(albumId, cartId, 1, System.DateTime.UtcNow) |> ignore
        ctx.SubmitUpdates()
    }

let getCartDetails cartId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Public.Cartdetails do
            where (cart.CartId = cartId)
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
        for cart in ctx.Public.Carts do
            where (cart.CartId = cartId)
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
        for user in ctx.Public.Users do
            where (user.Username = username)
            select (user |> userEntityToType)
    }
    |> Seq.tryHeadAsync

let newUser (username, password, email) (ctx : DB.dataContext) =
    let user = ctx.Public.Users.Create(email, password, "user", username)
    ctx.SubmitUpdates()
    async {
        return (user |> userEntityToType)
    }

let placeOrder (username : string) (ctx : DB.dataContext) =
    async {
        let! maybeCarts = getCartDetails username ctx
        let total = maybeCarts |> List.sumBy (fun c -> decimal c.Count * c.Price)
        let order = ctx.Public.Orders.Create(System.DateTime.UtcNow, total)
        order.Username <- username
        ctx.SubmitUpdates()
        for cart in maybeCarts do
            ctx.Public.Orderdetails.Create(
                cart.AlbumId,
                order.Id,
                cart.Count,
                cart.Price) |> ignore
            let! maybeCart = getCart cart.CartId cart.AlbumId ctx
            maybeCart |> Option.iter (fun c -> deleteCart c ctx)
        ctx.SubmitUpdates()
    }