module ServerProject.Repository

open FSharp.Data.Sql
open ServerProject.Db
open ServerProject.TypeConverter
open Shared.Types
open System


let getGenres (ctx : DB.dataContext) =
    query {
        for genre in ctx.Public.Genres do
            sortBy genre.Id
            select (genre |> genreEntityToType)
    } |> List.executeQueryAsync

let getAlbumsForGenre genreName (ctx : DB.dataContext) =
    query {
        for album in ctx.Public.Albums do
            join genre in ctx.Public.Genres on (album.GenreId = genre.Id)
            where (genre.Name = genreName)
            sortBy album.Id
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
            sortBy album.Id
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
            sortBy album.Id
            select (album |> albumEntityToType)
    }
    |> List.executeQueryAsync

let getBestsellers (ctx : DB.dataContext) =
    query {
        for bestseller in ctx.Public.Bestsellers do
            sortBy bestseller.Id
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
            ctx.SubmitUpdates()
        | None -> ()
    }

let deleteCart (cartId : string) (albumId : int) (ctx : DB.dataContext) =
    let foundCart = query {
        for c in ctx.Public.Carts do
        where (c.CartId = cartId && c.AlbumId = albumId)
        select (Some c)
        exactlyOneOrDefault
    }
    match foundCart with
    | Some foundCart ->
        foundCart.Delete()
        ctx.SubmitUpdates()
    | None -> ()

let getArtists (ctx : DB.dataContext) =
    query {
        for artist in ctx.Public.Artists do
            sortBy artist.Id
            select (artist |> artistEntityToType)
    }
    |> List.executeQueryAsync

let createAlbum (artistId, genreId, price, title, thumbnail) (ctx : DB.dataContext) =
    async {
        let newAlbum = ctx.Public.Albums.Create(artistId, genreId, price, title)
        newAlbum.Thumbnail <- thumbnail
        ctx.SubmitUpdates()
        return newAlbum.Id
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

let passHash (pass : string) =
    use sha = Security.Cryptography.SHA256.Create()
    Text.Encoding.UTF8.GetBytes(pass)
    |> sha.ComputeHash
    |> Array.map (fun b -> b.ToString("x2"))
    |> String.concat ""

let validateUserAsync (username, pass) (ctx : DB.dataContext) =
    let password = passHash pass
    query {
        for user in ctx.Public.Users do
            where (user.Username = username && user.Password = password)
            select (user |> userEntityToType)
    }
    |> Seq.tryHeadAsync

let validateUser (username, pass) (ctx : DB.dataContext) =
    let password = passHash pass
    query {
        for user in ctx.Public.Users do
            where (user.Username = username && user.Password = password)
            select (user |> userEntityToType)
    }
    |> Seq.tryHead

let getCart cartId albumId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Public.Carts do
            where (cart.CartId = cartId && cart.AlbumId = albumId)
            select cart
    }
    |> Seq.tryHeadAsync

let addToCart cartId albumId (ctx : DB.dataContext) =
    async {
        let foundCart =
            query {
                for cart in ctx.Public.Carts do
                    where (cart.CartId = cartId && cart.AlbumId = albumId)
                    select cart
            }|> Seq.tryHead
        match foundCart with
        | Some cart ->
            let newCount = cart.Count + 1
            cart.Count <- newCount
            ctx.SubmitUpdates()
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

let removeFromCart (cartId : string) (albumId : int) (ctx : DB.dataContext) =
    async {
        let foundCart =
            query {
                for cartE in ctx.Public.Carts do
                    where (cartE.CartId = cartId && cartE.AlbumId = albumId)
                    select cartE
            }|> Seq.tryHead
        match foundCart with
        | Some cart ->
            let newCount = cart.Count - 1
            cart.Count <- newCount
            if cart.Count = 0 then
                deleteCart cart.CartId cart.AlbumId ctx
            ctx.SubmitUpdates()
        | None -> ()
    }

let getCarts cartId (ctx : DB.dataContext) =
    query {
        for cart in ctx.Public.Carts do
            sortBy cart.Id
            where (cart.CartId = cartId)
            select cart
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
                deleteCart cart.CartId cart.AlbumId ctx
                ctx.SubmitUpdates()
            | None ->
                cart.CartId <- username
                ctx.SubmitUpdates()
        ctx.SubmitUpdates()
    }

let getUser username (ctx : DB.dataContext) =
    query {
        for user in ctx.Public.Users do
            where (user.Username = username)
            select (user |> userEntityToType)
    }
    |> Seq.tryHeadAsync

let newUser (username, pass, email) (ctx : DB.dataContext) =
    let password = passHash pass
    let user = ctx.Public.Users.Create(email, password, "user", username)
    ctx.SubmitUpdates()
    async {
        let u = user |> userEntityToType
        return {u with Password = pass }
    }

let placeOrder
    ((username : string), (firstName : string), (lastName : string), (address : string), (promoCode : bool))
    (ctx : DB.dataContext) =
        async {
            let! maybeCarts = getCartDetails username ctx
            let totalSum = maybeCarts |> List.sumBy (fun c -> decimal c.Count * c.Price)
            let total = if promoCode then totalSum * (decimal 0.8) else totalSum
            let order = ctx.Public.Orders.Create(System.DateTime.UtcNow, total)
            order.Username <- username
            order.Firstname <- firstName
            order.Lastname <- lastName
            order.Address <- address
            ctx.SubmitUpdates()
            for cart in maybeCarts do
                ctx.Public.Orderdetails.Create(
                    cart.AlbumId,
                    order.Id,
                    cart.Count,
                    cart.Price) |> ignore
                let! maybeCart = getCart cart.CartId cart.AlbumId ctx
                maybeCart |> Option.iter (fun c -> deleteCart c.CartId c.AlbumId ctx)
            ctx.SubmitUpdates()
        }