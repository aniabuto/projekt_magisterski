namespace Shared.Types

open System

type Genre = {
    Id : int
    Name : string option
    Description : string option
}

type Artist = {
    Id : int
    Name : string option
    Bio : string option
}

type Album = {
    Id : int
    mutable GenreId : int
    mutable ArtistId : int
    mutable Title : string
    mutable Price : decimal
    Thumbnail : string option
}

type Order = {
    Id : int
    OrderDate : DateTime
    Username : string option
    FirstName : string option
    LastName : string option
    Address : string option
    City : string option
    State : string option
    PostalCode : string option
    Country : string option
    Phone : string option
    Email : string option
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
    Thumbnail : string option
    Price : decimal
    Title : string
    ArtistName : string option
    GenreName : string option
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
    Thumbnail : string option
    Count : int
}