using BoardBots.Shared;

namespace BoardBots.Tests {
    internal class TestableGame: Game {
        internal TestableGame(IPlayer player1, IPlayer player2)
            : base(player1, player2) {
        }

        internal void SetResult(GameStatus result){
            _result = result;
        }
    }
}
