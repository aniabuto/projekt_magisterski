namespace Shared.Types

open System

type Genre = {
    Id : int
    Name : string
    Description : string
}

type Artist = {
    Id : int
    Name : string
    Bio : string
}

type Album = {
    Id : int
    mutable GenreId : int
    mutable ArtistId : int
    mutable Title : string
    mutable Price : decimal
    Thumbnail : string
}

type Order = {
    Id : int
    OrderDate : DateTime
    Username : string
    FirstName : string
    LastName : string
    Address : string
    City : string
    State : string
    PostalCode : string
    Country : string
    Phone : string
    Email : string
    Total : decimal
}

type OrderDetails = {
    Id : int
    OrderId : int
    AlbumId : int
    Quantity : int
    UnitPrice : decimal
}

type Cart = {
    RecordId : int
    mutable CartId : string
    AlbumId : int
    mutable Count : int
    DateCreated : DateTime
}

type User = {
    Id : int
    Username : string
    Email : string
    Password : string
    Role : string
}

// From Views

type AlbumDetails = {
    AlbumId : int
    Thumbnail : string
    Price : decimal
    Title : string
    ArtistName : string
    GenreName : string
}

type CartDetails = {
    CartId : string
    Count : int
    AlbumTitle : string
    AlbumId : int
    Price : decimal
}

type Bestseller = {
    AlbumId : int
    Title : string
    Thumbnail : string
    Count : int64
}