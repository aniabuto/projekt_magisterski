module ServerProject.Db

open FSharp.Data.Sql

[<Literal>]
let TPConnectionString =
    "Server=localhost;" +
    "Database=safe_music_store;" +
    "User Id=safe;" +
    "Password=1234;"

type DB =
    SqlDataProvider<
        ConnectionString      = TPConnectionString,
        DatabaseVendor        = Common.DatabaseProviderTypes.POSTGRESQL,
        CaseSensitivityChange = Common.CaseSensitivityChange.ORIGINAL >

let createContext (connectionString: string) =
    DB.GetDataContext(connectionString)