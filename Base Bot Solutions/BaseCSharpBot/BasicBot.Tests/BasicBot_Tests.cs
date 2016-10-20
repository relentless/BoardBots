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

            // act
            var result = player.TakeTurn(new FakeBoard());

            // assert
            Assert.That(result.Column, Is.EqualTo(0));
            Assert.That(result.Row, Is.EqualTo(0));
        }

        [Test]
        public void TakeTurn_Column0Row0AlreadyHasPiece_PlaysInColumn0Row0Anyway()
        {
            // arrange
            BasicBot player = new BasicBot();
            FakeBoard partiallyFullBoard = new FakeBoard();
            partiallyFullBoard.Tokens[0, 0] = PlayerToken.Opponent;

            // act
            var result = player.TakeTurn(partiallyFullBoard);

            // assert
            Assert.That(result.Column, Is.EqualTo(0));
            Assert.That(result.Row, Is.EqualTo(0));
        }

        [Test]
        public void TakeTurn_BoardHasAFewPieces_PlaysInColumn0Row0() {
            // arrange
            BasicBot player = new BasicBot();
            FakeBoard partiallyFullBoard = new FakeBoard();

            // NOTE: Rows & columns not where you would expect them to be.. 
            
            partiallyFullBoard.Tokens =
                new PlayerToken[,] {
                    {PlayerToken.None,PlayerToken.Me,PlayerToken.Opponent},    // Column 0
                    {PlayerToken.Opponent,PlayerToken.None,PlayerToken.Me},    // Column 1
                    {PlayerToken.None,PlayerToken.Opponent,PlayerToken.None}}; // Column 2
                    //   Row 0             Row 1              Row 2

            // act
            var result = player.TakeTurn(partiallyFullBoard);

            // assert
            Assert.That(result.Column, Is.EqualTo(0));
            Assert.That(result.Row, Is.EqualTo(0));
        }

        // PlayerBoard we can use for testing
        internal class FakeBoard : IPlayerBoard
        {
            internal PlayerToken[,] Tokens = new PlayerToken[3, 3];

            internal FakeBoard()
            {
                InitialiseBoard();
            }

            public PlayerToken TokenAt(BoardPosition position)
            {
                return Tokens[position.Column, position.Row];
            }

            // Set empty fields to PlayerToken.None, which is what the real board will have (they will not be NULL).
            private void InitialiseBoard()
            {
                for (int columns = 0; columns < 3; columns++)
                {
                    for (int rows = 0; rows < 3; rows++)
                    {
                        Tokens[columns, rows] = PlayerToken.None;
                    }
                }
            }
        }
    }
}
