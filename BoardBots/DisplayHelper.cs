using System;
using System.Collections.Generic;

namespace BoardBots {
    internal static class DisplayHelper {
        internal static void ShowWelcomeMessage() {
            Console.Clear();
            SetTextColour(ConsoleColor.Cyan);
            Console.WriteLine(@"
          ______                     _  ______       _       
          | ___ \                   | | | ___ \     | |      
          | |_/ / ___   __ _ _ __ __| | | |_/ / ___ | |_ ___ 
          | ___ \/ _ \ / _` | '__/ _` | | ___ \/ _ \| __/ __|
          | |_/ / (_) | (_| | | | (_| | | |_/ / (_) | |_\__ \
          \____/ \___/ \__,_|_|  \__,_| \____/ \___/ \__|___/");

            ResetTextColour();
            Console.WriteLine(@"


                             Version 1.2

                   by Grant Crofton & Scott Sellers");
            Console.WriteLine();
            ResetTextColour();

        }

        internal static void ShowGoodbyeMessage() {
            Console.Clear();
            SetTextColour(ConsoleColor.Cyan);
            Console.WriteLine(@"
              _____                 _ _                _ 
             |  __ \               | | |              | |
             | |  \/ ___   ___   __| | |__  _   _  ___| |
             | | __ / _ \ / _ \ / _` | '_ \| | | |/ _ \ |
             | |_\ \ (_) | (_) | (_| | |_) | |_| |  __/_|
              \____/\___/ \___/ \__,_|_.__/ \__, |\___(_)
                                             __/ |       
                                            |___/        ");
            Console.WriteLine();
            ResetTextColour();
        }

        internal static void AnyKeyToContinue(string message) {
            SetTextColour(ConsoleColor.Gray);
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
            ResetTextColour();
        }

        internal static void ShowAnyKeyToSkipGameMessage() {
            SetTextColour(ConsoleColor.DarkGray);
            Console.WriteLine("\n\n[any key to skip..]");
            ResetTextColour();
        }

        internal static void SetTextColour(ConsoleColor colour) {
            Console.ForegroundColor = colour;
        }

        internal static void ResetTextColour() {
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static ConsoleColor ColourFor(GameStatus status) {
            var statusColours = new Dictionary<GameStatus, ConsoleColor>{
                {GameStatus.Player1Wins, ConsoleColor.Green},
                {GameStatus.Player2Wins, ConsoleColor.Green},
                {GameStatus.Draw, ConsoleColor.Yellow},
                {GameStatus.Ongoing, ConsoleColor.Red},
            };

            return statusColours[status];
        }

        internal static void ShowNotEnoughPlayersWarning() {
            SetTextColour(ConsoleColor.Red);
            Console.WriteLine("** Error: You must have at least 2 players!  *\n\n");
            ResetTextColour();
        }
    }
}
