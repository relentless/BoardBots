using System;
using NUnit.Framework;
using BoardBots.Shared;

namespace BoardBots.Tests {
    [TestFixture]
    public class PlayerExtensions_Tests {
        [Test]
        public void GetName_Called_ReturnsLastPartOfTypeNameAfterFinalDot() {
            // arrange
            var dummyPlayer = new BoardBots.Tests.AnotherLayerOfNamespace.DummyPlayer();

            // act
            var name = dummyPlayer.GetName();

             // assert
            Assert.That(name, Is.EqualTo("DummyPlayer"));
        }
    }

    namespace AnotherLayerOfNamespace {
        internal class DummyPlayer : IPlayer {

            public BoardPosition TakeTurn(IPlayerBoard board) {
                throw new NotImplementedException();
            }
        }
    }
}
