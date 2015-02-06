using BoardBots;
using BoardBots.Shared;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System;

namespace BoardBots.Tests
{
    [TestFixture]
    public class Game_Tests
    {
        [Test]
        public void PlayTurn_Called_GetsMoveFromPlayer1(){
            // arrange
            var player1 = CreateFakePlayer();
            var game = new Game(player1, CreateFakePlayer());

            // act
            game.PlayTurn();

            // assert
            player1.Received().TakeTurn(Arg.Any<IPlayerBoard>());
        }

        [Test]
        public void PlayTurn_Called_GetsMoveFromPlayer2() {
            // arrange
            var player2 = CreateFakePlayer();
            var game = new Game(CreateFakePlayer(), player2);

            // act
            game.PlayTurn();

            // assert
            player2.Received().TakeTurn(Arg.Any<IPlayerBoard>());
        }

        [Test]
        public void PlayTurn_Player1Moves_MoveRecorded() {
            // arrange
            var player1 = CreateFakePlayer();
            var game = new Game(player1, CreateFakePlayer());

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Player, Is.EqualTo(player1));
        }

        [Test]
        public void PlayTurn_Player2Moves_MoveRecorded() {
            // arrange
            var player2 = CreateFakePlayer();
            var game = new Game(CreateFakePlayer(), player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[1].Player, Is.EqualTo(player2));
        }

        [Test]
        public void PlayTurn_Player1Moves_BoardUpdated() {
            // arrange
            var player1 = CreateFakePlayer();
            player1.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(new BoardPosition(1, 1));
            var game = new Game(player1, CreateFakePlayer());

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Board.TokenAt(new BoardPosition(1,1)), Is.EqualTo(GameToken.Player1));
        }

        [Test]
        public void PlayTurn_Player2Moves_BoardUpdated() {
            // arrange
            var player2 = CreateFakePlayer();
            player2.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(new BoardPosition(2, 2));
            var game = new Game(CreateFakePlayer(), player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[1].Board.TokenAt(new BoardPosition(2, 2)), Is.EqualTo(GameToken.Player2));
        }

        [Test]
        public void PlayTurn_MultipleMovesTaken_MovesHaveOwnCopyOfBoard() {
            // arrange
            var game = new Game(CreateFakePlayer(), CreateFakePlayer());
            
            // act
            game.PlayTurn(); // (2 player turns)

            // assert
            Assert.That(game.Moves[0].Board, Is.Not.EqualTo(game.Moves[1].Board));
        }

        [Test]
        public void PlayTurn_PlayersMove_MoveNumbersRecorded() {
            // arrange
            var game = new Game(CreateFakePlayer(), CreateFakePlayer());

            // act
            game.PlayTurn();
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Number, Is.EqualTo(1));
            Assert.That(game.Moves[1].Number, Is.EqualTo(2));
            Assert.That(game.Moves[2].Number, Is.EqualTo(3));
            Assert.That(game.Moves[3].Number, Is.EqualTo(4));
        }

        [Test]
        public void PlayTurn_Player1Wins_Player2NotCalled() {
            // arrange
            var fakeGameChecker = Substitute.For<IGameChecker>();
            fakeGameChecker.GetStatusFrom(Arg.Any<CrossesBoard>()).Returns(GameStatus.Player1Wins);
            var player2 = CreateFakePlayer();
            var game = new Game(CreateFakePlayer(), player2, fakeGameChecker);

            // act
            game.PlayTurn();

            // assert
            player2.DidNotReceive().TakeTurn(Arg.Any<IPlayerBoard>());
        }

        [Test]
        public void Play_Player1Wins_NoMoreMovesPlayed() {
            // arrange
            var fakeGameChecker = Substitute.For<IGameChecker>();
            fakeGameChecker.GetStatusFrom(Arg.Any<CrossesBoard>()).Returns(GameStatus.Player1Wins);
            var game = new Game(CreateFakePlayer(), CreateFakePlayer(), fakeGameChecker);

            // act
            game.Play();

            // assert
            Assert.That(game.Moves.Count, Is.EqualTo(1));
        }

        [Test]
        public void Play_PlayerWins_WinnerRecorded() {
            // arrange
            var fakeGameChecker = Substitute.For<IGameChecker>();
            fakeGameChecker.GetStatusFrom(Arg.Any<CrossesBoard>()).Returns(GameStatus.Player1Wins);
            var game = new Game(CreateFakePlayer(), CreateFakePlayer(), fakeGameChecker);

            // act
            game.Play();

            // assert
            Assert.That(game.Result, Is.EqualTo(GameStatus.Player1Wins));
        }

        [Test]
        public void ResultText_Player1Won_ReturnsTextWithPlayer1Name() {
            // arrange
            var fakeGameChecker = Substitute.For<IGameChecker>();
            fakeGameChecker.GetStatusFrom(Arg.Any<CrossesBoard>()).Returns(GameStatus.Player1Wins);
            var game = new Game(new DummyPlayer(), CreateFakePlayer(), fakeGameChecker);

            // act
            game.Play();

            // assert
            Assert.That(game.ResultText, Is.EqualTo("DummyPlayer Wins!"));
        }

        [Test]
        public void ResultText_Player2Won_ReturnsTextWithPlayer2Name() {
            // arrange
            var fakeGameChecker = Substitute.For<IGameChecker>();
            fakeGameChecker.GetStatusFrom(Arg.Any<CrossesBoard>()).Returns(GameStatus.Player2Wins);
            var game = new Game(CreateFakePlayer(), new DummyPlayer2(), fakeGameChecker);

            // act
            game.Play();

            // assert
            Assert.That(game.ResultText, Is.EqualTo("DummyPlayer2 Wins!"));
        }

        [Test]
        public void ResultText_Draw_ReturnsTextWithDraw() {
            // arrange
            var fakeGameChecker = Substitute.For<IGameChecker>();
            fakeGameChecker.GetStatusFrom(Arg.Any<CrossesBoard>()).Returns(GameStatus.Draw);
            var game = new Game(CreateFakePlayer(), CreateFakePlayer(), fakeGameChecker);

            // act
            game.Play();

            // assert
            Assert.That(game.ResultText, Is.EqualTo("Draw!"));
        }

        [Test]
        public void PlayTurn_Called_PlayerGetsPersonalCopyOfBoard() {
            // arrange
            var player1 = CreateFakePlayer();
            player1.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(new BoardPosition(1, 1));
            var player2 = CreateFakePlayer();
            var game = new Game(player1, player2);

            // act
            game.PlayTurn();

            // assert
            player2.Received().TakeTurn(Arg.Is<PlayerBoard>(x => x.TokenAt(BoardPosition.At(1,1)) == PlayerToken.Opponent));
        }

        [Test]
        public void Play_GameStillRunningAfter10Rounds_DeclaredADraw() {
            // arrange
            var player1 = CreateFakePlayer();
            player1.Take10TurnsAt(BoardPosition.At(1, 1));

            var player2 = CreateFakePlayer();
            player2.Take10TurnsAt(BoardPosition.At(1, 1));

            var game = new Game(player1, player2);

            // act
            game.Play();

            // assert
            Assert.That(game.Result, Is.EqualTo(GameStatus.Draw));
        }

        [Test]
        public void Play_GameStillRunningAfter10Rounds_GameMessageSet() {
            // arrange
            var player1 = CreateFakePlayer();
            player1.Take10TurnsAt(BoardPosition.At(1, 1));

            var player2 = CreateFakePlayer();
            player2.Take10TurnsAt(BoardPosition.At(1, 1));

            var game = new Game(player1, player2);

            // act
            game.Play();

            // assert
            Assert.That(game.Message, Is.EqualTo("Game stopped after 10 rounds"));
        }

        [Test]
        public void Play_PlayerWinsOnTenthTurn_PlayerWins() {
            // arrange
            var player1 = CreateFakePlayer();
            player1.Take10TurnsAt(BoardPosition.At(1, 1));

            var player2 = CreateFakePlayer();
            player2.TakeTurn(Arg.Any<IPlayerBoard>()).
                Returns(
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 0),
                    BoardPosition.At(0, 1),
                    BoardPosition.At(0, 2));

            var game = new Game(player1, player2);

            // act
            game.Play();

            // assert
            Assert.That(game.Result, Is.EqualTo(GameStatus.Player2Wins));
        }

        [Test]
        public void PlayTurn_PlayerTakesAges_MoveNotAccepted() {
            // arrange
            var player1 = CreateFakePlayerWhichTakesAgesToPlay(BoardPosition.At(2,2));
            var player2 = CreateFakePlayer();
            var game = new Game(player1, player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Board.TokenAt(BoardPosition.At(2, 2)) != GameToken.Player1);
        }

        [Test]
        public void PlayTurn_PlayerTakesAges_MoveResultSetToTimeout() {
            // arrange
            var player1 = CreateFakePlayerWhichTakesAgesToPlay(BoardPosition.At(2, 2));
            var player2 = CreateFakePlayer();
            var game = new Game(player1, player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Result, Is.EqualTo(MoveResult.Timeout));
        }

        [Test]
        public void PlayTurn_PlayerThrowsException_MoveResultSetToException() {
            // arrange
            var player1 = CreateFakePlayerWhichThrows( new Exception("Bad things!"));
            var player2 = CreateFakePlayer();
            var game = new Game(player1, player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Result, Is.EqualTo(MoveResult.Exception));
        }

        [Test]
        public void PlayTurn_PlayerThrowsException_ExceptionMessageAddedToMove() {
            // arrange
            var player1 = CreateFakePlayerWhichThrows(new Exception("Bad things!"));
            var player2 = CreateFakePlayer();
            var game = new Game(player1, player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Message, Is.EqualTo("Bad things!"));
        }

        [Test]
        public void PlayTurn_PlayerPlaysOffBoard_MoveResultSetToOutsideBoundary() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            player1.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(new BoardPosition(3, 0));
            var player2 = CreateFakePlayer();
            var game = new Game(player1, player2);

            // act
            game.PlayTurn();

            // assert
            Assert.That(game.Moves[0].Result, Is.EqualTo( MoveResult.OutsideBoardBoundary));
        }

        private IPlayer CreateFakePlayer() {
            var player = Substitute.For<IPlayer>();
            player.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(new BoardPosition(0, 0));
            return player;
        }

        private IPlayer CreateFakePlayerWhichTakesAgesToPlay(BoardPosition positionPlayed) {
            var player = Substitute.For<IPlayer>();
            player.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(x => {
                Thread.Sleep(2000);
                return positionPlayed;
            });
            return player;
        }

        private IPlayer CreateFakePlayerWhichThrows(Exception exceptionToThrow) {
            var player = Substitute.For<IPlayer>();
            player.TakeTurn(Arg.Any<IPlayerBoard>()).Returns(x => {
                throw exceptionToThrow;
            });
            return player;
        }
    }

    internal class DummyPlayer : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    internal class DummyPlayer2 : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    internal static class PlayerExtensions {
        internal static void Take10TurnsAt(this IPlayer player, BoardPosition position) {
            player.TakeTurn(Arg.Any<IPlayerBoard>()).
                Returns(
                    position,
                    position,
                    position,
                    position,
                    position,
                    position,
                    position,
                    position,
                    position,
                    position);
        }
    }
}
