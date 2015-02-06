using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBots.Shared {
    public class PlayerBoard: IPlayerBoard  {

        private const int BOARD_SIZE = 3;
        private PlayerToken[,] _board = new PlayerToken[3, 3];

        internal PlayerBoard() { 
            InitialiseBoard();
        }

        internal void SetToken(PlayerToken token, BoardPosition position) {
            _board[position.Column, position.Row] = token;
        }

        public PlayerToken TokenAt(BoardPosition position) {
            return _board[position.Column, position.Row];
        }

        private void InitialiseBoard() {
            for (int columns = 0; columns < BOARD_SIZE; columns++) {
                for (int rows = 0; rows < BOARD_SIZE; rows++) {
                    _board[columns, rows] = PlayerToken.None;
                }
            }
        }
    }
}
