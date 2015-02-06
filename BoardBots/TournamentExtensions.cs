using BoardBots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBots {
    public static class TournamentExtensions {

        private const string _header = @"
┌────────────────────────────┬────────┬─────┬───────┬──────┬────────┐
│ Player                     │ Played │ Won │ Drawn │ Lost │ Points │
";
        private const string HORIZONTAL_SPLIT = "├────────────────────────────┼────────┼─────┼───────┼──────┼────────┤\r\n";
        private const string _footer = @"└────────────────────────────┴────────┴─────┴───────┴──────┴────────┘";
        private const int NAME_LENGTH = 26;
        private const int GAME_COUNT_LENGTH = 3;
        private const int SCORE_LENGTH = 4;

        public static string AsTable(this List<KeyValuePair<IPlayer, TournamentStats>> score) {

            var output = new StringBuilder(_header);

            foreach (var result in score) {
                output.Append(HORIZONTAL_SPLIT);
                output.Append(string.Format("│ {0} │    {1} │ {2} │   {3} │  {4} │   {5} │\r\n",
                    result.Key.GetName().FormattedToLength(NAME_LENGTH),
                    result.Value.GamesPlayed.FormattedToLength(GAME_COUNT_LENGTH),
                    result.Value.GamesWon.FormattedToLength(GAME_COUNT_LENGTH),
                    result.Value.GamesDrawn.FormattedToLength(GAME_COUNT_LENGTH),
                    result.Value.GamesLost.FormattedToLength(GAME_COUNT_LENGTH),
                    result.Value.TournamentPoints.FormattedToLength(SCORE_LENGTH)));
            }

            output.Append(_footer);

            return output.ToString();
        }

        private static string FormattedToLength(this string text, int length) {
            return text.PadRight(length, ' ').Substring(0, length);
        }

        private static string FormattedToLength(this int number, int length) {
            return number.ToString().PadLeft(length, ' ').Substring(0, length);
        }
    }
}
