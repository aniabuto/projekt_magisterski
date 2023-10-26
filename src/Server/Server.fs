module Server
//
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
// open Giraffe
open Saturn

open Shared
open System


module Storage =
    let books = ResizeArray<Book>()

    let addBook (book : Book) =
        let bookPresent =
            books
            |> Seq.tryFind (fun b -> b.Id = book.Id)
        match bookPresent with
        | Some(b) ->
            Error "Book already in library"
        | None ->
            books.Add book
            Ok()

    let deleteBook (id : Guid) =
        let book =
            books
            |> Seq.tryFind (fun book -> book.Id = id)
        match book with
        | Some(b) ->
            books.Remove b |> ignore
            Ok()
        | None ->
            Error "Book doesn't exist"

    do
        addBook (Book.create "The Witcher" "Andrzej Sapkowski" "jakis opis ksiazki")
        |> ignore



let booksApi =
    {
        getBooks = fun () -> async { return Storage.books |> List.ofSeq }

        addBook = fun book -> async {
            return
                match Storage.addBook book with
                    | Ok () -> book
                    | Error e -> failwith e
        }

        removeBook = fun id -> async {
            return
                match Storage.deleteBook id with
                    | Ok () -> true
                    | Error e -> failwith e
        }

        removeBook2 = fun () -> async {
            let id = Guid("8afc50aa-fc75-46a1-a20c-0d08a16216bd")
            return
                match Storage.deleteBook id with
                    | Ok () -> true
                    | Error e -> failwith e
        }
    }


let webApp =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue booksApi
    |> Remoting.buildHttpHandler

let app =
    application {
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
    }

run app
