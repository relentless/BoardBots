using BoardBots;
using BoardBots.Shared;
using NUnit.Framework;

namespace BoardBots.Tests {
    [TestFixture]
    public class BoardVisualiser_Tests {

        [Test]
        public void ToString_EmptyBoard_DrawsBoard() {
            // arrange
            var board = new CrossesBoard();

            // act
            var output = BoardVisualiser.ToString(board);

            // assert
            Assert.That(output, Is.EqualTo(@"
    0   1   2
  ┌───┬───┬───┐
0 │   │   │   │
  ├───┼───┼───┤
1 │   │   │   │
  ├───┼───┼───┤
2 │   │   │   │
  └───┴───┴───┘
"));
        }

        [Test]
        public void ToString_BoardWithMixedTokens_DrawsBoard() {
            // arrange
            var board = new CrossesBoard();
            board.SetToken(GameToken.Player1, new BoardPosition(0, 0));
            board.SetToken(GameToken.Player2, new BoardPosition(1, 2));
            board.SetToken(GameToken.Player1, new BoardPosition(2, 1));

            // act
            var output = BoardVisualiser.ToString(board);

            // assert
            Assert.That(output, Is.EqualTo(@"
    0   1   2
  ┌───┬───┬───┐
0 │ X │   │   │
  ├───┼───┼───┤
1 │   │   │ X │
  ├───┼───┼───┤
2 │   │ O │   │
  └───┴───┴───┘
"));
        }
    }
}
