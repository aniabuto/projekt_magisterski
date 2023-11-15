module ServerProject.Db

open FSharp.Data.Sql
open FSharp.Data.Sql.Common
open Farmer.Arm.Network


[<Literal>]
let SqlPath = __SOURCE_DIRECTORY__ + @"/../../sql/SafeMusicStoreDb/bin/Debug/SafeMusicStoreDb.dacpac"

type DB =
    SqlDataProvider<
        Common.DatabaseProviderTypes.MSSQLSERVER_SSDT,
        SsdtPath = SqlPath,
        UseOptionTypes = true
    >

let createContext (connectionString: string) =
    DB.GetDataContext(connectionString)
