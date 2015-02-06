using System;
using NUnit.Framework;
using BoardBots.Shared;

namespace BoardBots.Tests {
    [TestFixture]
    public class GameChecker_Tests {
        [Test]
        public void GetStatusFrom_PartiallyFilledBoard_ReturnsOngoing() {
            // arrange
            var checker = new GameChecker();
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 2));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));

            // act
            var status = checker.GetStatusFrom(board);

            // assert
            Assert.That(status, Is.EqualTo(GameStatus.Ongoing));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetStatusFrom_PlayerWinsColumn_ReturnsPlayer(int column) {
            // arrange
            var checker = new GameChecker();
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(column, 0));
            board.SetToken(GameToken.Player1, new BoardPosition(column, 1));
            board.SetToken(GameToken.Player1, new BoardPosition(column, 2));

            // act
            var status = checker.GetStatusFrom(board);

            // assert
            Assert.That(status, Is.EqualTo(GameStatus.Player1Wins));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetStatusFrom_PlayerWinsRow_ReturnsPlayer(int row) {
            // arrange
            var checker = new GameChecker();
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, row));
            board.SetToken(GameToken.Player1, new BoardPosition(1, row));
            board.SetToken(GameToken.Player1, new BoardPosition(2, row));

            // act
            var status = checker.GetStatusFrom(board);

            // assert
            Assert.That(status, Is.EqualTo(GameStatus.Player1Wins));
        }

        [TestCase(0,0,1,1,2,2)]
        [TestCase(0,2,1,1,2,0)]
        public void GetStatusFrom_PlayerWinsDiagonal_ReturnsPlayer(int column1, int row1, int column2, int row2, int column3, int row3) {
            // arrange
            var checker = new GameChecker();
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(column1, row1));
            board.SetToken(GameToken.Player1, new BoardPosition(column2, row2));
            board.SetToken(GameToken.Player1, new BoardPosition(column3, row3));

            // act
            var status = checker.GetStatusFrom(board);

            // assert
            Assert.That(status, Is.EqualTo(GameStatus.Player1Wins));
        }

        [Test]
        public void GetStatusFrom_Player2Wins_ReturnsPlayer2() {
            // arrange
            var checker = new GameChecker();
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player2, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(0, 1));
            board.SetToken(GameToken.Player2, new BoardPosition(0, 2));

            // act
            var status = checker.GetStatusFrom(board);

            // assert
            Assert.That(status, Is.EqualTo(GameStatus.Player2Wins));
        }

        [Test]
        public void GetStatusFrom_FilledBoardNoWinner_ReturnsDraw() {
            // arrange
            var checker = new GameChecker();
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player1, new BoardPosition(0, 1));
            board.SetToken(GameToken.Player1, new BoardPosition(1, 2));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 0));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));
            board.SetToken(GameToken.Player2, new BoardPosition(0, 2));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 1));
            board.SetToken(GameToken.Player2, new BoardPosition(2, 2));

            // act
            var status = checker.GetStatusFrom(board);

            // assert
            Assert.That(status, Is.EqualTo(GameStatus.Draw));
        }
    }
}
