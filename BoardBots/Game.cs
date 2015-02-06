using BoardBots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoardBots {
    public class Game {
        private const int MAX_TURNS = 10;
        private const int TURN_TIMEOUT_MILLISECONDS = 500;
        
        private IPlayer _player1;
        private IPlayer _player2;
        private CrossesBoard _board = new CrossesBoard();
        private List<Move> _moves = new List<Move>();
        private Dictionary<IPlayer, GameToken> _gameTokenFor;
        private IGameChecker _checker;
        protected GameStatus _result = GameStatus.Ongoing;
        private string _message = string.Empty;
        private int _moveCount = 0;
        private int _gameNumber = 0;

        public Game(IPlayer player1, IPlayer player2, IGameChecker checker) {
            _player1 = player1;
            _player2 = player2;
            _checker = checker;

            _gameTokenFor = new Dictionary<IPlayer, GameToken> {
                {_player1, GameToken.Player1},
                {_player2, GameToken.Player2}
            };
        }

        public Game(IPlayer player1, IPlayer player2):
            this(player1, player2, new GameChecker()) {
        }

        public Game(IPlayer player1, IPlayer player2, int gameNumber) :
            this(player1, player2, new GameChecker()) {
            _gameNumber = gameNumber;
        }

        public List<Move> Moves {
            get {
                return _moves;
            }
        }

        public GameStatus Result {
            get {
                return _result;
            }
        }

        public string ResultText {
            get {
                switch (_result) {
                    case GameStatus.Player1Wins:
                        return _player1.GetName() + " Wins!";
                    case GameStatus.Player2Wins:
                        return _player2.GetName() + " Wins!";
                    case GameStatus.Draw:
                        return "Draw!";
                    default:
                        return "Result undetermined";
                }
            }
        }

        public string Message {
            get {
                return _message;
            }
        }

        public int Number {
            get {
                return _gameNumber;
            }
        }

        public void Play() {
            var gameStatus = GameStatus.Ongoing;
            int turnCount = 0;

            while (gameStatus == GameStatus.Ongoing && turnCount < MAX_TURNS) {
                gameStatus = PlayTurn();
                turnCount++;
            }

            if (turnCount >= MAX_TURNS && gameStatus == GameStatus.Ongoing) {
                gameStatus = GameStatus.Draw;
                _message = string.Format("Game stopped after {0} rounds", MAX_TURNS );
            }

            _result = gameStatus;
        }

        public GameStatus PlayTurn() {
            var statusAfterPlayer1 = PlaySinglePlayerTurn(_player1);
            if (statusAfterPlayer1 != GameStatus.Ongoing) {
                return statusAfterPlayer1;
            }

            var statusAfterPlayer2 = PlaySinglePlayerTurn(_player2);
            return statusAfterPlayer2;
        }

        private GameStatus PlaySinglePlayerTurn(IPlayer player) {
            var status = GameStatus.Ongoing;
            MoveResult moveResult = MoveResult.Unknown;
            BoardPosition positionPlayed = null;
            string moveMessage = string.Empty;

            try {
                positionPlayed = PlayTurnWithTimeout(player);
                moveResult = _board.SetToken(_gameTokenFor[player], positionPlayed);
            }
            catch (TimeoutException) {
                moveResult = MoveResult.Timeout;
            }
            catch (Exception ex) {
                moveResult = MoveResult.Exception;
                moveMessage = ex.Message;
            }

            Moves.Add(new Move(_board.Copy(), player, positionPlayed, moveResult, ++_moveCount, moveMessage));

            if (moveResult == MoveResult.ValidMove) {
                status = _checker.GetStatusFrom(_board);
            }

            return status;
        }

        private BoardPosition PlayTurnWithTimeout(IPlayer player) {
            Thread threadToKill = null;
            Func<IPlayer, BoardPosition> wrappedAction = (x) => {
                threadToKill = Thread.CurrentThread;
                return PlayTurn(x);
            };

            IAsyncResult result = wrappedAction.BeginInvoke(player, null, null);
            if (result.AsyncWaitHandle.WaitOne(TURN_TIMEOUT_MILLISECONDS)) {
                return wrappedAction.EndInvoke(result);
            }
            else {
                threadToKill.Abort();
                throw new TimeoutException();
            }
        }

        private BoardPosition PlayTurn(IPlayer player) {

            return player.TakeTurn(_board.For(_gameTokenFor[player]));
        }

        public IPlayer Player1 {
            get {
                return _player1;
            }
        }

        public IPlayer Player2 {
            get {
                return _player2;
            }
        }
    }
}
