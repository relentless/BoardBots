module TestHelpers

open FsUnit
open BoardBots.Shared

type FakeBoard()=
    let mutable internalTokens = 
        [|
            [| PlayerToken.None; PlayerToken.None; PlayerToken.None  |]
            [| PlayerToken.None; PlayerToken.None; PlayerToken.None  |]
            [| PlayerToken.None; PlayerToken.None; PlayerToken.None  |]
        |]

    member this.Tokens
        with get() = internalTokens
        and set(value) = internalTokens <- value

    interface IPlayerBoard with
        member this.TokenAt(position) = 
            internalTokens.[position.Column].[position.Row]

let shouldEqual expected (actual:BoardPosition) =
    (actual.Column, actual.Row) |> should equal expected

let shouldNotEqual unexpected (actual:BoardPosition) =
    (actual.Column, actual.Row) |> should not' (equal unexpected)

let shouldBeIn expected (actual:BoardPosition) = 
    expected |> should contain (actual.Column, actual.Row)