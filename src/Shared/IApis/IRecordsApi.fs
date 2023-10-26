module Shared.IRecordsApi

open System
open Shared.Types

type IRecordsApi =
    {
        getRecords : unit -> Async<Record list>
        addRecord : Record -> Async<Record>
        addRecordArtist : Artist -> Async<Record>
        removeRecordArtist : Artist -> Async<Record>
        removeRecord : Guid -> Async<bool>
    }
