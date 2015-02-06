using BoardBots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBots {
    public class GameChecker: IGameChecker {

        private List<BoardPosition[]> _winningRows = new List<BoardPosition[]> {
            // columns
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(0,0), BoardPosition.At(0,1), BoardPosition.At(0,2)},
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(1,0), BoardPosition.At(1,1), BoardPosition.At(1,2)},
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(2,0), BoardPosition.At(2,1), BoardPosition.At(2,2)},

            // rows
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(0,0), BoardPosition.At(1,0), BoardPosition.At(2,0)},
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(0,1), BoardPosition.At(1,1), BoardPosition.At(2,1)},
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(0,2), BoardPosition.At(1,2), BoardPosition.At(2,2)},

            // diagonals
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(0,0), BoardPosition.At(1,1), BoardPosition.At(2,2)},
            new BoardPosition[CrossesBoard.BOARD_SIZE] { BoardPosition.At(0,2), BoardPosition.At(1,1), BoardPosition.At(2,0)},
        };

        public GameStatus GetStatusFrom(CrossesBoard board) {

            foreach (var winningRow in _winningRows) {
                if(winningRow.All( position => board.TokenAt(position) == GameToken.Player1 )){
                    return GameStatus.Player1Wins;
                }

                if (winningRow.All(position => board.TokenAt(position) == GameToken.Player2)) {
                    return GameStatus.Player2Wins;
                }
            }

            if (!EmptySpacesExist(board)) {
                return GameStatus.Draw;
            }

            return GameStatus.Ongoing;
        }

        private static bool EmptySpacesExist(CrossesBoard board) {

            for (int rowIndex = 0; rowIndex < CrossesBoard.BOARD_SIZE; rowIndex++) {
                for (int columnIndex = 0; columnIndex < CrossesBoard.BOARD_SIZE; columnIndex++) {
                    if (board.TokenAt(BoardPosition.At(columnIndex, rowIndex)) == GameToken.None) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
