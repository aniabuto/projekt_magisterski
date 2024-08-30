module ServerProject.TypeConverter

open ServerProject.Db
open Shared.Types

let genreEntityToType (genre : DB.dataContext.``public.genresEntity``) =
    {
        Id = genre.Id
        Name =  genre.Name
        Description =  genre.Description
    }

let artistEntityToType (artist : DB.dataContext.``public.artistsEntity``) =
    {
       Id = artist.Id
       Name = artist.Name
       Bio = artist.Bio
    }

let albumEntityToType (album : DB.dataContext.``public.albumsEntity``) =
    {
        Id = album.Id
        ArtistId =  album.ArtistId
        GenreId =  album.GenreId
        Title =  album.Title
        Price =  album.Price
        Thumbnail =  album.Thumbnail
    }

let orderEntityToType (order : DB.dataContext.``public.ordersEntity``) =
    {
        Id = order.Id
        OrderDate = order.Date
        Username = order.Username
        FirstName = order.Firstname
        LastName = order.Lastname
        Address = order.Address
        City = order.City
        State = order.State
        PostalCode = order.PostalCode
        Country = order.Country
        Phone = order.Phone
        Email = order.Email
        Total = order.Total
    }

let orderDetailsEntityToType (orderDetails : DB.dataContext.``public.orderdetailsEntity``) =
    {
        Id = orderDetails.Id
        OrderId = orderDetails.OrderId
        AlbumId = orderDetails.AlbumId
        Quantity = orderDetails.Quantity
        UnitPrice = orderDetails.UnitPrice
    }

let cartEntityToType (cart : DB.dataContext.``public.cartsEntity``) =
    {
        RecordId = cart.Id
        CartId = cart.CartId
        AlbumId = cart.AlbumId
        Count = cart.Count
        DateCreated = cart.DateCreated
    }

let userEntityToType (user : DB.dataContext.``public.usersEntity``) =
    {
        Id = user.Id
        Username = user.Username
        Email = user.Email
        Password = user.Password
        Role = user.Role
    }

let albumDetailsEntityToType (album : DB.dataContext.``public.albumdetailsEntity``) =
    {
        AlbumId = album.Id
        Thumbnail = album.Thumbnail
        Price = album.Price
        Title = album.Title
        ArtistName = album.Artist
        GenreName = album.Genre
    }

let cartDetailsEntityToType (cart : DB.dataContext.``public.cartdetailsEntity``) =
    {
        CartId = cart.CartId
        Count = cart.Count
        AlbumTitle = cart.AlbumTitle
        AlbumId = cart.AlbumId
        Price = cart.Price
    }

let bestsellerEntityToType (bestseller : DB.dataContext.``public.bestsellersEntity``) =
    {
        AlbumId = bestseller.Id
        Title = bestseller.Title
        Thumbnail = bestseller.Thumbnail
        Count = bestseller.Count
    }