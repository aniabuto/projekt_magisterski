module Client.BestsellersList

open Fable.Remoting.Client
open Shared
open Shared.Types
open Elmish
open Client.Apis
open Feliz.Router

type Model = {Bestsellers : Bestseller list}

type Msg =
    | GotBestsellers of Bestseller list
    | GetDetails of int

let init () : Model * Cmd<Msg> =
    let model = { Bestsellers = [] }

    let cmd = Cmd.OfAsync.perform albumsApi.getBestsellers () GotBestsellers

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotBestsellers bestsellers -> { model with Bestsellers =  bestsellers }, Cmd.none
     | GetDetails id ->
         model, Cmd.navigate ("albums", "details", id, ["prev", "bestsellers"])


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

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            Bulma.subtitle [
                text.hasTextCentered
                prop.text "Bestsellers"
                color.hasTextDark
            ]
            Html.ol [
                for bestseller in model.Bestsellers do
                    Html.li [ prop.text bestseller.Title ]
            ]

            Bulma.table [
                prop.children [
                    Html.tr [
                        prop.children [
                            Html.th [
                                prop.text "Cover"
                            ]
                            Html.th [
                                prop.text "Title"
                            ]
                            Html.th [
                                prop.text "Sold"
                            ]
                            Html.th [
                                prop.text ""
                            ]
                        ]
                    ]
                    for album in model.Bestsellers do
                        Html.tr [
                            prop.children [
                                Html.td [
                                    prop.text (album.Thumbnail |> Option.get)
                                ]
                                Html.td [
                                    prop.text album.Title
                                ]
                                Html.td [
                                    prop.text album.Count
                                ]
                                Html.td [
                                    Html.button [
                                        prop.text "Details"
                                        prop.onClick (fun _ -> GetDetails album.AlbumId |> dispatch)
                                    ]
                                ]
                            ]
                        ]
                ]
            ]
        ]
    ]
