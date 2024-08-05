module ServerProject.Authorize

open System
open System.IO
open System.Security.Claims
open Microsoft.IdentityModel.JsonWebTokens
open Shared.Types

let private algorithm = Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256

let private createPassPhrase () =
    let crypto = System.Security.Cryptography.RandomNumberGenerator.Create()
    let randomNumber = Array.init 32 byte
    crypto.GetBytes(randomNumber)
    randomNumber

let secret =
    let fi = FileInfo("./temp/token.txt")

    if not fi.Exists then
        let passPhrase = createPassPhrase ()

        if not fi.Directory.Exists then
            fi.Directory.Create()

        File.WriteAllBytes(fi.FullName, passPhrase)

    File.ReadAllBytes(fi.FullName) |> System.Text.Encoding.UTF8.GetString

let issuer = "safe_music_store.io"

let generateToken username role =
    [
        Claim(JwtRegisteredClaimNames.Sub, username)
        Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", role)
    ]
    |> Saturn.Auth.generateJWT (secret, algorithm) issuer (DateTime.UtcNow.AddHours(1.0))

let getRole username =
    match username with
        | "admin" -> "Admin"
        | _ -> "User"

let createUserData (login: Login) : UserData = {
    UserName = UserName login.UserName
    Token = generateToken login.UserName (login.UserName |> getRole)
    Role = login.UserName |> getRole
}

let login (login: Login) =
    login |> createUserData