module Client.Index

open Elmish
open Feliz.Bulma
open Feliz
open Feliz.Router

type Page =
    | BestsellersList of BestsellersList.Model
    | GenresList of GenresList.Model
    | ArtistsList of ArtistsList.Model
    | AlbumsList of AlbumsList.Model
    | AlbumDetails of AlbumDetails.Model
    | NotFound

type Model = { CurrentPage : Page }

type Msg =
    | BestsellersListMsg of BestsellersList.Msg
    | GenresListMsg of GenresList.Msg
    | ArtistsListMsg of ArtistsList.Msg
    | AlbumsListMsg of AlbumsList.Msg
    | AlbumDetailsMsg of AlbumDetails.Msg
    | UrlChanged of string list

let initFromUrl url =
    match url with
    | ["bestsellers"] ->
        let bestsellersListModel, bestsellersListMsg = BestsellersList.init ()
        let model = { CurrentPage = BestsellersList bestsellersListModel }

        model, bestsellersListMsg |> Cmd.map BestsellersListMsg
    | ["genres"] ->
        let genresListModel, genresListMsg = GenresList.init ()
        let model = { CurrentPage = GenresList genresListModel }

        model, genresListMsg |> Cmd.map GenresListMsg
    | ["artists"] ->
        let artistsListModel, artistsListMsg = ArtistsList.init ()
        let model = { CurrentPage = ArtistsList artistsListModel }

        model, artistsListMsg |> Cmd.map ArtistsListMsg
    | ["albums"] ->
        let albumsListModel, albumsListMsg = AlbumsList.init ()
        let model = { CurrentPage = AlbumsList albumsListModel }

        model, albumsListMsg |> Cmd.map AlbumsListMsg
    | ["albums"; "details"; id] ->
        let albumsListModel, albumsListMsg = AlbumDetails.init (int id)
        let model = { CurrentPage = AlbumDetails albumsListModel }

        model, albumsListMsg |> Cmd.map AlbumDetailsMsg
    | _ -> { CurrentPage = NotFound }, Cmd.none


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
    | _, UrlChanged url -> initFromUrl url
    | _, _ -> model, Cmd.none


let init () : Model * Cmd<Msg> =
    Router.currentUrl ()
    |> initFromUrl


let containerBox model dispatch =
    match model.CurrentPage with
    | BestsellersList bestsellersModel -> BestsellersList.view bestsellersModel (BestsellersListMsg >> dispatch)
    | GenresList genresModel -> GenresList.view genresModel (GenresListMsg >> dispatch)
    | ArtistsList artistsModel -> ArtistsList.view artistsModel (ArtistsListMsg >> dispatch)
    | AlbumsList albumsModel -> AlbumsList.view albumsModel (AlbumsListMsg >> dispatch)
    | AlbumDetails albumsModel -> AlbumDetails.view albumsModel (AlbumDetailsMsg >> dispatch)
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
                            containerBox model dispatch
                        ]
                    ]
                ]
            ]
        ]
    ]
