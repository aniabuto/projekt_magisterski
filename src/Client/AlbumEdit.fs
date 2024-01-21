module Client.AlbumEdit

open System
open Fable.Form.Simple.Field
open Fable.Remoting.Client
open Feliz.Router
open Shared
open Shared.Types
open Elmish
open Fable.Form.Simple
open Fable.Form.Simple.Bulma

open Client.Apis

type Values = {
    Title : string
    Thumbnail : string
    Price : string
}

type Form = Form.View.Model<Values>

type Model = {
    AlbumDetails : AlbumDetails option
    AlbumId : int
    Form : Form
}

type Msg =
    | GotAlbumDetails of AlbumDetails option
    | RequestAlbumDetails of int
    | EditAlbum of string * decimal * string
    | FormChanged of Form
    | AlbumEdited of unit
    | GoBack


let init id : Model * Cmd<Msg> =
    let model = {
        AlbumDetails = None
        AlbumId = id
        Form = Form.View.idle { Title = ""; Thumbnail = ""; Price = string(Decimal.Zero) }
    }

    let cmd = Cmd.batch [
        Cmd.OfAsync.perform albumsApi.getAlbumDetails id GotAlbumDetails
    ]

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotAlbumDetails albums ->
         match albums with
         | Some album ->
             { model with
                AlbumDetails = Some album
                Form = Form.View.idle { Title = album.Title; Thumbnail = album.Thumbnail; Price = string(album.Price) }
             }, Cmd.none
         | None ->
                { model with
                    AlbumDetails = albums
                    Form = Form.View.idle { Title = ""; Thumbnail = ""; Price = string(Decimal.Zero) }
                }, Cmd.none
     | EditAlbum ( title, price, thumbnail) ->
         model, Cmd.OfAsyncImmediate.perform albumsAdminApi.updateAlbum (model.AlbumId, title, price, thumbnail) AlbumEdited
     | FormChanged form ->
         { model with Form = form }, Cmd.none
     | AlbumEdited () ->
         model, Cmd.navigateBack 1
     | GoBack ->
         model, Cmd.navigateBack 1
     | _ ->
         model, Cmd.none


open Feliz
open Feliz.Bulma

let form (title : string) (price : decimal) (thumbnail : string) : Form.Form<Values, Msg, _> =
    let titleField =
        Form.textField
            {
                Parser = Ok
                Value = fun values -> values.Title
                Update = fun newValue values -> { values with Title = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Album Title"
                        Placeholder = title
                        HtmlAttributes = []
                    }
            }
    let priceField =
        Form.textField
            {
                Parser = Ok
                Value = fun values -> values.Price
                Update = fun newValue values -> { values with Price = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Album Price"
                        Placeholder = string(price)
                        HtmlAttributes = []
                    }
            }
    let thumbnailField =
        Form.textField
            {
                Parser = Ok
                Value = fun values -> values.Thumbnail
                Update = fun newValue values -> { values with Thumbnail = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Album Thumbnail"
                        Placeholder = thumbnail
                        HtmlAttributes = []
                    }
            }
    let onSubmit =
        fun ftitle fprice fthumbnail ->
            EditAlbum (ftitle, decimal(fprice), fthumbnail)

    Form.succeed onSubmit
        |> Form.append titleField
        |> Form.append priceField
        |> Form.append thumbnailField


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
        match model.AlbumDetails with
        | Some albumDetails ->
            Form.View.asHtml
                {
                    Dispatch = dispatch
                    OnChange = FormChanged
                    Action = Form.View.Action.SubmitOnly "Edit Album"
                    Validation = Form.View.ValidateOnSubmit
                }
                (form albumDetails.Title albumDetails.Price albumDetails.Thumbnail)
                model.Form
        | None ->
            Bulma.column[
                prop.onLoad (fun _ -> RequestAlbumDetails model.AlbumId |> dispatch)
            ]
        Bulma.content [
            // match model.AlbumDetails with
            // | Some albumDetails ->
            // //     Bulma.subtitle [
            // //         text.hasTextCentered
            // //         prop.text albumDetails.Title
            // //         color.hasTextDark
            // //     ]
            // //     Bulma.columns [
            // //         Bulma.column [
            // //             Bulma.label [
            // //                 prop.text $"Artist : %s{albumDetails.ArtistName}"
            // //                 color.hasTextDark
            // //             ]
            // //             Bulma.label [
            // //                 prop.text $"Genre : %s{albumDetails.GenreName}"
            // //                 color.hasTextDark
            // //             ]
            // //             Bulma.label [
            // //                 prop.text $"Price : %.2f{albumDetails.Price}"
            // //                 color.hasTextDark
            // //             ]
            // //         ]
            // //         Bulma.column [
            // //             Html.img [
            // //                 prop.src albumDetails.Thumbnail
            // //             ]
            // //         ]
            // //     ]
            // //
            //
            //
            // | None ->
            // Bulma.column[
            // prop.onLoad (fun _ -> RequestAlbumDetails model.AlbumId |> dispatch)
            // ]



            Html.br[]
            Html.button [
                text.hasTextCentered
                prop.text "Go Back"
                prop.onClick (fun _ -> dispatch GoBack)
                color.hasTextDark
            ]
        ]
    ]
