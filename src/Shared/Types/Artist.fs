module Shared.Artist

open System
open Shared.Types

let create (name : string) (surname : string) (dob : DateOnly option) (dod : DateOnly option) =
    {
        Id = Guid.NewGuid()
        Name = name
        Surname = surname
        DoB = dob
        DoD = dod
    }

let isValid (dob : DateOnly option) (dod : DateOnly option) =
    match dob, dod with
    | None, None -> true
    | None, Some dateOfDeath ->
        not (dateOfDeath >= DateOnly.FromDateTime DateTime.Today)
    | Some dateOfBirth, None ->
        not (dateOfBirth >= (DateOnly.FromDateTime DateTime.Today))
    | Some dateOfBirth, Some dateOfDeath ->
        if dateOfBirth >= (DateOnly.FromDateTime DateTime.Today)
           || dateOfDeath >= DateOnly.FromDateTime DateTime.Today
           || dateOfBirth >= dateOfDeath then
            false
        else
            true