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
    | AlbumsList of AlbumsList.Model
    | ArtistsList of ArtistsList.Model
    | GenresList of GenresList.Model
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
    CurrentUser : UserClient
    CurrentCartId : string
    CartItems : CartDetails list
    CartModalShown : bool
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
    | NavigateBestsellers
    | NavigateAlbums
    | NavigateArtists
    | NavigateGenres
    | GoToCheckout
    | ClearCart
    | RefreshCart of unit
    | RemoveFromCart of int
    | GotCartDetails of CartDetails list
    | GetAlbumDetails of int
    | ToggleViewCart of bool
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
    match url with
    | [] ->
        model, Cmd.none
    | ["albums"] ->
        let albumsListModel, albumsListMsg = AlbumsList.init guestApi
        { model with CurrentPage = AlbumsList albumsListModel}, albumsListMsg |> Cmd.map AlbumsListMsg
    | ["artists"] ->
        let artistsListModel, artistsListMsg = ArtistsList.init guestApi
        { model with CurrentPage = ArtistsList artistsListModel}, artistsListMsg |> Cmd.map ArtistsListMsg
    | ["genres"] ->
        let genresListModel, genresListMsg = GenresList.init guestApi
        { model with CurrentPage = GenresList genresListModel}, genresListMsg |> Cmd.map GenresListMsg
    | ["albums"; Route.Int id] ->
        let albumsListModel, albumsListMsg = AlbumDetails.init id guestApi
        { model with CurrentPage = AlbumDetails albumsListModel}, albumsListMsg |> Cmd.map AlbumDetailsMsg
    | ["albums"; Route.Int id; "edit"] ->
        match model.CurrentUser with
        | User u ->
            match u.Role with
            | "Admin" ->
                let albumsListModel, albumsListMsg = AlbumEdit.init id (adminApi u.Token) u.UserName
                { model with CurrentPage = AlbumEdit albumsListModel}, albumsListMsg |> Cmd.map AlbumEditMsg
            | _ ->
                { model with CurrentPage = NotAuthorized }, Cmd.none
        | Guest -> { model with CurrentPage = NotAuthorized }, Cmd.none
    | ["albums"; "create"] ->
        match model.CurrentUser with
        | User u ->
            let albumsListModel, albumsListMsg = AlbumCreate.init (adminApi u.Token) u.UserName
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
        CurrentCartId = ""
        CartItems = []
        CartModalShown = false
        LModalShown = false
        RModalShown = false
        LoginForm = Form.View.idle { LUsername = ""; LPassword = "" }
        RegisterForm = Form.View.idle { Username = ""; Email = ""; Password = "" }
    }

let update (message: Msg) (model: Model) : Model * Cmd<Msg> =
    match model.CurrentPage, message with
    | AlbumsList albumsList, AlbumsListMsg albumsListMessage ->
        let newAlbumsListModel, newCommand = AlbumsList.update albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumsList newAlbumsListModel; CurrentCartId = newAlbumsListModel.CartId }
        model, newCommand |> Cmd.map AlbumsListMsg

    | ArtistsList artistsList, ArtistsListMsg artistsListMessage ->
        let newArtistsModel, newCommand = ArtistsList.update artistsListMessage artistsList
        let model = { model with CurrentPage = ArtistsList newArtistsModel }

        model, newCommand |> Cmd.map ArtistsListMsg
    | GenresList genresList, GenresListMsg genresListMessage ->
        let newGenresListModel, newCommand = GenresList.update genresListMessage genresList
        let model = { model with CurrentPage = GenresList newGenresListModel }

        model, newCommand |> Cmd.map GenresListMsg
    // | BestsellersList bestsellersList, BestsellersListMsg bestsellersListMessage ->
    //     let newBestsellersListModel, newCommand = BestsellersList.update bestsellersListMessage bestsellersList
    //     let model = { model with CurrentPage = BestsellersList newBestsellersListModel }
    //
    //     model, newCommand |> Cmd.map BestsellersListMsg
    | AlbumDetails albumsList, AlbumDetailsMsg albumsListMessage ->
        let token =
            match model.CurrentUser with
            | User data -> data.Token
            | Guest -> ""
        let newAlbumsListModel, newCommand = AlbumDetails.update (adminApi token) albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumDetails newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumDetailsMsg
    | AlbumEdit albumsList, AlbumEditMsg albumsListMessage ->
        let token =
            match model.CurrentUser with
            | User data -> data.Token
            | Guest -> ""
        let newAlbumsListModel, newCommand = AlbumEdit.update (adminApi token) albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumEdit newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumEditMsg
    | AlbumCreate albumsList, AlbumCreateMsg albumsListMessage ->
        let token =
            match model.CurrentUser with
            | User data -> data.Token
            | Guest -> ""
        let newAlbumsListModel, newCommand = AlbumCreate.update (adminApi token) albumsListMessage albumsList
        let model = { model with CurrentPage = AlbumCreate newAlbumsListModel }

        model, newCommand |> Cmd.map AlbumCreateMsg
    | _, UrlChanged url -> initFromUrl model url
    | _, NavigateBestsellers _ ->
        model, Cmd.navigate "bestsellers"
    | _, NavigateAlbums _ ->
        model, Cmd.navigate "albums"
    | _, NavigateArtists _ ->
        model, Cmd.navigate "artists"
    | _, NavigateGenres _ ->
        model, Cmd.navigate "genres"
    | _, ToggleViewCart value ->
        let cartId =
            match model.CurrentUser with
            | User user -> user.UserName.Value
            | Guest _ -> model.CurrentCartId
        match value with
        | false ->
            {model with CartModalShown = value }, Cmd.none
        | true ->
            model, Cmd.OfAsync.perform guestApi.getCartDetails cartId GotCartDetails
    | _, GotCartDetails cartDetails ->
        { model with
            CartItems =  cartDetails
            CartModalShown = true }, Cmd.none
    | _, ClearCart ->
        model, Cmd.none
    | _, RefreshCart _ ->
        let cartId =
            match model.CurrentUser with
            | User user -> user.UserName.Value
            | Guest _ -> model.CurrentCartId
        model, Cmd.OfAsync.perform guestApi.getCartDetails cartId GotCartDetails
    | _, RemoveFromCart id ->
        let cartId =
            match model.CurrentUser with
            | User user -> user.UserName.Value
            | Guest _ -> model.CurrentCartId
        model, Cmd.OfAsync.perform guestApi.removeFromCart (cartId, id) RefreshCart
    | _, ToggleLModal value ->
        {model with LModalShown = value }, Cmd.none
    | _, LoginFormChanged form ->
        { model with LoginForm = form }, Cmd.none
    | _, Login (username, password) ->
        let cmd = Cmd.OfAsync.perform guestApi.login (username, password) ChangedUser
        { model with LoginForm = Form.View.idle { LUsername = ""; LPassword = "" }}, cmd
    | _, ChangedUser user ->
        { model with LModalShown = false }, Cmd.OfFunc.either Session.saveUser user UserChangeSuccess Fail
    | _,  UserChangeSuccess _ ->
        { model with LModalShown = false }, Cmd.ofMsg OnSessionChange
    | _, Fail ex ->
        model, Cmd.none
    | _, OnSessionChange ->
        let session = Session.loadUser ()
        let user = session |> Option.map User |> Option.defaultValue Guest
        match user with
        | User u -> Session.saveCart u.UserName.Value
        | _ -> Session.deleteCart ()
        let cmd =
            session
            |> Option.map (fun u -> Cmd.OfAsync.perform (authorizedApi u.Token).updateCarts (model.CurrentCartId, u.UserName.Value) RefreshCart )
            |> Option.defaultValue (Cmd.navigate "albums")
        { model with CurrentUser = user }, cmd
    | _, Logout ->
        model, Cmd.OfFunc.either Session.deleteUser () LoggedOut Fail
    | _, LoggedOut _ ->
        Session.deleteCart ()
        { model with CurrentUser = Guest }, Cmd.navigate "albums"
    | _, ToggleRModal value ->
        {model with RModalShown = value }, Cmd.none
    | _, Register (username, email, password) ->
        let cmd = Cmd.OfAsync.perform guestApi.newUser (username, password, email) RegisteredUser
        { model with RegisterForm = Form.View.idle { Username = ""; Email = ""; Password = "" }}, cmd
    | _, RegisteredUser user ->
        let cmd = Cmd.OfAsync.perform guestApi.login (user.Username, user.Password) ChangedUser
        model, cmd
    | _, RegisterFormChanged form ->
        { model with RegisterForm = form }, Cmd.none
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
    | AlbumsList albumsModel -> AlbumsList.view albumsModel (AlbumsListMsg >> dispatch)
    | AlbumDetails albumsModel -> AlbumDetails.view albumsModel (AlbumDetailsMsg >> dispatch)
    | AlbumEdit albumsModel -> AlbumEdit.view albumsModel (AlbumEditMsg >> dispatch)
    | AlbumCreate albumsModel -> AlbumCreate.view albumsModel (AlbumCreateMsg >> dispatch)
    | ArtistsList artistsModel -> ArtistsList.view artistsModel (ArtistsListMsg >> dispatch)
    | GenresList genresModel -> GenresList.view genresModel (GenresListMsg >> dispatch)
    // | BestsellersList bestsellersModel -> BestsellersList.view bestsellersModel (BestsellersListMsg >> dispatch)
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

let emptyCart (dispatch: Msg -> unit) =
    Bulma.modalCard [
        Bulma.modalCardHead [
            Bulma.modalCardTitle "My Cart"
            Bulma.delete [
                prop.ariaLabel "close"
                prop.onClick (fun _ -> ToggleViewCart false |> dispatch)
            ]
        ]
        Bulma.modalCardBody [
            Bulma.subtitle [
                Bulma.color.hasTextBlack
                prop.text "Cart is empty"
            ]
        ]
        Bulma.modalCardFoot []
    ]

let nonEmptyCart (carts : CartDetails list) (dispatch: Msg -> unit) =
    Bulma.modalCard [
        Bulma.modalCardHead [
            Bulma.modalCardTitle "My Cart"
            Bulma.delete [
                prop.ariaLabel "close"
                prop.onClick (fun _ -> ToggleViewCart false |> dispatch)
            ]
        ]
        Bulma.modalCardBody [
            Bulma.table [
                yield Html.tr [
                    for h in ["Album Name"; "Price (each)"; "Quantity"; ""] ->
                        Html.th [
                            prop.text h
                        ]
                ]
                for cart in carts ->
                    Html.tr [
                        Html.td [
                            Html.a [
                                prop.text cart.AlbumTitle
                                prop.onClick (fun _ -> GetAlbumDetails cart.AlbumId |> dispatch)
                            ]
                        ]
                        Html.td [
                            prop.text (string $"%.2f{cart.Price}")
                        ]
                        Html.td [
                            // Bulma.input.number [
                            //     prop.
                            // ]
                            prop.text (string $"{cart.Count}")
                        ]
                        Html.td [
                            Html.button [
                                prop.text "Remove"
                                prop.onClick (fun _ -> RemoveFromCart cart.AlbumId |> dispatch)
                            ]
                        ]
                    ]
            ]
        ]
        Bulma.modalCardFoot [
            Bulma.container [
                Bulma.block [
                    yield Bulma.input.text [
                        let total =
                            carts
                            |> List.sumBy (fun c -> c.Price * (decimal c.Count))
                        prop.value (string $"{total}")
                        prop.readOnly true
                    ]
                ]

                Bulma.buttons [
                    Bulma.button.button [
                        Bulma.color.isSuccess
                        prop.text "Checkout"
                        prop.onClick (fun _ -> dispatch GoToCheckout)
                    ]
                    // Bulma.button.button [
                    //     prop.text "Clear"
                    //     prop.onClick (fun _ -> dispatch ClearCart)
                    // ]
                ]
            ]
        ]
    ]


let cart (dispatch: Msg -> unit) = function
    | [] -> emptyCart dispatch
    | list -> nonEmptyCart list dispatch


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
                            Bulma.box [
                                Bulma.button.button [
                                    prop.text "Bestsellers"
                                    prop.onClick (fun _ -> NavigateBestsellers |> dispatch)
                                ]
                                Bulma.button.button [
                                    prop.text "Albums"
                                    prop.onClick (fun _ -> NavigateAlbums |> dispatch)
                                ]
                                Bulma.button.button [
                                    prop.text "Genres"
                                    prop.onClick (fun _ -> NavigateGenres |> dispatch)
                                ]
                                Bulma.button.button [
                                    prop.text "Artists"
                                    prop.onClick (fun _ -> NavigateArtists |> dispatch)
                                ]
                                Bulma.button.button [
                                    prop.text "Cart"
                                    prop.onClick (fun _ -> ToggleViewCart true |> dispatch)
                                ]
                            ]
                            Bulma.box [
                                match model.CurrentUser with
                                | User user ->
                                    Bulma.subtitle [
                                        text.hasTextCentered
                                        prop.text user.UserName.Value
                                        Bulma.color.hasTextBlack
                                    ]
                                    Bulma.button.button [
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
                            ]

                            Bulma.modal [
                                if model.CartModalShown then Bulma.modal.isActive
                                prop.id "modal-cart"
                                prop.children [
                                    Bulma.modalBackground []
                                    cart dispatch model.CartItems
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