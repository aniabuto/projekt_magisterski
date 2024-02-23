module Client.Apis

open Fable.Remoting.Client
open Shared

let guestApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IGuestApi>

let authorizedApi token =
    let bearer = $"Bearer {token}"
    Remoting.createApi ()
    |> Remoting.withAuthorizationHeader bearer
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IAuthorizedApi>