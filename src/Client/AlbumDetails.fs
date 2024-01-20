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
    DeletionConfirmation : bool
}

type Msg =
    | GotAlbumDetails of AlbumDetails option
    | RequestAlbumDetails of int
    | EditAlbum of int
    | DeleteAlbumWarning of int
    | CancelDeletion
    | DeleteAlbum of int
    | AlbumDeleted of unit
    | GoBack


let init id : Model * Cmd<Msg> =
    let model = {
        AlbumDetails = None
        AlbumId = id
        DeletionConfirmation = false
    }

    let cmd = Cmd.batch [
        Cmd.OfAsync.perform albumsApi.getAlbumDetails id GotAlbumDetails
    ]

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotAlbumDetails albums ->
         { model with AlbumDetails =   albums }, Cmd.none
     | EditAlbum id ->
         model, Cmd.navigate ("albums", id, "edit")
     | DeleteAlbumWarning id ->
         { model with DeletionConfirmation = true }, Cmd.none
     | CancelDeletion ->
         { model with DeletionConfirmation = false }, Cmd.none
     | DeleteAlbum id ->
         model, Cmd.OfAsyncImmediate.perform albumsApi.deleteAlbum id AlbumDeleted
     | AlbumDeleted () ->
         model, Cmd.navigateBack 1
     | GoBack ->
         model, Cmd.navigateBack 1
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

            if model.DeletionConfirmation then
                Bulma.modal [
                    if model.DeletionConfirmation then Bulma.modal.isActive
                    prop.id "modal-sample"
                    prop.children [
                        Bulma.modalBackground []
                        Bulma.modalContent [
                            Bulma.box [
                                Html.h1 "Confirm album deletion"
                                Html.button [
                                    text.hasTextCentered
                                    prop.style [style.marginLeft 10]
                                    prop.text "Delete"
                                    prop.onClick (fun _ -> DeleteAlbum model.AlbumId |> dispatch)
                                    color.hasTextDark
                                ]
                                Html.button [
                                    text.hasTextCentered
                                    prop.style [style.marginLeft 10]
                                    prop.text "Cancel"
                                    prop.onClick (fun _ -> CancelDeletion |> dispatch)
                                    color.hasTextDark
                                ]
                            ]
                        ]
                        Bulma.modalClose [
                            prop.onClick (fun _ -> CancelDeletion |> dispatch)
                        ]
                    ]
                ]

            Html.br[]
            Html.button [
                text.hasTextCentered
                prop.text "Go Back"
                prop.onClick (fun _ -> dispatch GoBack)
                color.hasTextDark
            ]
            Html.button [
                text.hasTextCentered
                prop.style [style.marginLeft 10]
                prop.text "Edit"
                prop.onClick (fun _ -> EditAlbum model.AlbumId |> dispatch)
                color.hasTextDark
            ]
            Html.button [
                text.hasTextCentered
                prop.style [style.marginLeft 10]
                prop.text "Delete"
                prop.onClick (fun _ -> DeleteAlbumWarning model.AlbumId |> dispatch)
                color.hasTextDark
            ]
        ]
    ]
