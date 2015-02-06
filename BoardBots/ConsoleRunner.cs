using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BoardBots {
    class ConsoleRunner {

        private const int MIN_PLAYERS = 2;

        static void Main(string[] args) {

            DisplayHelper.ShowWelcomeMessage();
            Thread.Sleep(2000);
            Console.Clear();

            var playerTypes = TypeLoader.LoadTypesFromAssembliesIn(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

            var loader = new PlayerLoader();
            var players = loader.LoadPlayersFrom(playerTypes);

            DisplayPlayerLoadingErrors(loader);

            if (players.Count < MIN_PLAYERS) {
                DisplayHelper.ShowNotEnoughPlayersWarning();
                return;
            }

            DisplayPlayers(players);
            DisplayHelper.AnyKeyToContinue("\n[Press any key to continue..]");

            var tournament = new Tournament(players);
            
            tournament.ScheduleGames();
            PlayGames(tournament);

            DisplayHelper.AnyKeyToContinue("\n[Press any key to see the results..]");

            tournament.AllocateTournamentPoints();

            var menu = new Menu(tournament);
            menu.DisplayTournamentResults();
            menu.DisplayGamesMenu();
        }

        private static void DisplayPlayerLoadingErrors(PlayerLoader loader) {
            if (loader.ValidationErrors.Count > 0) {
                Console.WriteLine("Player Load Validation Errors");
                Console.WriteLine("─────────────────────────────");
            }

            DisplayHelper.SetTextColour(ConsoleColor.Red);
            foreach (var error in loader.ValidationErrors) {
                Console.WriteLine(error);
            }
            DisplayHelper.ResetTextColour();
        }

        private static void DisplayPlayers(List<Shared.IPlayer> players) {
            Console.WriteLine("\nPlayers Loaded");
            Console.WriteLine("──────────────");
            foreach (var player in players) {
                Console.WriteLine(player);
            }
        }

        private static void PlayGames(Tournament tournament) {
            // TODO: this should probably be in tournament
            Console.WriteLine("\nTournament in Progress");
            Console.WriteLine("──────────────────────\n");
            foreach (var game in tournament.Games) {
                Console.Write("Game {0}: {1} vs {2}.. ", game.Number, game.Player1.GetName(), game.Player2.GetName());
                PlayWithoutConsoleOutput(game);
                tournament.AllocateGamePoints(game);
                Thread.Sleep(1200);
                ShowResult(game);
            }
        }

        private static void ShowResult(Game game) {
            DisplayHelper.SetTextColour(DisplayHelper.ColourFor(game.Result));
            Console.WriteLine(game.ResultText);
            DisplayHelper.ResetTextColour();
        }

        private static void PlayWithoutConsoleOutput(Game game) {
            var outputWriter = Console.Out;
            Console.SetOut(new NullWriter());
            game.Play();
            Console.SetOut(outputWriter);
        }
    }

    internal class NullWriter : TextWriter {
        public override Encoding Encoding {
            get { return Encoding.ASCII; }
        }
    }
}
