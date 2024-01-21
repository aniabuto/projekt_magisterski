module Client.Index

open Elmish
open Fable.Import
open Feliz.Bulma
open Feliz
open Feliz.Router
open Feliz.ReactApi
open Shared.Types
open Client.Apis
open Fable.Form.Simple
open Fable.Form.Simple.Bulma
open Fable.Core.JsInterop

type Page =
    | BestsellersList of BestsellersList.Model
    | GenresList of GenresList.Model
    | ArtistsList of ArtistsList.Model
    | AlbumsList of AlbumsList.Model
    | AlbumDetails of AlbumDetails.Model
    | AlbumEdit of AlbumEdit.Model
    | AlbumCreate of AlbumCreate.Model
    | NotFound
    | NotAuthorized

type LoginValues = {
    LUsername : string
    LPassword : string
}

type LoginForm = Form.View.Model<LoginValues>

type RegisterValues = {
    Username : string
    Email : string
    Password : string
}

type RegisterForm = Form.View.Model<RegisterValues>

type Model = {
    CurrentPage : Page
    CurrentUser : User option
    LModalShown : bool
    RModalShown : bool
    LoginForm : LoginForm
    RegisterForm : RegisterForm
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
    | RegisteredUser of User
    | ToggleLModal of bool
    | ToggleRModal of bool
    | LoginFormChanged of LoginForm
    | RegisterFormChanged of RegisterForm
    | Login of string * string
    | Register of string * string * string
    | GoBack

let getCookieValue (cookieName : string) : string option =
    let cookieArray = Browser.Dom.document.cookie.Split [|';'|] |> Array.toList

    let rec findCookie (cookies : string list) =
        match cookies with
        | [] -> None
        | cookie :: rest ->
            let parts = cookie.Split [|'='|]
            if parts.Length = 2 && parts.[0].Trim() = cookieName then Some (parts.[1].Trim())
            else findCookie rest

    findCookie cookieArray

let setCookie(cookieName: string, cookieValue: string) =
    Browser.Dom.document.cookie <- sprintf "%s=%s" cookieName cookieValue

let initFromUrl url user =
    let model = {
                  CurrentPage = NotFound
                  CurrentUser = user
                  LModalShown = false
                  RModalShown = false
                  LoginForm = Form.View.idle { LUsername = ""; LPassword = "" }
                  RegisterForm = Form.View.idle { Username = ""; Email = ""; Password = "" } }

    match url with
    | ["bestsellers"] ->
        let bestsellersListModel, bestsellersListMsg = BestsellersList.init ()
        { model with CurrentPage = BestsellersList bestsellersListModel}, bestsellersListMsg |> Cmd.map BestsellersListMsg
    | ["genres"] ->
        match user with
        | Some _ ->
            let genresListModel, genresListMsg = GenresList.init ()
            { model with CurrentPage = GenresList genresListModel}, genresListMsg |> Cmd.map GenresListMsg
        | None -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | ["artists"] ->
        match user with
        | Some _ ->
            let artistsListModel, artistsListMsg = ArtistsList.init ()
            { model with CurrentPage = ArtistsList artistsListModel}, artistsListMsg |> Cmd.map ArtistsListMsg
        | None -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | ["albums"] ->
        let albumsListModel, albumsListMsg = AlbumsList.init ()
        { model with CurrentPage = AlbumsList albumsListModel}, albumsListMsg |> Cmd.map AlbumsListMsg
    | ["albums"; Route.Int id] ->
        let albumsListModel, albumsListMsg = AlbumDetails.init id
        { model with CurrentPage = AlbumDetails albumsListModel}, albumsListMsg |> Cmd.map AlbumDetailsMsg
    | ["albums"; Route.Int id; "edit"] ->
        match user with
        | Some u ->
            if u.Role = "admin" then
                let albumsListModel, albumsListMsg = AlbumEdit.init id
                { model with CurrentPage = AlbumEdit albumsListModel}, albumsListMsg |> Cmd.map AlbumEditMsg
            else
                { model with CurrentPage = NotAuthorized }, Cmd.none
        | None -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | ["albums"; "create"] ->
        match user with
        | Some u ->
            if u.Role = "admin" then
                let albumsListModel, albumsListMsg = AlbumCreate.init ()
                { model with CurrentPage = AlbumCreate albumsListModel}, albumsListMsg |> Cmd.map AlbumCreateMsg
            else
                { model with CurrentPage = NotAuthorized }, Cmd.none
        | None -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | _ -> model, Cmd.none

let init () : Model * Cmd<Msg> =
    match getCookieValue "user" with
    | Some u ->
        let model, cmd = initFromUrl (Router.currentUrl ()) None
        model, Cmd.OfAsync.perform usersApi.getUser u ChangedUser
    | None ->
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
    | _, ChangeUser username ->
        setCookie ("user", username)
        let cmd = Cmd.OfAsync.perform usersApi.getUser username ChangedUser
        model, cmd
    | _, Login (username, password) ->
        let cmd = Cmd.OfAsync.perform usersApi.validateUser (username, password) ChangedUser
        model, cmd
    | _, Register (username, email, password) ->
        let cmd = Cmd.OfAsync.perform usersApi.newUser (username, password, email) RegisteredUser
        model, cmd
    | _, ChangedUser user ->
        match user with
        | Some u -> setCookie ("user", u.Username)
        | None -> "" |> ignore
        initFromUrl (Router.currentUrl ()) user
    | _, RegisteredUser user ->
        setCookie ("user", user.Username)
        initFromUrl (Router.currentUrl ()) (Some user)
    | _, LoginFormChanged form ->
        { model with LoginForm = form }, Cmd.none
    | _, RegisterFormChanged form ->
        { model with RegisterForm = form }, Cmd.none
    | _, ToggleLModal value ->
        {model with LModalShown = value }, Cmd.none
    | _, ToggleRModal value ->
        {model with RModalShown = value }, Cmd.none
    | _, GoBack ->
        model, Cmd.navigateBack 1
    | _, _ -> initFromUrl (Router.currentUrl ()) model.CurrentUser


let notAuthorizedView dispatch =
    Bulma.box [
        Bulma.box "Not authorized"
        Bulma.content [
            Html.br[]
            Html.button [
                text.hasTextCentered
                prop.text "Go Back"
                prop.onClick (fun _ -> dispatch GoBack)
                color.hasTextDark
            ]
        ]
    ]

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
    | NotAuthorized -> notAuthorizedView dispatch

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
    { model with LModalShown = true }

let loginForm _ : Form.Form<LoginValues, Msg, _> =
    let usernameField =
        Form.textField
            {
                Parser = Ok
                Value = fun values -> values.LUsername
                Update = fun newValue values -> { values with LUsername = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Username"
                        Placeholder = ""
                        HtmlAttributes = []
                    }
            }
    let passwordField =
        Form.passwordField
            {
                Parser = Ok
                Value = fun values -> values.LPassword
                Update = fun newValue values -> { values with LPassword = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Password"
                        Placeholder = ""
                        HtmlAttributes = []
                    }
            }

    let onSubmit =
        fun username password ->
            Login (username, password)

    Form.succeed onSubmit
        |> Form.append usernameField
        |> Form.append passwordField

let registerForm _ : Form.Form<RegisterValues, Msg, _> =
    let usernameField =
        Form.textField
            {
                Parser = Ok
                Value = fun values -> values.Username
                Update = fun newValue values -> { values with Username = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Username"
                        Placeholder = "username"
                        HtmlAttributes = []
                    }
            }
    let emailField =
        Form.emailField
            {
                Parser = Ok
                Value = fun values -> values.Email
                Update = fun newValue values -> { values with Email = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Email"
                        Placeholder = "email"
                        HtmlAttributes = []
                    }
            }
    let passwordField =
        Form.passwordField
            {
                Parser = Ok
                Value = fun values -> values.Password
                Update = fun newValue values -> { values with Password = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Password"
                        Placeholder = "password"
                        HtmlAttributes = []
                    }
            }

    let onSubmit =
        fun username email password ->
            Register (username, email, password)

    Form.succeed onSubmit
        |> Form.append usernameField
        |> Form.append emailField
        |> Form.append passwordField

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
                                    prop.target "modal-login"
                                    prop.text "Log in"
                                    prop.onClick (fun _ -> ToggleLModal true |> dispatch)
                                ]
                                Bulma.modal [
                                    if model.LModalShown then Bulma.modal.isActive
                                    prop.id "modal-login"
                                    prop.children [
                                        Bulma.modalBackground []
                                        Bulma.modalContent [
                                            Form.View.asHtml
                                                {
                                                    Dispatch = dispatch
                                                    OnChange = LoginFormChanged
                                                    Action = Form.View.Action.SubmitOnly "Log In"
                                                    Validation = Form.View.ValidateOnSubmit
                                                }
                                                (loginForm ())
                                                model.LoginForm
                                        ]
                                        Bulma.modalClose [
                                            prop.onClick (fun _ -> ToggleLModal false |> dispatch)
                                        ]
                                    ]
                                ]
                                Bulma.button.button [
                                    prop.ariaHasPopup true
                                    prop.target "modal-register"
                                    prop.text "Register"
                                    prop.onClick (fun _ -> ToggleRModal true |> dispatch)
                                ]
                                Bulma.modal [
                                    if model.RModalShown then Bulma.modal.isActive
                                    prop.id "modal-register"
                                    prop.children [
                                        Bulma.modalBackground []
                                        Bulma.modalContent [
                                            Form.View.asHtml
                                                {
                                                    Dispatch = dispatch
                                                    OnChange = RegisterFormChanged
                                                    Action = Form.View.Action.SubmitOnly "Register"
                                                    Validation = Form.View.ValidateOnSubmit
                                                }
                                                (registerForm ())
                                                model.RegisterForm
                                        ]
                                        Bulma.modalClose [
                                            prop.onClick (fun _ -> ToggleRModal false |> dispatch)
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
