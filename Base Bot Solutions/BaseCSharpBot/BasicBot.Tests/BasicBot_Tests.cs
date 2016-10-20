using System;
using NUnit.Framework;
using BoardBots.Shared;
using BasicBot;

namespace BasicBot.Tests
{
    // you may want to rename this to reflect what you called your bot
    [TestFixture]
    public class BasicBot_Tests
    {
        [Test]
        public void TakeTurn_EmptyBoard_PlaysInColumn0Row0()
        {
            // arrange
            BasicBot player = new BasicBot();
            FakeBoard emptyBoard = new FakeBoard();

            // act
            var positionPlayed = player.TakeTurn(emptyBoard);

            // assert
            Assert.That(positionPlayed.Column, Is.EqualTo(0));
            Assert.That(positionPlayed.Row, Is.EqualTo(0));
        }

        [Test]
        public void TakeTurn_Column0Row0AlreadyHasPiece_PlaysInColumn0Row0Anyway()
        {
            // arrange
            BasicBot player = new BasicBot();
            FakeBoard partiallyFullBoard = new FakeBoard();
            partiallyFullBoard.SetToken(0, 0, PlayerToken.Opponent);

            // act
            var positionPlayed = player.TakeTurn(partiallyFullBoard);

            // assert
            Assert.That(positionPlayed.Column, Is.EqualTo(0));
            Assert.That(positionPlayed.Row, Is.EqualTo(0));
        }

        [Test]
        public void TakeTurn_BoardHasAFewPieces_PlaysInColumn0Row0() {
            // arrange
            BasicBot player = new BasicBot();
            FakeBoard partiallyFullBoard = new FakeBoard();

            partiallyFullBoard.SetBoard(
                new PlayerToken[,] {
                    {PlayerToken.Me,   PlayerToken.Me,       PlayerToken.None},       // Row 0,
                    {PlayerToken.None, PlayerToken.None,     PlayerToken.None},       // Row 1,
                    {PlayerToken.None, PlayerToken.Opponent, PlayerToken.Opponent}}); // Row 2
                    //  Column 0,         Column 1,               Column 2

            // act
            var positionPlayed = player.TakeTurn(partiallyFullBoard);

            // assert
            Assert.That(positionPlayed.Column, Is.EqualTo(0));
            Assert.That(positionPlayed.Row, Is.EqualTo(0));
        }

        // PlayerBoard we can use for testing
        internal class FakeBoard : IPlayerBoard {
            const int BOARD_SIZE = 3;

            private PlayerToken[,] Tokens = new PlayerToken[BOARD_SIZE, BOARD_SIZE];

            internal FakeBoard() {
                InitialiseBoard();
            }

            public PlayerToken TokenAt(BoardPosition position) {
                return Tokens[position.Column, position.Row];
            }

            internal void SetToken(int column, int row, PlayerToken token) {
                Tokens[column, row] = token;
            }

            internal void SetBoard(PlayerToken[,] tokens) {
                // tokens for this will be the 'wrong way round' (rows then columns)
                for (int row = 0; row < BOARD_SIZE; row++) {
                    for (int column = 0; column < BOARD_SIZE; column++) {
                        Tokens[column, row] = tokens[row, column];
                    }
                }
            }

            // Set empty fields to PlayerToken.None, which is what the real board will have (they will not be NULL).
            private void InitialiseBoard() {
                for (int columns = 0; columns < 3; columns++) {
                    for (int rows = 0; rows < 3; rows++) {
                        Tokens[columns, rows] = PlayerToken.None;
                    }
                }
            }
        }
    }
}
