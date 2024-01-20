module ServerProject.Db

open FSharp.Data.Sql
open FSharp.Data.Sql.Common
open Farmer.Arm.Network


// [<Literal>]
// let SqlPath = __SOURCE_DIRECTORY__ + @"/../../sql/SafeMusicStoreDb/bin/Debug/SafeMusicStoreDb.dacpac"
//
// type DB =
//     SqlDataProvider<
//         Common.DatabaseProviderTypes.MSSQLSERVER_SSDT,
//         SsdtPath = SqlPath,
//         UseOptionTypes = true
//     >



[<Literal>]
let TPConnectionString =
    "Server=localhost;"    +
    "Database=safe_music_store;" +
    "User Id=safe;"            +
    "Password=1234;"

type DB =
    SqlDataProvider<
        ConnectionString      = TPConnectionString,
        DatabaseVendor        = Common.DatabaseProviderTypes.POSTGRESQL,
        CaseSensitivityChange = Common.CaseSensitivityChange.ORIGINAL >

let createContext (connectionString: string) =
    DB.GetDataContext(connectionString)