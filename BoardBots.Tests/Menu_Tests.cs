using BoardBots.Shared;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoardBots.Tests {
    
    [TestFixture]
    public class Menu_Tests {
        [SetUp]
        public void Setup() {
            StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
        }

        [Test]
        public void ChooseGame_ValidGameSelected_ReturnsGame() {
            // arrange
            var menu = new Menu(CreateTournament());
            int selectedGame;

            // act
            using (StringReader inputStream = new StringReader("2")) {
                Console.SetIn(inputStream);
                selectedGame = menu.ChooseGame();
            }

            // assert
            Assert.That(selectedGame, Is.EqualTo(2));
        }

        [Test]
        public void ChooseGame_InvalidGameSelected_AsksUserToSelectAgain() {
            // arrange
            var menu = new Menu(CreateTournament());
            string outputText = null;

            // act
            using (StringReader inputStream = new StringReader("3\n2")) {
                using (StringWriter outputStream = new StringWriter()) {
                    Console.SetIn(inputStream);
                    Console.SetOut(outputStream);

                    menu.ChooseGame();

                    outputText = outputStream.ToString();
                }
            }

            // assert
            Assert.That(outputText, Contains.Substring("That is not a valid option"));
        }

        [Test]
        public void ChooseGame_NonNumericEntered_AsksUserToSelectAgain() {
            // arrange
            var menu = new Menu(CreateTournament());
            string outputText = null;

            // act
            using (StringReader inputStream = new StringReader("XxXxxX\n2")) {
                using (StringWriter outputStream = new StringWriter()) {
                    Console.SetIn(inputStream);
                    Console.SetOut(outputStream);

                    menu.ChooseGame();

                    outputText = outputStream.ToString();
                }
            }

            // assert
            Assert.That(outputText, Contains.Substring("That is not a valid option"));
        }

        private static Tournament CreateTournament() {
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2
            };

            var tournament = new Tournament(players);
            tournament.ScheduleGames();

            return tournament;
        }
    }
}
