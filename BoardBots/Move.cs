using BoardBots.Shared;
using System;

namespace BoardBots {
    public class Move {
        private CrossesBoard _boardAfterMove;
        private IPlayer _player;
        private string _message;
        private BoardPosition _position;
        private MoveResult _moveResult;
        private int _moveNumber;

        public Move(CrossesBoard boardAfterMove, IPlayer player, BoardPosition positionPlayed, MoveResult isValidMove, int moveNumber = 1, string message = "") {
            _boardAfterMove = boardAfterMove;
            _player = player;
            _position = positionPlayed;
            _message = message;
            _moveResult = isValidMove;
            _moveNumber = moveNumber;
        }

        public IPlayer Player {
            get {
                return _player;
            }
        }

        public CrossesBoard Board {
            get {
                return _boardAfterMove;
            }
        }

        public string Message {
            get {
                return _message;
            }
        }

        public MoveResult Result {
            get {
                return _moveResult;
            }
        }

        public int Number {
            get {
                return _moveNumber;
            }
        }

        public string MoveText() {

            switch (_moveResult) {
                case MoveResult.ValidMove:
                case MoveResult.PositionTaken:
                case MoveResult.OutsideBoardBoundary:
                    return string.Format("{1} plays at [{2},{3}]", _moveNumber, _player.GetName(), _position.Column, _position.Row);
                case MoveResult.Timeout:
                case MoveResult.Exception:
                    return string.Format("{1} failed to play", _moveNumber, _player.GetName());
                default:
                    throw new InvalidOperationException("Displaying move: Unrecognised MoveResult");
            }
        }

        public string Information() {
            switch (_moveResult) {
                case MoveResult.PositionTaken:
                    return "Position already taken";
                case MoveResult.OutsideBoardBoundary:
                    return "Position is outside board";
                case MoveResult.Timeout:
                    return "Move timed out";
                case MoveResult.Exception:
                    return string.Format("Exception: {0}", _message);
                default:
                    return string.Empty;
            }
        }
    }
}
