module Client.AlbumsList

open Fable.Remoting.Client
open Shared
open Shared.Types
open Elmish
open Feliz.Router

open Client.Apis

type Model = {
    Albums : Album list
    AlbumsDetails : AlbumDetails list
    Genres : Genre list
}

type Msg =
    | GotAlbumsDetails of AlbumDetails list
    | GotGenres of Genre list
    | FilterByGenre of string
    | GotAlbumsByGenre of Album list
    | GetDetails of int


let init () : Model * Cmd<Msg> =
    let model = {
        Albums = []
        AlbumsDetails = []
        Genres = []
    }

    let cmd = Cmd.batch [
        Cmd.OfAsync.perform albumsApi.getAlbumsDetails () GotAlbumsDetails
        Cmd.OfAsync.perform genresApi.getGenres () GotGenres
    ]

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotAlbumsDetails albums ->
         { model with AlbumsDetails =   albums }, Cmd.none
     | GotGenres genres ->
         { model with Genres = genres }, Cmd.none
     | FilterByGenre genre ->
         if genre = "" then
             { model with Albums = [] }, Cmd.none
         else
             let cmd = Cmd.OfAsync.perform albumsApi.getAlbumsForGenre genre GotAlbumsByGenre
             model, cmd
     | GotAlbumsByGenre albums ->
         { model with Albums = albums }, Cmd.none
     | GetDetails id ->
         model, Cmd.navigate ("albums", id)
     | _ ->
         model, Cmd.none


open Feliz
open Feliz.Bulma

let navBrand =
    Bulma.navbarBrand.div [
        Bulma.navbarItem.a [
            prop.href "https://safe-stack.github.io/"
            navbarItem.isActive
            prop.children [
                Html.img [
                    prop.src "/favicon.png"
                    prop.alt "Logo"
                ]
            ]
        ]
    ]

let searchForAlbum id (albums : Album list) =
    if albums.IsEmpty then
        true
    else
        albums |> List.map (fun album -> album.Id) |> List.contains id

let albumsHeadRowView =
    Html.thead [
        prop.children [
            Html.th [
                prop.text "Title"
            ]
            Html.th [
                prop.text "ArtistName"
            ]
            Html.th [
                prop.text "GenreName"
            ]
            Html.th [
                prop.text "Price"
            ]
            Html.th [
                prop.text ""
            ]
        ]
    ]

let albumsRowView (album : AlbumDetails) (dispatch: Msg -> unit) =
    Html.tr [
        prop.children [
            Html.td [
            prop.text album.Title
            ]
            Html.td [
                prop.text (album.ArtistName |> Option.get)
            ]
            Html.td [
                prop.text (album.GenreName |> Option.get)
            ]
            Html.td [
                prop.text (string album.Price)
            ]
            Html.td [
                Html.button [
                    prop.text "Details"
                    prop.onClick (fun _ -> GetDetails album.AlbumId |> dispatch)
                ]
            ]
        ]
    ]


let genresListView (genresList : Genre list) (dispatch: Msg -> unit) =
    Bulma.select [
        prop.onChange (fun id -> FilterByGenre id |> dispatch )
        prop.children [
            Html.option [
                    prop.text ""
                ]
            for genre in genresList do
                Html.option [
                    match genre.Name with
                    | Some gn ->
                        prop.value gn
                        prop.text gn
                    | None -> failwith "todo"
                ]
        ]
    ]

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            Bulma.subtitle [
                text.hasTextCentered
                prop.text "Albums"
                color.hasTextDark
            ]
            genresListView model.Genres dispatch
            Bulma.table [
                prop.children [
                    albumsHeadRowView
                    Html.tbody[
                        for album in model.AlbumsDetails do
                            if searchForAlbum album.AlbumId model.Albums then
                                albumsRowView album dispatch
                    ]
                ]
            ]
        ]
    ]
