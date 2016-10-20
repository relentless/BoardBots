module BasicBot_Tests

open NUnit.Framework
open FsUnit
open BoardBots.Shared
open BasicBot
open TestHelpers

let testBot = new BasicBot()

[<Test>]
let ``When board is empty should play in centre`` () =
    let board = new FakeBoard()
    board |> testBot.TakeTurn |> shouldEqual (1,1)

[<Test>]
let ``When centre is taken should not play in centre`` () =
    let board = new FakeBoard()
    board.Tokens.[1].[1] <- PlayerToken.Opponent
    board |> testBot.TakeTurn |> shouldNotEqual (1,1)

[<Test>]
let ``When centre is taken should play in a corner`` () =
    let board = new FakeBoard()
    board.Tokens.[1].[1] <- PlayerToken.Opponent
    board |> testBot.TakeTurn |> shouldBeIn [(0,0);(0,2);(2,0);(2,2)]

[<Test>]
let ``Example of specifying the whole board`` () =
    let board = new FakeBoard()
    
    // NOTE: Rows & columns not where you wold expect
    board.Tokens = 
        [|
                   // Row 0             Row 1              Row 2
            [| PlayerToken.Opponent; PlayerToken.None; PlayerToken.None  |] // Column 0
            [| PlayerToken.None; PlayerToken.None; PlayerToken.Me  |] // Column 1
            [| PlayerToken.None; PlayerToken.Me; PlayerToken.Opponent  |] // Column 2
        |] |> ignore

    board |> testBot.TakeTurn |> shouldEqual (1,1)