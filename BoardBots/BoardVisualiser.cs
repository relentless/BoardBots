using BoardBots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBots {
    public static class BoardVisualiser {
        public static string ToString(CrossesBoard board) {

            const string TOP_LINE = "  ┌───┬───┬───┐";
            const string HORIZONTAL_LINE = "  ├───┼───┼───┤";
            const string VERTICAL_SECTION = "│";
            const string BOTTOM_LINE = "  └───┴───┴───┘";
            const string NEWLINE = "\r\n";

            Dictionary<GameToken, string> displayToken = new Dictionary<GameToken, string> {
                {GameToken.Player1, "X"},
                {GameToken.Player2, "O"},
                {GameToken.None, " "},
            };

            var output = new StringBuilder(NEWLINE);
            output.AppendLine("    0   1   2");
            output.AppendLine(TOP_LINE);

            for (int rows = 0; rows < CrossesBoard.BOARD_SIZE; rows++) {
                output.Append(rows + " " );
                for (int columns = 0; columns < CrossesBoard.BOARD_SIZE; columns++) {
                    output.Append(VERTICAL_SECTION + " ");
                    output.Append(displayToken[board.TokenAt(new BoardPosition(columns, rows))] + " ");
                }

                output.AppendLine(VERTICAL_SECTION);

                if (rows < CrossesBoard.BOARD_SIZE - 1) {
                    output.AppendLine(HORIZONTAL_LINE);
                }
            }

            output.AppendLine(BOTTOM_LINE);

            return output.ToString();
        }
    }
}
