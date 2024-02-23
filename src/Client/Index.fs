module Client.Index

open System
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
open Browser
open Client.Exceptions

type Page =
    // | BestsellersList of BestsellersList.Model
    // | GenresList of GenresList.Model
    // | ArtistsList of ArtistsList.Model
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

type User =
    | Guest
    | User of UserData

type Model = {
    CurrentPage : Page
    CurrentUser : User
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
    | ToggleLModal of bool
    | ToggleRModal of bool
    | LoginFormChanged of LoginForm
    | Login of string * string
    | ChangedUser of UserData
    | Logout
    | RegisterFormChanged of RegisterForm
    | Register of string * string * string
    | RegisteredUser of Shared.Types.User
    | UserChangeSuccess of unit
    | OnSessionChange
    | LoggedOut of unit
    | Fail of exn
    | GoBack

let initFromUrl model url =
    // let model = {
    //               CurrentPage = NotFound
    //               CurrentUser = user
    //               LModalShown = false
    //               RModalShown = false
    //               LoginForm = Form.View.idle { LUsername = ""; LPassword = "" }
    //               RegisterForm = Form.View.idle { Username = ""; Email = ""; Password = "" } }
    match url with
    | [] ->
        model, Cmd.none
    // | ["bestsellers"] ->
    //     let bestsellersListModel, bestsellersListMsg = BestsellersList.init ()
    //     { model with CurrentPage = BestsellersList bestsellersListModel}, bestsellersListMsg |> Cmd.map BestsellersListMsg
    // | ["genres"] ->
    //     match user with
    //     | Some _ ->
    //         let genresListModel, genresListMsg = GenresList.init ()
    //         { model with CurrentPage = GenresList genresListModel}, genresListMsg |> Cmd.map GenresListMsg
    //     | None -> { model with CurrentPage = NotAuthorized }, Cmd.none
    // | ["artists"] ->
    //     match user with
    //     | Some _ ->
    //         let artistsListModel, artistsListMsg = ArtistsList.init ()
    //         { model with CurrentPage = ArtistsList artistsListModel}, artistsListMsg |> Cmd.map ArtistsListMsg
    //     | None -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | ["albums"] ->
        let albumsListModel, albumsListMsg = AlbumsList.init guestApi
        { model with CurrentPage = AlbumsList albumsListModel}, albumsListMsg |> Cmd.map AlbumsListMsg
    | ["albums"; Route.Int id] ->
        let albumsListModel, albumsListMsg = AlbumDetails.init id guestApi
        { model with CurrentPage = AlbumDetails albumsListModel}, albumsListMsg |> Cmd.map AlbumDetailsMsg
    | ["albums"; Route.Int id; "edit"] ->
        match model.CurrentUser with
        | User u ->
            let albumsListModel, albumsListMsg = AlbumEdit.init id (authorizedApi u.Token) u.UserName
            { model with CurrentPage = AlbumEdit albumsListModel}, albumsListMsg |> Cmd.map AlbumEditMsg
        | Guest -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | ["albums"; "create"] ->
        match model.CurrentUser with
        | User u ->
            let albumsListModel, albumsListMsg = AlbumCreate.init (authorizedApi u.Token) u.UserName
            { model with CurrentPage = AlbumCreate albumsListModel}, albumsListMsg |> Cmd.map AlbumCreateMsg
        | Guest -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | _ -> model, Cmd.none

let init () : Model * Cmd<Msg> =
    let model, _ = AlbumsList.init guestApi
    let user =
        Session.loadUser ()
        |> Option.map User
        |> Option.defaultValue Guest

    Router.currentUrl ()
    |> initFromUrl {
        CurrentPage = AlbumsList model
        CurrentUser = user
        LModalShown = false
        RModalShown = false
        LoginForm = Form.View.idle { LUsername = ""; LPassword = "" }
        RegisterForm = Form.View.idle { Username = ""; Email = ""; Password = "" }
    }

let update (message: Msg) (model: Model) : Model * Cmd<Msg> =
    match model.CurrentPage, message with
    // | BestsellersList bestsellersList, BestsellersListMsg bestsellersListMessage ->
    //     let newBestsellersListModel, newCommand = BestsellersList.update bestsellersListMessage bestsellersList
    //     let model = { model with CurrentPage = BestsellersList newBestsellersListModel }
    //
    //     model, newCommand |> Cmd.map BestsellersListMsg
    // | GenresList genresList, GenresListMsg genresListMessage ->
    //     let newGenresListModel, newCommand = GenresList.update genresListMessage genresList
    //     let model = { model with CurrentPage = GenresList newGenresListModel }
    //
    //     model, newCommand |> Cmd.map GenresListMsg
    // | ArtistsList artistsList, ArtistsListMsg artistsListMessage ->
    //     let newArtistsListModel, newCommand = ArtistsList.update artistsListMessage artistsList
    //     let model = { model with CurrentPage = ArtistsList newArtistsListModel }
    //
    //     model, newCommand |> Cmd.map ArtistsListMsg
    | AlbumsList albumsList, AlbumsListMsg albumsListMessage ->
        let newAlbumsListModel, newCommand = AlbumsList.update albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumsList newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumsListMsg
    | AlbumDetails albumsList, AlbumDetailsMsg albumsListMessage ->
        let token =
            match model.CurrentUser with
            | User data -> data.Token
            | Guest -> ""
        let newAlbumsListModel, newCommand = AlbumDetails.update (authorizedApi token) albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumDetails newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumDetailsMsg
    | AlbumEdit albumsList, AlbumEditMsg albumsListMessage ->
        let token =
            match model.CurrentUser with
            | User data -> data.Token
            | Guest -> ""
        let newAlbumsListModel, newCommand = AlbumEdit.update (authorizedApi token) albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumEdit newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumEditMsg
    | AlbumCreate albumsList, AlbumCreateMsg albumsListMessage ->
        let token =
            match model.CurrentUser with
            | User data -> data.Token
            | Guest -> ""
        let newAlbumsListModel, newCommand = AlbumCreate.update (authorizedApi token) albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumCreate newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumCreateMsg
    | _, UrlChanged url -> initFromUrl model url
    | _, Login (username, password) ->
        let cmd = Cmd.OfAsync.perform guestApi.login (username, password) ChangedUser
        { model with LoginForm = Form.View.idle { LUsername = ""; LPassword = "" }}, cmd
    | _, ChangedUser user ->
        { model with LModalShown = false }, Cmd.OfFunc.either Session.saveUser user UserChangeSuccess Fail
    | _,  UserChangeSuccess _ ->
        { model with LModalShown = false }, Cmd.ofMsg OnSessionChange
    | _, Fail ex ->
        model, Cmd.none
    | _, Logout ->

        model, Cmd.OfFunc.either Session.deleteUser () LoggedOut Fail
    | _, LoggedOut _ ->
        { model with CurrentUser = Guest }, Cmd.navigate "albums"
    | _, Register (username, email, password) ->
        let cmd = Cmd.OfAsync.perform guestApi.newUser (username, password, email) RegisteredUser
        { model with RegisterForm = Form.View.idle { Username = ""; Email = ""; Password = "" }}, cmd
    | _, OnSessionChange ->
        let session = Session.loadUser ()
        let user = session |> Option.map User |> Option.defaultValue Guest
        let cmd =
            session
            |> Option.map (fun _ -> Cmd.none)
            |> Option.defaultValue (Cmd.navigate "albums")
        { model with CurrentUser = user }, cmd
    | _, RegisteredUser user ->
        let cmd = Cmd.OfAsync.perform guestApi.login (user.Username, user.Password) ChangedUser
        model, cmd
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
    | _, _ -> initFromUrl model (Router.currentUrl ())


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
    // | BestsellersList bestsellersModel -> BestsellersList.view bestsellersModel (BestsellersListMsg >> dispatch)
    // | GenresList genresModel -> GenresList.view genresModel (GenresListMsg >> dispatch)
    // | ArtistsList artistsModel -> ArtistsList.view artistsModel (ArtistsListMsg >> dispatch)
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

let validateUsername name =
    if String.IsNullOrWhiteSpace name |> not then
        Ok name
    else
        Error "You need to fill in a username."

let validatePassword password =
    if String.IsNullOrWhiteSpace password |> not then
        Ok password
    else
        Error "You need to fill in a password."

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
                            | User user ->
                                Bulma.subtitle [
                                    text.hasTextCentered
                                    prop.text user.UserName.Value
                                ]
                                Html.button [
                                    prop.text "Log Out"
                                    prop.onClick (fun _ -> Logout |> dispatch)
                                ]
                            | Guest ->
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


let resetStorage onResetStorageMsg =
    let register dispatch =
        let callback _ = dispatch onResetStorageMsg
        window.addEventListener ("storage", callback)

        { new IDisposable with
            member _.Dispose() =
                window.removeEventListener ("storage", callback)
        }

    register

let subscribe _ = [ [ "resetStorage" ], resetStorage OnSessionChange ]