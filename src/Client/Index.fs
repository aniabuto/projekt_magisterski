module Client.Index

open Elmish
open Feliz.Bulma
open Feliz
open Feliz.Router
open Feliz.ReactApi
open Shared.Types
open Client.Apis

type Page =
    | BestsellersList of BestsellersList.Model
    | GenresList of GenresList.Model
    | ArtistsList of ArtistsList.Model
    | AlbumsList of AlbumsList.Model
    | AlbumDetails of AlbumDetails.Model
    | AlbumEdit of AlbumEdit.Model
    | AlbumCreate of AlbumCreate.Model
    | NotFound

type Model = {
    CurrentPage : Page
    CurrentUser : User option
    ModalShown : bool
}

type Msg =
    | BestsellersListMsg of BestsellersList.Msg
    | GenresListMsg of GenresList.Msg
    | ArtistsListMsg of ArtistsList.Msg
    | AlbumsListMsg of AlbumsList.Msg
    | AlbumDetailsMsg of AlbumDetails.Msg
    | AlbumEditMsg of AlbumEdit.Msg
    | AlbumCreateMsg of AlbumCreate.Msg
    | UrlChanged of string list
    | ChangeUser of string
    | ChangedUser of User option
    | ToggleModal of bool

let initFromUrl url user =
    match url with
    | ["bestsellers"] ->
        let bestsellersListModel, bestsellersListMsg = BestsellersList.init ()
        let model = { CurrentPage = BestsellersList bestsellersListModel; CurrentUser = user; ModalShown = false }

        model, bestsellersListMsg |> Cmd.map BestsellersListMsg
    | ["genres"] ->
        let genresListModel, genresListMsg = GenresList.init ()
        let model = { CurrentPage = GenresList genresListModel; CurrentUser = user; ModalShown = false }

        model, genresListMsg |> Cmd.map GenresListMsg
    | ["artists"] ->
        let artistsListModel, artistsListMsg = ArtistsList.init ()
        let model = { CurrentPage = ArtistsList artistsListModel; CurrentUser = user; ModalShown = false }

        model, artistsListMsg |> Cmd.map ArtistsListMsg
    | ["albums"] ->
        let albumsListModel, albumsListMsg = AlbumsList.init ()
        let model = { CurrentPage = AlbumsList albumsListModel; CurrentUser = user; ModalShown = false }

        model, albumsListMsg |> Cmd.map AlbumsListMsg
    | ["albums"; Route.Int id] ->
        let albumsListModel, albumsListMsg = AlbumDetails.init id
        let model = { CurrentPage = AlbumDetails albumsListModel; CurrentUser = user; ModalShown = false }

        model, albumsListMsg |> Cmd.map AlbumDetailsMsg
    | ["albums"; Route.Int id; "edit"] ->
        let albumsListModel, albumsListMsg = AlbumEdit.init id
        let model = { CurrentPage = AlbumEdit albumsListModel; CurrentUser = user; ModalShown = false }

        model, albumsListMsg |> Cmd.map AlbumEditMsg
    | ["albums"; "create"] ->
        let albumsListModel, albumsListMsg = AlbumCreate.init ()
        let model = { CurrentPage = AlbumCreate albumsListModel; CurrentUser = user; ModalShown = false }

        model, albumsListMsg |> Cmd.map AlbumCreateMsg
    | _ -> { CurrentPage = NotFound; CurrentUser = user; ModalShown = false }, Cmd.none


let init () : Model * Cmd<Msg> =
    initFromUrl (Router.currentUrl ()) None

let update (message: Msg) (model: Model) : Model * Cmd<Msg> =
    match model.CurrentPage, message with
    | BestsellersList bestsellersList, BestsellersListMsg bestsellersListMessage ->
        let newBestsellersListModel, newCommand = BestsellersList.update bestsellersListMessage bestsellersList
        let model = { model with CurrentPage = BestsellersList newBestsellersListModel }

        model, newCommand |> Cmd.map BestsellersListMsg
    | GenresList genresList, GenresListMsg genresListMessage ->
        let newGenresListModel, newCommand = GenresList.update genresListMessage genresList
        let model = { model with CurrentPage = GenresList newGenresListModel }

        model, newCommand |> Cmd.map GenresListMsg
    | ArtistsList artistsList, ArtistsListMsg artistsListMessage ->
        let newArtistsListModel, newCommand = ArtistsList.update artistsListMessage artistsList
        let model = { model with CurrentPage = ArtistsList newArtistsListModel }

        model, newCommand |> Cmd.map ArtistsListMsg
    | AlbumsList albumsList, AlbumsListMsg albumsListMessage ->
        let newAlbumsListModel, newCommand = AlbumsList.update albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumsList newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumsListMsg
    | AlbumDetails albumsList, AlbumDetailsMsg albumsListMessage ->
        let newAlbumsListModel, newCommand = AlbumDetails.update albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumDetails newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumDetailsMsg
    | AlbumEdit albumsList, AlbumEditMsg albumsListMessage ->
        let newAlbumsListModel, newCommand = AlbumEdit.update albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumEdit newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumEditMsg
    | AlbumCreate albumsList, AlbumCreateMsg albumsListMessage ->
        let newAlbumsListModel, newCommand = AlbumCreate.update albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumCreate newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumCreateMsg
    | _, UrlChanged url -> initFromUrl url model.CurrentUser
    // | _, UrlChanged url -> initFromUrl url
    | _, ChangeUser username ->
        let cmd = Cmd.OfAsync.perform usersApi.getUser username ChangedUser
        model, cmd
    | _, ChangedUser user ->
        initFromUrl (Router.currentUrl ()) user
        // initFromUrl (Router.currentUrl ())
    | _, ToggleModal value ->
        {model with ModalShown = value }, Cmd.none
    | _, _ -> initFromUrl (Router.currentUrl ()) model.CurrentUser
    // | _, _ -> initFromUrl (Router.currentUrl ())



let containerBox model dispatch =
    match model.CurrentPage with
    | BestsellersList bestsellersModel -> BestsellersList.view bestsellersModel (BestsellersListMsg >> dispatch)
    | GenresList genresModel -> GenresList.view genresModel (GenresListMsg >> dispatch)
    | ArtistsList artistsModel -> ArtistsList.view artistsModel (ArtistsListMsg >> dispatch)
    | AlbumsList albumsModel -> AlbumsList.view albumsModel (AlbumsListMsg >> dispatch)
    | AlbumDetails albumsModel -> AlbumDetails.view albumsModel (AlbumDetailsMsg >> dispatch)
    | AlbumEdit albumsModel -> AlbumEdit.view albumsModel (AlbumEditMsg >> dispatch)
    | AlbumCreate albumsModel -> AlbumCreate.view albumsModel (AlbumCreateMsg >> dispatch)
    | NotFound -> Bulma.box "Page not found"

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

let toggleLogInModal (model : Model) =
    { model with ModalShown = true }



let view (model: Model) (dispatch: Msg -> unit) =
    React.router [
        router.onUrlChanged (UrlChanged >> dispatch)
        router.children [
            Bulma.hero [
                hero.isFullHeight
                color.isPrimary
                prop.children [
                    Bulma.heroHead [
                        Bulma.navbar [
                            Bulma.container [ navBrand ]
                        ]
                    ]
                    Bulma.heroBody [
                        Bulma.container [
                            Bulma.title [
                                text.hasTextCentered
                                prop.text "SAFE Music Store"
                            ]
                            match model.CurrentUser with
                            | Some user ->
                                Bulma.subtitle [
                                    text.hasTextCentered
                                    prop.text user.Username
                                ]
                                Html.button [
                                    prop.text "Log Out"
                                    prop.onClick (fun _ -> ChangeUser "" |> dispatch)
                                ]
                            | None ->
                                Bulma.button.button [
                                    prop.ariaHasPopup true
                                    prop.target "modal-sample"
                                    prop.text "Log in"
                                    prop.onClick (fun _ -> ToggleModal true |> dispatch)
                                ]
                                Bulma.modal [
                                    if model.ModalShown then Bulma.modal.isActive
                                    prop.id "modal-sample"
                                    prop.children [
                                        Bulma.modalBackground []
                                        Bulma.modalContent [
                                            Bulma.box [
                                                Html.h1 "Modal content"
                                            ]
                                        ]
                                        Bulma.modalClose [
                                            prop.onClick (fun _ -> ToggleModal false |> dispatch)
                                        ]
                                    ]
                                ]
                            containerBox model dispatch
                        ]
                    ]
                ]
            ]
        ]
    ]
