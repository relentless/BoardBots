using System;
using NUnit.Framework;
using BoardBots.Shared;

namespace BoardBots.Tests {
    [TestFixture]
    public class CrossesBoard_Tests {
        [Test]
        public void Copy_Called_CreatesCopyOfBoard() {
            // arrange
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 2));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));

            // act
            var newBoard = board.Copy();

            // assert
            Assert.That(newBoard, Is.Not.EqualTo(board));
            Assert.That(newBoard.TokenAt(new BoardPosition(0, 0)), Is.EqualTo(board.TokenAt(new BoardPosition(0, 0))));
            Assert.That(newBoard.TokenAt(new BoardPosition(1, 0)), Is.EqualTo(board.TokenAt(new BoardPosition(1, 0))));
            Assert.That(newBoard.TokenAt(new BoardPosition(2, 0)), Is.EqualTo(board.TokenAt(new BoardPosition(2, 0))));
            Assert.That(newBoard.TokenAt(new BoardPosition(0, 1)), Is.EqualTo(board.TokenAt(new BoardPosition(0, 1))));
            Assert.That(newBoard.TokenAt(new BoardPosition(1, 1)), Is.EqualTo(board.TokenAt(new BoardPosition(1, 1))));
            Assert.That(newBoard.TokenAt(new BoardPosition(2, 1)), Is.EqualTo(board.TokenAt(new BoardPosition(2, 1))));
            Assert.That(newBoard.TokenAt(new BoardPosition(0, 2)), Is.EqualTo(board.TokenAt(new BoardPosition(0, 2))));
            Assert.That(newBoard.TokenAt(new BoardPosition(1, 2)), Is.EqualTo(board.TokenAt(new BoardPosition(1, 2))));
            Assert.That(newBoard.TokenAt(new BoardPosition(2, 2)), Is.EqualTo(board.TokenAt(new BoardPosition(2, 2))));
        }

        [Test]
        public void For_Player1_CreatesBoardsForPlayer() {
            // arrange
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 2));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));

            // act
            var playerBoard = board.For(GameToken.Player1);

            // assert
            Assert.That(playerBoard.TokenAt(new BoardPosition(0, 0)), Is.EqualTo(PlayerToken.Me));
            Assert.That(playerBoard.TokenAt(new BoardPosition(1, 2)), Is.EqualTo(PlayerToken.Opponent));
            Assert.That(playerBoard.TokenAt(new BoardPosition(2, 1)), Is.EqualTo(PlayerToken.Me));
            Assert.That(playerBoard.TokenAt(new BoardPosition(0, 1)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(1, 0)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(2, 0)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(0, 2)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(1, 1)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(2, 2)), Is.EqualTo(PlayerToken.None));
        }

        [Test]
        public void For_Player2_CreatesBoardsForPlayer() {
            // arrange
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 2));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));

            // act
            var playerBoard = board.For(GameToken.Player2);

            // assert
            Assert.That(playerBoard.TokenAt(new BoardPosition(0, 0)), Is.EqualTo(PlayerToken.Opponent));
            Assert.That(playerBoard.TokenAt(new BoardPosition(1, 2)), Is.EqualTo(PlayerToken.Me));
            Assert.That(playerBoard.TokenAt(new BoardPosition(2, 1)), Is.EqualTo(PlayerToken.Opponent));
            Assert.That(playerBoard.TokenAt(new BoardPosition(0, 1)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(1, 0)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(2, 0)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(0, 2)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(1, 1)), Is.EqualTo(PlayerToken.None));
            Assert.That(playerBoard.TokenAt(new BoardPosition(2, 2)), Is.EqualTo(PlayerToken.None));
        }

        [Test]
        public void SetToken_PositionEmpty_ReturnsValidMove()
        {
            // arrange
            var board = new CrossesBoard();
            
            // act
            var validMovePlayed = board.SetToken(GameToken.Player1, new BoardPosition(0, 0));

            // assert
            Assert.That(validMovePlayed, Is.EqualTo(MoveResult.ValidMove));
        }

        [Test]
        public void SetToken_PositionOccupied_ReturnsInvalidMove()
        {
            // arrange
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));

            // act
            var validMovePlayed = board.SetToken(GameToken.Player2, new BoardPosition(2, 1));

            // assert
            Assert.That(validMovePlayed, Is.EqualTo(MoveResult.PositionTaken));
        }

        [Test]
        public void SetToken_PositionOffBoard_ReturnsInvalidMove() {
            // arrange
            var board = new CrossesBoard();

            // act
            var validMovePlayed = board.SetToken(GameToken.Player1, new BoardPosition(3, -1));

            // assert
            Assert.That(validMovePlayed, Is.EqualTo(MoveResult.OutsideBoardBoundary));
        }
    }
}
