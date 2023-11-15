module ServerProject.TypeConverter

open ServerProject.Db
open Shared.Types

let genreEntityToType (genre : DB.dataContext.``dbo.genresEntity``) =
    {
        Id = genre.Id
        Name =  genre.Name
        Description =  genre.Description
    }

let artistEntityToType (artist : DB.dataContext.``dbo.artistsEntity``) =
    {
       Id = artist.Id
       Name = artist.Name
       Bio = artist.Bio
    }

let albumEntityToType (album : DB.dataContext.``dbo.albumsEntity``) =
    {
        Id = album.Id
        ArtistId =  album.Artistid
        GenreId =  album.Genreid
        Title =  album.Title
        Price =  album.Price
        Thumbnail =  album.Thumbnail
    }

let orderEntityToType (order : DB.dataContext.``dbo.ordersEntity``) =
    {
        Id = order.Id
        OrderDate = order.Orderdate
        Username = order.Username
        FirstName = order.Firstname
        LastName = order.Lastname
        Address = order.Address
        City = order.City
        State = order.State
        PostalCode = order.Postalcode
        Country = order.Country
        Phone = order.Phone
        Email = order.Email
        Total = order.Total
    }

let orderDetailsEntityToType (orderDetails : DB.dataContext.``dbo.orderdetailsEntity``) =
    {
        Id = orderDetails.Id
        OrderId = orderDetails.Orderid
        AlbumId = orderDetails.Albumid
        Quantity = orderDetails.Quantity
        UnitPrice = orderDetails.Unitprice
    }

let cartEntityToType (cart : DB.dataContext.``dbo.cartsEntity``) =
    {
        RecordId = cart.Recordid
        CartId = cart.Cartid
        AlbumId = cart.Albumid
        Count = cart.Count
        DateCreated = cart.Datecreated
    }

let userEntityToType (user : DB.dataContext.``dbo.usersEntity``) =
    {
        Id = user.Id
        Username = user.Username
        Email = user.Email
        Password = user.Password
        Role = user.Role
    }

let albumDetailsEntityToType (album : DB.dataContext.``dbo.albumdetailsEntity``) =
    {
        AlbumId = album.Id
        Thumbnail = album.Thumbnail
        Price = album.Price
        Title = album.Title
        ArtistName = album.Artist
        GenreName = album.Genre
    }

let cartDetailsEntityToType (cart : DB.dataContext.``dbo.cartdetailsEntity``) =
    {
        CartId = cart.Cartid
        Count = cart.Count
        AlbumTitle = cart.Albumtitle
        AlbumId = cart.Albumid
        Price = cart.Price
    }

let bestsellerEntityToType (bestseller : DB.dataContext.``dbo.bestsellersEntity``) =
    {
        AlbumId = bestseller.Id
        Title = bestseller.Title
        Thumbnail = bestseller.Thumbnail
        Count = bestseller.Count :?> int
    }