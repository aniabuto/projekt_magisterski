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


let init id (guestApi: IGuestApi) : Model * Cmd<Msg> =
    let model = {
        AlbumDetails = None
        AlbumId = id
        DeletionConfirmation = false
    }

    let cmd = Cmd.batch [
        Cmd.OfAsync.perform guestApi.getAlbumDetails id GotAlbumDetails
    ]

    model, cmd

let update (authorizedApi : IAuthorizedApi) (msg: Msg) (model: Model) : Model * Cmd<Msg> =
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
         model, Cmd.OfAsyncImmediate.perform authorizedApi.deleteAlbum id AlbumDeleted
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

let albumDetailsView (albumDetailsOption : AlbumDetails option) id dispatch =
    match albumDetailsOption with
    | Some albumDetails ->
        Bulma.content [
            Bulma.subtitle [
            text.hasTextCentered
            prop.text albumDetails.Title
            color.hasTextDark
            ]
            Bulma.columns [
                Bulma.column [
                    Bulma.label [
                        prop.text $"Artist : %s{albumDetails.ArtistName}"
                        color.hasTextDark
                    ]
                    Bulma.label [
                        prop.text $"Genre : %s{albumDetails.GenreName}"
                        color.hasTextDark
                    ]
                    Bulma.label [
                        prop.text $"Price : %.2f{albumDetails.Price}"
                        color.hasTextDark
                    ]
                ]
                Bulma.column [
                    Html.img [
                        prop.src albumDetails.Thumbnail
                    ]
                ]
            ]
        ]
    | None ->
        Bulma.column[
            prop.onLoad (fun _ -> RequestAlbumDetails id |> dispatch)
        ]

let deletionConfirmationView (deletionConfirmation : bool) (albumId : int) dispatch =
    Bulma.modal [
        if deletionConfirmation then Bulma.modal.isActive
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
                        prop.onClick (fun _ -> DeleteAlbum albumId |> dispatch)
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

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            albumDetailsView model.AlbumDetails model.AlbumId dispatch

            deletionConfirmationView model.DeletionConfirmation model.AlbumId dispatch

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
