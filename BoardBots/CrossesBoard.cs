using BoardBots.Shared;
using System;
using System.Collections.Generic;

namespace BoardBots {
    public class CrossesBoard : GameBoard {
        internal const int BOARD_SIZE = 3;
        private GameToken[,] _board = new GameToken[BOARD_SIZE, BOARD_SIZE];

        public CrossesBoard() {
            InitialiseBoard();
        }

        private void InitialiseBoard() {
            for (int columns = 0; columns < BOARD_SIZE; columns++) {
                for (int rows = 0; rows < BOARD_SIZE; rows++) {
                    _board[columns, rows] = GameToken.None;
                }
            }
        }

        public virtual GameToken TokenAt(BoardPosition position) {
            return _board[position.Column, position.Row];
        }

        public MoveResult SetToken(GameToken token, BoardPosition position) {
            if (outsideBoard(position)) {
                return MoveResult.OutsideBoardBoundary;
            }

            if (TokenAt(position) == GameToken.None) {
                _board[position.Column, position.Row] = token;
                return MoveResult.ValidMove;
            }
            return MoveResult.PositionTaken;
        }

        private bool outsideBoard(BoardPosition position) {
            return position.Column < 0 ||
                position.Column > BOARD_SIZE - 1 ||
                position.Row < 0 ||
                position.Row > BOARD_SIZE - 1;
        }

        public CrossesBoard Copy() {
            var boardCopy = new CrossesBoard();

            for (int columns = 0; columns < BOARD_SIZE; columns++) {
                for (int rows = 0; rows < BOARD_SIZE; rows++) {
                    boardCopy._board[columns, rows] = _board[columns, rows];
                }
            }
            return boardCopy;
        }

        public IPlayerBoard For(GameToken player) {
            var playerTokenFor = new Dictionary<GameToken, PlayerToken> {
                {GameToken.Player1, player == GameToken.Player1 ? PlayerToken.Me : PlayerToken.Opponent },
                {GameToken.Player2, player == GameToken.Player2 ? PlayerToken.Me : PlayerToken.Opponent },
                {GameToken.None, PlayerToken.None}
            };

            PlayerBoard boardForPlayer = new PlayerBoard();

            for (int columns = 0; columns < BOARD_SIZE; columns++) {
                for (int rows = 0; rows < BOARD_SIZE; rows++) {

                    var gameToken = _board[columns, rows];
                    boardForPlayer.SetToken(playerTokenFor[gameToken], BoardPosition.At(columns, rows));
                }
            }

            return boardForPlayer;
        }
    }
}
