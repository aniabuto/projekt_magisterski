module Client.BestsellersList

open Fable.Remoting.Client
open Shared
open Shared.Types
open Elmish
open Client.Apis

type Model = {Bestsellers : Bestseller list}

type Msg =
    | GotBestsellers of Bestseller list

let init () : Model * Cmd<Msg> =
    let model = { Bestsellers = [] }

    let cmd = Cmd.OfAsync.perform albumsApi.getBestsellers () GotBestsellers

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotBestsellers bestsellers -> { model with Bestsellers =  bestsellers }, Cmd.none


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

let view (model: Model) (_: Msg -> unit) =
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
        ]
    ]
