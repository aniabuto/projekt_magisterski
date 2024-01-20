module Client.AlbumCreate

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
    Artist : string
    Genre : string
}

type Form = Form.View.Model<Values>

type Model = {
    Form : Form
    Artists : Artist list
    Genres : Genre list
}

[<Literal>]
let placeholderString = "/placeholder.webp"

type Msg =
    | GotArtists of Artist list
    | GotGenres of Genre list
    | CreateAlbum of int * int * string * decimal * string
    | FormChanged of Form
    | AlbumCreated of int
    | GoBack


let init () : Model * Cmd<Msg> =
    let model = {
        Form = Form.View.idle { Title = ""; Thumbnail = placeholderString; Price = string(Decimal.Zero); Artist = "0"; Genre = "0" }
        Artists = []
        Genres = []
    }

    let cmd = Cmd.batch [
        Cmd.OfAsync.perform artistsApi.getArtists () GotArtists
        Cmd.OfAsync.perform genresApi.getGenres () GotGenres
    ]

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
     | GotArtists artists ->
         { model with Artists =  artists }, Cmd.none
     | GotGenres genres ->
         { model with Genres = genres }, Cmd.none
     | CreateAlbum ( artistId, genreId, title, price, thumbnail) ->
         model, Cmd.OfAsyncImmediate.perform albumsApi.createAlbum (artistId, genreId, price, title, thumbnail) AlbumCreated
     | FormChanged form ->
         { model with Form = form }, Cmd.none
     | AlbumCreated id ->
         model, Cmd.navigateBack 1
     | GoBack ->
         model, Cmd.navigateBack 1
     | _ ->
         model, Cmd.none


open Feliz
open Feliz.Bulma

let form (artistsList : Artist list) (genresList : Genre list) : Form.Form<Values, Msg, _> =
    let artistField =
        Form.selectField
            {
                Parser = Ok
                Value = fun values -> values.Artist
                Update = fun newValue values -> { values with Artist = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Album Artist"
                        Placeholder = ""
                        Options = artistsList |> List.map (fun artist -> (string(artist.Id), artist.Name))
                    }
            }
    let genreField =
        Form.selectField
            {
                Parser = Ok
                Value = fun values -> values.Genre
                Update = fun newValue values -> { values with Genre = newValue }
                Error = fun _ -> None
                Attributes =
                    {
                        Label = "Album Genre"
                        Placeholder = ""
                        Options = genresList |> List.map (fun genre -> (string(genre.Id), genre.Name))
                    }
            }
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
                        Placeholder = "Album Title"
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
                        Placeholder = string(Decimal.Zero)
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
                        Placeholder = placeholderString
                        HtmlAttributes = []
                    }
            }
    let onSubmit =
        fun fartist fgenre ftitle fprice fthumbnail ->
            CreateAlbum (int(fartist), int(fgenre), ftitle, decimal(fprice), fthumbnail)

    Form.succeed onSubmit
        |> Form.append artistField
        |> Form.append genreField
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
        Form.View.asHtml
            {
                Dispatch = dispatch
                OnChange = FormChanged
                Action = Form.View.Action.SubmitOnly "Create Album"
                Validation = Form.View.ValidateOnSubmit
            }
            (form model.Artists model.Genres)
            model.Form
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
