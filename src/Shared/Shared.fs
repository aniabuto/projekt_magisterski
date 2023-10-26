namespace Shared

open System

// type Todo = { Id: Guid; Description: string }

// module Todo =
//     let isValid (description: string) =
//         String.IsNullOrWhiteSpace description |> not

//     let create (description: string) =
//         { Id = Guid.NewGuid()
//           Description = description }

module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

// type ITodosApi =
//     { getTodos: unit -> Async<Todo list>
//       addTodo: Todo -> Async<Todo>
//       removeTodo: Todo -> Async<bool> }

type Book = {
    Id : Guid;
    Title : string;
    Author : string;
    Description : string;
}

module Book =
    let create (title : string) (author : string) (description : string) =
        {
            Id = Guid.NewGuid()
            Author = author
            Title = title
            Description = description
        }

type IBooksApi =
    {
        getBooks : unit -> Async<Book list>
        addBook : Book -> Async<Book>
        removeBook : Guid -> Async<bool>
        removeBook2 : unit -> Async<bool>
    }
