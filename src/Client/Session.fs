module Client.Session

open Shared.Types
open Thoth.Json

[<Literal>]
let USER_SESSION_KEY = "user"
let CART_SESSION_KEY = "cart"

let loadUser () : UserData option =
    let userDecoder = Decode.Auto.generateDecoder<UserData> ()

    match LocalStorage.load userDecoder USER_SESSION_KEY with
    | Ok user -> Some user
    | Error _ -> None

let deleteUser () = LocalStorage.delete USER_SESSION_KEY

let saveUser (user: UserData) = LocalStorage.save USER_SESSION_KEY user

let loadCart () : string option =
    let cartDecoder = Decode.Auto.generateDecoder<string> ()

    match LocalStorage.load cartDecoder CART_SESSION_KEY with
    | Ok cart -> Some cart
    | Error _ -> None

let saveCart (cart : string) = LocalStorage.save CART_SESSION_KEY cart

let deleteCart () = LocalStorage.delete CART_SESSION_KEY