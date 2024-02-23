module Server
//
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Saturn
open Giraffe

open ServerProject
open Shared

// let connectionString = @"Server=localhost\SQLEXPRESS;Database=SafeMusicStore;Trusted_Connection=True;"

let webApp =
    let authenticated =
        warbler (fun _ -> requiresAuthentication (challenge JwtBearerDefaults.AuthenticationScheme))

    choose [
        Remoting.createApi ()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.fromValue Api.guestApi
        |> Remoting.buildHttpHandler

        authenticated >=> (
            Remoting.createApi ()
            |> Remoting.withRouteBuilder Route.builder
            |> Remoting.fromValue Api.authorizedApi
            |> Remoting.buildHttpHandler)
    ]

let app =
    application {
        use_router webApp
        use_jwt_authentication Authorize.secret Authorize.issuer
        memory_cache
        use_static "public"
        use_gzip
    }

run app