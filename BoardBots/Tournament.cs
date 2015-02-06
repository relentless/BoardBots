using BoardBots.Shared;
using System.Collections.Generic;
using System.Linq;

namespace BoardBots {

    public class Tournament {
        private const int POINTS_FOR_A_DRAW = 1;
        private const int POINTS_FOR_A_WIN = 3;
        private const int POINTS_PER_PLAYER_MULTIPLIER = 2;
        private readonly List<IPlayer> _players;
        private readonly List<Game> _games = new List<Game>();
        private int _gameCount = 0;

        public Dictionary<IPlayer, TournamentStats> TournamentTable { get; private set; }

        public Tournament(List<IPlayer> players) {
            _players = players;

            TournamentTable = new Dictionary<IPlayer, TournamentStats>();
            foreach (var player in _players) {
                TournamentTable.Add(player, new TournamentStats());
            }
        }

        public void ScheduleGames() {
            ScheduleHomeGames();
            ScheduleAwayGames();
        }

        private void ScheduleHomeGames() {
            for (int playerOneIndex = 0; playerOneIndex <= _players.Count; playerOneIndex++) {
                for (int playerTwoIndex = playerOneIndex + 1; playerTwoIndex < _players.Count; playerTwoIndex++) {
                    _games.Add(new Game(_players[playerOneIndex], _players[playerTwoIndex], ++_gameCount));
                }
            }
        }

        private void ScheduleAwayGames() {
            for (int playerOneIndex = _players.Count - 1; playerOneIndex >= 0; playerOneIndex--) {
                for (int playerTwoIndex = playerOneIndex - 1; playerTwoIndex >= 0; playerTwoIndex--) {
                    _games.Add(new Game(_players[playerOneIndex], _players[playerTwoIndex], ++_gameCount));
                }
            }
        }

        public void AllocateGamePoints(Game game)
        {
            TournamentTable[game.Player1].GamesPlayed++;
            TournamentTable[game.Player2].GamesPlayed++;

            if (game.Result == GameStatus.Draw) {
                TournamentTable[game.Player1].GamesDrawn++;
                TournamentTable[game.Player2].GamesDrawn++;

                TournamentTable[game.Player1].GamePoints = TournamentTable[game.Player1].GamePoints + POINTS_FOR_A_DRAW;
                TournamentTable[game.Player2].GamePoints = TournamentTable[game.Player2].GamePoints + POINTS_FOR_A_DRAW;
            }
            if (game.Result == GameStatus.Player1Wins) {
                TournamentTable[game.Player1].GamesWon++;
                TournamentTable[game.Player2].GamesLost++;

                TournamentTable[game.Player1].GamePoints = TournamentTable[game.Player1].GamePoints + POINTS_FOR_A_WIN;
            }
            if (game.Result == GameStatus.Player2Wins) {
                TournamentTable[game.Player1].GamesLost++;
                TournamentTable[game.Player2].GamesWon++;

                TournamentTable[game.Player2].GamePoints = TournamentTable[game.Player2].GamePoints + POINTS_FOR_A_WIN;
            }
        }

        public void AllocateTournamentPoints()
        {
            var rankPlayersByGamePoints = RankPlayers();

            var maxTournamentPoints = CalculateMaxTournamentPoints();
            var currentTournamentPoints = maxTournamentPoints;
            var currentIndex = 0;

            foreach (var keyValuePair in rankPlayersByGamePoints) {
                if ((currentIndex > 0) && (keyValuePair.Value.GamePoints < rankPlayersByGamePoints[currentIndex - 1].Value.GamePoints)) {
                    currentTournamentPoints = maxTournamentPoints - (currentIndex * POINTS_PER_PLAYER_MULTIPLIER);
                }

                TournamentTable[keyValuePair.Key].TournamentPoints = TournamentTable[keyValuePair.Key].TournamentPoints + currentTournamentPoints;
                currentIndex++;
            }
        }

        private int CalculateMaxTournamentPoints()
        {
            return _players.Count * POINTS_PER_PLAYER_MULTIPLIER;
        }

        public List<KeyValuePair<IPlayer, TournamentStats>> RankPlayers() {
            List<KeyValuePair<IPlayer, TournamentStats>> myList = TournamentTable.ToList();

            myList.Sort((firstPair, nextPair) => firstPair.Value.GamePoints.CompareTo(nextPair.Value.GamePoints));

            return myList.OrderByDescending(key => key.Value.GamePoints).ToList();
        }

        public IReadOnlyList<Game> Games {
            get {
                return _games.AsReadOnly();
            }
        }
    }
}
