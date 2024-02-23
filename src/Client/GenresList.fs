module Client.GenresList

open Fable.Remoting.Client
open Shared
open Shared.Types
open Elmish
open Client.Apis

type Model = {Genres : Genre list}

type Msg =
    | GotGenres of Genre list

let init () : Model * Cmd<Msg> =
    let model = { Genres = [] }

    let cmd = Cmd.OfAsync.perform guestApi.getGenres () GotGenres

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotGenres genres -> { model with Genres =  genres }, Cmd.none


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
                prop.text "Genres"
                color.hasTextDark
            ]
            Html.ol [
                for genre in model.Genres do
                    Html.li [
                        prop.children [
                            Bulma.label [
                                prop.text genre.Name
                            ]
                        ]

                    ]
            ]
        ]
    ]
