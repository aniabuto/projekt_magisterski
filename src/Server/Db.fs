module ServerProject.Db

open FSharp.Data.Sql
open FSharp.Data.Sql.Common
open Farmer.Arm.Network


let [<Literal>] dbVendor = Common.DatabaseProviderTypes.POSTGRESQL
let [<Literal>] connString = "Host=localhost;Database=safe_music_store;Username=suave;Password=1234"

type sql =
    SqlDataProvider<
        dbVendor,
        connString,
        CaseSensitivityChange = Common.CaseSensitivityChange.ORIGINAL
    >

//
// let [<Literal>] connectionString =
//     "Server=127.0.0.1;Database=safe_music_store;User Id=suave;Password=1234;"
//

//C:\Users\aniab\.nuget\packages\npgsql\8.0.0-rc.2\lib\net6.0
// type Sql =
//     SqlDataProvider<
//         dbVendor,
//         connString
//     >

type DbContext = sql.dataContext
type Album = DbContext.``public.AlbumsEntity``
type Genre = DbContext.``public.GenresEntity``
// type AlbumDetails = DbContext.``public.albumdetailsEntity``