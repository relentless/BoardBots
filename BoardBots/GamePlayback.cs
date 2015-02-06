using System;
using System.Threading;

namespace BoardBots {
    public static class GamePlayback {
        internal static void Show(Game game) {

            Console.Clear();
            ShowGameHeader(game);
            Thread.Sleep(1500);

            var previousBoard = new CrossesBoard();

            foreach (var move in game.Moves) {
                DisplayMove(game, move, previousBoard, showInfo: false);
                DisplayHelper.ShowAnyKeyToSkipGameMessage();

                Thread.Sleep(800);
                if (SkipRequested()) {
                    return;
                }

                DisplayMove(game, move, move.Board, showInfo: true);

                if (move != LastMove(game)) {
                    DisplayHelper.ShowAnyKeyToSkipGameMessage();

                    Thread.Sleep(GetPauseTimeFor(move));

                    if (SkipRequested()) {
                        return;
                    }
                }
                
                previousBoard = move.Board;
            }

            ShowGameResults(game);

            DisplayHelper.AnyKeyToContinue("\n\n[Press any key to continue...]");
        }

        private static void ShowGameResults(Game game) {
            DisplayHelper.SetTextColour(DisplayHelper.ColourFor(game.Result));
            Console.WriteLine("** {0} {1} **", game.ResultText, game.Message);
            DisplayHelper.ResetTextColour();
        }

        private static int GetPauseTimeFor(Move move) {
            return string.IsNullOrEmpty(move.Information()) ? 1200 : 2000;
        }

        private static void DisplayMove(Game game, Move move, CrossesBoard board, bool showInfo) {
            Console.Clear();
            ShowGameHeader(game);
            Console.WriteLine("Move {0}: {1}", move.Number, move.MoveText());
            Console.WriteLine(BoardVisualiser.ToString(board));

            if (showInfo) {
                DisplayHelper.SetTextColour(ConsoleColor.Red);
                Console.WriteLine(move.Information());
                DisplayHelper.ResetTextColour();
            }
            else{
                Console.WriteLine();
            }
        }

        private static void ShowGameHeader(Game game) {
            Console.WriteLine("\nGame {0}: {1} ('X') vs {2} ('O')\n", game.Number, game.Player1.GetName(), game.Player2.GetName());
        }

        private static Move LastMove(Game game) {
            return game.Moves[game.Moves.Count - 1];
        }

        private static bool SkipRequested() {
            if (Console.KeyAvailable) {
                ClearInputBuffer();

                Console.WriteLine("\nSkipping game");
                Thread.Sleep(400);
                return true;
            }

            return false;
        }

        private static void ClearInputBuffer() {
            while (Console.KeyAvailable) {
                Console.ReadKey();
            }
        }
    }
}
