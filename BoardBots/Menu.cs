using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BoardBots {
    public class Menu {
        private Tournament _tournament;

        public Menu(Tournament tournament) {
            _tournament = tournament;
        }

        public int ChooseGame() {

            var validGameChosen = false;
            int choice = -1;

            Console.Clear();

            Console.WriteLine("\nGames:\n");
            foreach (var game in _tournament.Games) {
                Console.WriteLine("{0}: {1} vs {2}: {3} ", game.Number, game.Player1.GetName(), game.Player2.GetName(), game.ResultText);
            }

            while (!validGameChosen) {
                Console.WriteLine("\nSelect a game to view (1-{0}):", _tournament.Games.Count);

                var parseSuccessful = int.TryParse(Console.ReadLine(), out choice);

                validGameChosen = 
                    parseSuccessful &&
                    choice >= 1 &&
                    choice <= _tournament.Games.Count;
                
                if(!validGameChosen) {
                    Console.WriteLine("\nThat is not a valid option");
                }
            }

            return choice;
        }

        internal void DisplayTournamentResults() {
            var sortedTournamentResults = _tournament.RankPlayers();
            Console.Clear();
            Console.WriteLine("\nTournament Results");
            Console.WriteLine("──────────────────");
            Console.WriteLine(sortedTournamentResults.AsTable());

            DisplayHelper.AnyKeyToContinue("\n[Press any key to continue..]");
        }

        internal void DisplayGamesMenu() {
            
            while (true) {
                Console.Clear();
                Console.WriteLine("\nGames Menu");
                Console.WriteLine("──────────\n");

                Console.WriteLine("1. Show Tournament results");
                Console.WriteLine("2. Show Game results");
                Console.WriteLine("3. Playback all games (random order)");
                Console.WriteLine("4. Playback individial game");
                Console.WriteLine("5. Exit BoardBots");

                var choice = Console.ReadKey().KeyChar;

                switch (choice) {
                    case '1':
                        DisplayTournamentResults();
                        break;
                    case '2':
                        ShowGameResults();
                        break;
                    case '3':
                        ShowGamesInRandomOrder();
                        break;
                    case '4':
                        ChooseAndShowGame();
                        break;
                    case '5':
                        Exit();
                        return;
                    default:
                        Console.WriteLine("\nPlease chose an option 1-5");
                        break;
                }
            }
        }

        private static void Exit() {
            DisplayHelper.ShowGoodbyeMessage();
            Thread.Sleep(2000);
            Console.Clear();
        }

        private void ShowGameResults() {
            Console.Clear();
            Console.WriteLine("\nGames");
            Console.WriteLine("─────\n");
            foreach (var game in _tournament.Games) {
                Console.Write("Game {0}: {1} vs {2}.. ", game.Number, game.Player1.GetName(), game.Player2.GetName());
                DisplayHelper.SetTextColour(DisplayHelper.ColourFor(game.Result));
                Console.WriteLine(game.ResultText);
                DisplayHelper.ResetTextColour();
            }

            DisplayHelper.AnyKeyToContinue("\n[Press any key to continue..]");
        }

        private void ChooseAndShowGame() {
            int selectedGame = ChooseGame();

            GamePlayback.Show(_tournament.Games.Where(x => x.Number == selectedGame).First());
        }

        private void ShowGamesInRandomOrder() {
            foreach (var game in _tournament.Games.OrderBy(x => Guid.NewGuid())) {
                GamePlayback.Show(game);
            }
        }
    }
}
