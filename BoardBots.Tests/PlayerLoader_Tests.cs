using BoardBots.Shared;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BoardBots.Tests {
    [TestFixture]
    public class PlayerLoader_Tests {
        [Test]
        public void LoadPlayersFrom_SingleTypeWithValidPlayer_ReturnsPlayer() {
            // arrange

            // act
            var players = new PlayerLoader().LoadPlayersFrom(new List<Type> { typeof(Fakeplayer) });

            // assert
            Assert.That(players.Count, Is.EqualTo(1));
            Assert.That(players[0].GetType(), Is.EqualTo(typeof(Fakeplayer)));
        }

        [Test]
        public void LoadPlayersFrom_PlayerNotClass_SetsValidationError() {
            // arrange
            var loader = new PlayerLoader();

            // act
            loader.LoadPlayersFrom(new List<Type> { typeof(InterfacePlayer) });

            // assert
            Assert.That(loader.ValidationErrors, Has.Exactly(1).EqualTo("BoardBots.Tests.InterfacePlayer is not a class"));
        }

        [Test]
        public void LoadPlayersFrom_NoParameterlessConstructor_SetsValidationError() {
            // arrange
            var loader = new PlayerLoader();

            // act
            loader.LoadPlayersFrom(new List<Type> { typeof(ComplexConstructorPlayer) });

            // assert
            Assert.That(loader.ValidationErrors, Has.Exactly(1).EqualTo("BoardBots.Tests.ComplexConstructorPlayer does not have a parameterless constructor"));
        }

        [Test]
        public void LoadPlayersFrom_ConstructorThrowsException_SetsValidationError() {
            // arrange
            var loader = new PlayerLoader();

            // act
            loader.LoadPlayersFrom(new List<Type> { typeof(ExceptionalPlayer) });

            // assert
            Assert.That(loader.ValidationErrors, Has.Exactly(1).EqualTo("BoardBots.Tests.ExceptionalPlayer threw an exception while loading: Can't load me, sucka!"));
        }
    }

    public class Fakeplayer : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    public class NotPlayer {}

    public interface InterfacePlayer: IPlayer {}

    public class ComplexConstructorPlayer : IPlayer {
        public ComplexConstructorPlayer(int randomParam) { }

        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    public class ExceptionalPlayer : IPlayer {
        public ExceptionalPlayer() {
            throw new Exception("Can't load me, sucka!");
        }

        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }
}
