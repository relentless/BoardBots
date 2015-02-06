using System;
using NUnit.Framework;
using BoardBots.Shared;
using NSubstitute;

namespace BoardBots.Tests {
    [TestFixture]
    public class Move_Tests {
        
        [Test]
        public void MoveText_NormalMove_DisplaysMoveNormally() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.ValidMove, moveNumber: 1);

            // act
            var output = move.MoveText();

            // assert
            Assert.That(output, Is.EqualTo("FakePlayer plays at [1,2]"));
        }

        [Test]
        public void MoveText_PositionTaken_DisplaysMoveNormally() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.PositionTaken);

            // act
            var output = move.MoveText();

            // assert
            Assert.That(output, Is.EqualTo("FakePlayer plays at [1,2]"));
        }

        [Test]
        public void MoveText_OutsideBoundary_DisplaysMoveNormally() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(3, 4), MoveResult.OutsideBoardBoundary);

            // act
            var output = move.MoveText();

            // assert
            Assert.That(output, Is.EqualTo("FakePlayer plays at [3,4]"));
        }

        [Test]
        public void MoveText_Timeout_DisplaysFailedMove() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.Timeout);

            // act
            var output = move.MoveText();

            // assert
            Assert.That(output, Is.EqualTo("FakePlayer failed to play"));
        }

        [Test]
        public void MoveText_Exception_DisplaysFailedMove() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.Exception, 0, "Problems!!");

            // act
            var output = move.MoveText();

            // assert
            Assert.That(output, Is.EqualTo("FakePlayer failed to play"));
        }

        [Test]
        public void Information_NormalMove_NoInformation() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.ValidMove, moveNumber: 1);

            // act
            var output = move.Information();

            // assert
            Assert.That(output, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Information_PositionTaken_ReturnsPositionTakenMessage() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.PositionTaken, moveNumber: 1);

            // act
            var output = move.Information();

            // assert
            Assert.That(output, Is.EqualTo("Position already taken"));
        }

        [Test]
        public void Information_OutsideBoundary_ReturnsOutsideBoundaryMessage() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.OutsideBoardBoundary, moveNumber: 1);

            // act
            var output = move.Information();

            // assert
            Assert.That(output, Is.EqualTo("Position is outside board"));
        }

        [Test]
        public void Information_Timeout_DisplaysTimeoutMessage() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.Timeout);

            // act
            var output = move.Information();

            // assert
            Assert.That(output, Is.EqualTo("Move timed out"));
        }

        [Test]
        public void Information_Exception_DisplaysException() {
            // arrange
            var move = new Move(new FakeBoard(), new FakePlayer(), BoardPosition.At(1, 2), MoveResult.Exception, 0, "Problems!!");

            // act
            var output = move.Information();

            // assert
            Assert.That(output, Is.EqualTo("Exception: Problems!!"));
        }
    }

    internal class FakePlayer : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    internal class FakeBoard : CrossesBoard {

        private const int BOARD_SIZE = 3;
        internal GameToken[,] Tokens = new GameToken[BOARD_SIZE, BOARD_SIZE];

        internal FakeBoard() {
            InitialiseBoard();
        }

        public override GameToken TokenAt(BoardPosition position) {
            return Tokens[position.Column, position.Row];
        }

        private void InitialiseBoard() {
            for (int columns = 0; columns < BOARD_SIZE; columns++) {
                for (int rows = 0; rows < BOARD_SIZE; rows++) {
                    Tokens[columns, rows] = GameToken.None;
                }
            }
        }
    }
}
