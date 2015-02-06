namespace BoardBots {
    public class TournamentStats {

        public TournamentStats() {
            GamesPlayed = 0;
            GamesWon = 0;
            GamesDrawn = 0;
            GamesLost = 0;
            GamePoints = 0;
            TournamentPoints = 0;
        }

        public int TournamentPoints { get; set; }

        public int GamePoints { get; set; }

        public int GamesLost { get; set; }

        public int GamesDrawn { get; set; }

        public int GamesWon { get; set; }

        public int GamesPlayed { get; set; }

        public override string ToString()
        {
            return "\t" + GamesPlayed + "\t" + GamesWon + "\t" + GamesDrawn + "\t" + GamesLost + "\t" + GamePoints +
                   "\t" + TournamentPoints;
        }
    }
}
