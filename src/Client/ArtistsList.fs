module Client.ArtistsList

open Fable.Remoting.Client
open Shared
open Shared.Types
open Elmish
open Client.Apis

type Model = {Artists : Artist list}

type Msg =
    | GotArtists of Artist list

let init () : Model * Cmd<Msg> =
    let model = { Artists = [] }

    let cmd = Cmd.OfAsync.perform artistsApi.getArtists () GotArtists

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotArtists artists -> { model with Artists =  artists }, Cmd.none


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
                prop.text "Artists"
                color.hasTextDark
            ]
            Html.ol [
                for artist in model.Artists do
                    Html.li [
                        match artist.Name with
                        | Some n -> prop.text n
                        | None -> failwith "todo"
                    ]
            ]
        ]
    ]
