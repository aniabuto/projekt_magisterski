module Client.AlbumDetails

open Fable.Remoting.Client
open Feliz.Router
open Shared
open Shared.Types
open Elmish

open Client.Apis

type Model = {
    AlbumDetails : AlbumDetails option
    AlbumId : int
    Previous : string
}

type Msg =
    | GotAlbumDetails of AlbumDetails option
    | RequestAlbumDetails of int
    | GoBack


let init id prev : Model * Cmd<Msg> =
    let model = {
        AlbumDetails = None
        AlbumId = id
        Previous = prev
    }

    let cmd = Cmd.batch [
        Cmd.OfAsync.perform albumsApi.getAlbumDetails id GotAlbumDetails
    ]

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotAlbumDetails albums ->
         { model with AlbumDetails =   albums }, Cmd.none
     | GoBack ->
         model, Cmd.navigate(model.Previous)
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

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            match model.AlbumDetails with
            | Some albumDetails ->
                Bulma.subtitle [
                    text.hasTextCentered
                    prop.text albumDetails.Title
                    color.hasTextDark
                ]
                Bulma.columns [
                    Bulma.column [
                        match albumDetails.ArtistName with
                        | Some artistName ->
                            Bulma.label [
                                prop.text $"Artist : %s{artistName}"
                                color.hasTextDark
                            ]
                        | None ->
                            Bulma.label [
                                prop.text $"Artist :"
                                color.hasTextDark
                            ]
                        match albumDetails.GenreName with
                        | Some genreName ->
                            Bulma.label [
                                prop.text $"Genre : %s{genreName}"
                                color.hasTextDark
                            ]
                        | None ->
                            Bulma.label [
                                prop.text $"Genre :"
                                color.hasTextDark
                            ]
                        Bulma.label [
                            prop.text $"Price : %.2f{albumDetails.Price}"
                            color.hasTextDark
                        ]
                    ]
                    Bulma.column [
                        match albumDetails.Thumbnail with
                        | Some image ->
                            Html.img [
                                prop.src image
                            ]
                        | None -> failwith "no image path"
                    ]
                ]

            | None ->
                Bulma.column[
                    prop.onLoad (fun _ -> RequestAlbumDetails model.AlbumId |> dispatch)
                ]



            Html.br[]
            Html.button [
                text.hasTextCentered
                prop.text "Go Back"
                prop.onClick (fun _ -> dispatch GoBack)
                color.hasTextDark
            ]
        ]
    ]
