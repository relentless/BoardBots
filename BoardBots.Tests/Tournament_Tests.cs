using System.Collections.Generic;
using BoardBots.Shared;
using NSubstitute;
using NUnit.Framework;

namespace BoardBots.Tests {

    [TestFixture]
    public class Tournament_Tests {
        [Test]
        public void ScheduleGames_TwoPlayers_CreatesTwoGames() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2
            };

            var tournament = new Tournament(players);

            // act
            tournament.ScheduleGames();

            // assert
            Assert.That(tournament.Games.Count, Is.EqualTo(2));
            Assert.That(tournament.Games[0].Player1, Is.EqualTo(player1));
            Assert.That(tournament.Games[0].Player2, Is.EqualTo(player2));

            Assert.That(tournament.Games[1].Player1, Is.EqualTo(player2));
            Assert.That(tournament.Games[1].Player2, Is.EqualTo(player1));
        }

        [Test]
        public void ScheduleGames_ThreePlayers_CreatesSixGames()
        {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();
            var player3 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2,
                player3
            };

            var tournament = new Tournament(players);

            // act
            tournament.ScheduleGames();

            // assert
            Assert.That(tournament.Games.Count, Is.EqualTo(6));

            Assert.That(tournament.Games[0].Player1, Is.EqualTo(player1));
            Assert.That(tournament.Games[0].Player2, Is.EqualTo(player2));

            Assert.That(tournament.Games[1].Player1, Is.EqualTo(player1));
            Assert.That(tournament.Games[1].Player2, Is.EqualTo(player3));

            Assert.That(tournament.Games[2].Player1, Is.EqualTo(player2));
            Assert.That(tournament.Games[2].Player2, Is.EqualTo(player3));

            Assert.That(tournament.Games[3].Player1, Is.EqualTo(player3));
            Assert.That(tournament.Games[3].Player2, Is.EqualTo(player2));

            Assert.That(tournament.Games[4].Player1, Is.EqualTo(player3));
            Assert.That(tournament.Games[4].Player2, Is.EqualTo(player1));

            Assert.That(tournament.Games[5].Player1, Is.EqualTo(player2));
            Assert.That(tournament.Games[5].Player2, Is.EqualTo(player1));
        }

        [Test]
        public void ScheduleGames_Called_AssignsNumbersToGames() {
            // arrange
            var players = new List<IPlayer> {
                Substitute.For<IPlayer>(),
                Substitute.For<IPlayer>()
            };

            var tournament = new Tournament(players);

            // act
            tournament.ScheduleGames();

            // assert
            Assert.That(tournament.Games[0].Number, Is.EqualTo(1));
            Assert.That(tournament.Games[1].Number, Is.EqualTo(2));
        }

        [Test]
        public void AllocateGamePoints_Player1Wins_StatsAssignedCorrectly() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var tournament = new Tournament(new List<IPlayer> {
                player1,
                player2
            });

            var game = new TestableGame(player1, player2);
            game.SetResult(GameStatus.Player1Wins);

            // act
            tournament.AllocateGamePoints(game);

            // assert
            var expectedTournStatsP1 = new TournamentStats {
                GamesPlayed = 1,
                GamesWon = 1,
                GamesDrawn = 0,
                GamesLost = 0,
                GamePoints = 3,
                TournamentPoints = 0
            };

            var expectedTournStatsP2 = new TournamentStats {
                GamesPlayed = 1,
                GamesWon = 0,
                GamesDrawn = 0,
                GamesLost = 1,
                GamePoints = 0,
                TournamentPoints = 0
            };

            Assert.That(tournament.TournamentTable[player1].ToString(), Is.EqualTo(expectedTournStatsP1.ToString()));
            Assert.That(tournament.TournamentTable[player2].ToString(), Is.EqualTo(expectedTournStatsP2.ToString()));
        }

        [Test]
        public void AllocateGamePoints_Player2Wins_StatsAssignedCorrectly() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2
            };

            var tournament = new Tournament(players);

            var game = new TestableGame(player1, player2);
            game.SetResult(GameStatus.Player2Wins);

            // act
            tournament.AllocateGamePoints(game);

            // assert
            var expectedTournStatsP1 = new TournamentStats {
                GamesPlayed = 1,
                GamesWon = 0,
                GamesDrawn = 0,
                GamesLost = 1,
                GamePoints = 0,
                TournamentPoints = 0
            };

            var expectedTournStatsP2 = new TournamentStats {
                GamesPlayed = 1,
                GamesWon = 1,
                GamesDrawn = 0,
                GamesLost = 0,
                GamePoints = 3,
                TournamentPoints = 0
            };

            Assert.That(tournament.TournamentTable[player1].ToString(), Is.EqualTo(expectedTournStatsP1.ToString()));
            Assert.That(tournament.TournamentTable[player2].ToString(), Is.EqualTo(expectedTournStatsP2.ToString()));
        }

        [Test]
        public void AllocateGamePoints_Draw_BothPlayersGet1Point() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2
            };

            var tournament = new Tournament(players);

            var game = new TestableGame(player1, player2);
            game.SetResult(GameStatus.Draw);

            // act
            tournament.AllocateGamePoints(game);

            // assert
            var expectedTournStatsP1 = new TournamentStats {
                GamesPlayed = 1,
                GamesWon = 0,
                GamesDrawn = 1,
                GamesLost = 0,
                GamePoints = 1,
                TournamentPoints = 0
            };

            var expectedTournStatsP2 = new TournamentStats {
                GamesPlayed = 1,
                GamesWon = 0,
                GamesDrawn = 1,
                GamesLost = 0,
                GamePoints = 1,
                TournamentPoints = 0
            };

            Assert.That(tournament.TournamentTable[player1].ToString(), Is.EqualTo(expectedTournStatsP1.ToString()));
            Assert.That(tournament.TournamentTable[player2].ToString(), Is.EqualTo(expectedTournStatsP2.ToString()));
        }

        [Test]
        public void AllocateTournamentPoints_EqualGamePoints_ShouldGet4PointsEach() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2
            };

            var tournament = new Tournament(players);
           
            tournament.TournamentTable[player1].GamePoints = 6;
            tournament.TournamentTable[player2].GamePoints = 6;

            // act
            tournament.AllocateTournamentPoints();

            // assert
            Assert.That(tournament.TournamentTable[player1].TournamentPoints, Is.EqualTo(4));
            Assert.That(tournament.TournamentTable[player2].TournamentPoints, Is.EqualTo(4));
        }

        [Test]
        public void AllocateTournamentPoints_TwoPlayers_Get4PointsFor1stPlace2PointsFor2nd() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2
            };

            var tournament = new Tournament(players);

            tournament.TournamentTable[player1].GamePoints = 3;
            tournament.TournamentTable[player2].GamePoints = 9;

            // act
            tournament.AllocateTournamentPoints();

            // assert
            Assert.That(tournament.TournamentTable[player1].TournamentPoints, Is.EqualTo(2));
            Assert.That(tournament.TournamentTable[player2].TournamentPoints, Is.EqualTo(4));
        }

        [Test]
        public void AllocateTournamentPoints_TwoPlayersWithEqualGamePoints_1stGet6PointsEach2ndGets2Points() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();
            var player3 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2,
                player3
            };

            var tournament = new Tournament(players);

            tournament.TournamentTable[player1].GamePoints = 8;
            tournament.TournamentTable[player2].GamePoints = 4;
            tournament.TournamentTable[player3].GamePoints = 8;

            // act
            tournament.AllocateTournamentPoints();

            // assert
            Assert.That(tournament.TournamentTable[player1].TournamentPoints, Is.EqualTo(6));
            Assert.That(tournament.TournamentTable[player2].TournamentPoints, Is.EqualTo(2));
            Assert.That(tournament.TournamentTable[player3].TournamentPoints, Is.EqualTo(6));
        }

        [Test]
        public void AllocateTournamentPoints_ThreePlayers_Get6PointsFor1st4PointsFor2nd2PointsFor3rd() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();
            var player3 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2,
                player3
            };

            var tournament = new Tournament(players);

            tournament.TournamentTable[player1].GamePoints = 5;
            tournament.TournamentTable[player2].GamePoints = 7;
            tournament.TournamentTable[player3].GamePoints = 8;

            // act
            tournament.AllocateTournamentPoints();

            // assert
            Assert.That(tournament.TournamentTable[player1].TournamentPoints, Is.EqualTo(2));
            Assert.That(tournament.TournamentTable[player2].TournamentPoints, Is.EqualTo(4));
            Assert.That(tournament.TournamentTable[player3].TournamentPoints, Is.EqualTo(6));
        }

        [Test]
        public void AllocateTournamentPoints_ElevenPlayers_Get22PointsFor1st() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();
            var player3 = Substitute.For<IPlayer>();
            var player4 = Substitute.For<IPlayer>();
            var player5 = Substitute.For<IPlayer>();
            var player6 = Substitute.For<IPlayer>();
            var player7 = Substitute.For<IPlayer>();
            var player8 = Substitute.For<IPlayer>();
            var player9 = Substitute.For<IPlayer>();
            var player10 = Substitute.For<IPlayer>();
            var player11 = Substitute.For<IPlayer>();


            var players = new List<IPlayer> {
                player1,
                player2,
                player3,
                player4,
                player5,
                player6,
                player7,
                player8,
                player9,
                player10,
                player11
            };

            var tournament = new Tournament(players);

            tournament.TournamentTable[player1].GamePoints = 3;
            tournament.TournamentTable[player2].GamePoints = 2;
            tournament.TournamentTable[player3].GamePoints = 1;
            tournament.TournamentTable[player4].GamePoints = 5;
            tournament.TournamentTable[player5].GamePoints = 4;
            tournament.TournamentTable[player6].GamePoints = 8;
            tournament.TournamentTable[player7].GamePoints = 5;
            tournament.TournamentTable[player8].GamePoints = 7;
            tournament.TournamentTable[player9].GamePoints = 14;
            tournament.TournamentTable[player10].GamePoints = 10;
            tournament.TournamentTable[player11].GamePoints = 15;

            // act
            tournament.AllocateTournamentPoints();

            // assert
            Assert.That(tournament.TournamentTable[player1].TournamentPoints, Is.EqualTo(6));
            Assert.That(tournament.TournamentTable[player2].TournamentPoints, Is.EqualTo(4));
            Assert.That(tournament.TournamentTable[player3].TournamentPoints, Is.EqualTo(2));
            Assert.That(tournament.TournamentTable[player4].TournamentPoints, Is.EqualTo(12));
            Assert.That(tournament.TournamentTable[player5].TournamentPoints, Is.EqualTo(8));
            Assert.That(tournament.TournamentTable[player6].TournamentPoints, Is.EqualTo(16));
            Assert.That(tournament.TournamentTable[player7].TournamentPoints, Is.EqualTo(12));
            Assert.That(tournament.TournamentTable[player8].TournamentPoints, Is.EqualTo(14));
            Assert.That(tournament.TournamentTable[player9].TournamentPoints, Is.EqualTo(20));
            Assert.That(tournament.TournamentTable[player10].TournamentPoints, Is.EqualTo(18));
            Assert.That(tournament.TournamentTable[player11].TournamentPoints, Is.EqualTo(22));
        }

        [Test]
        public void RankPlayers_FivePlayers_ListInDecendingOrderByGamePoints() {
            // arrange
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();
            var player3 = Substitute.For<IPlayer>();
            var player4 = Substitute.For<IPlayer>();
            var player5 = Substitute.For<IPlayer>();

            var players = new List<IPlayer> {
                player1,
                player2,
                player3,
                player4,
                player5
            };

            var tournament = new Tournament(players);

            tournament.TournamentTable[player1].GamePoints = 5;
            tournament.TournamentTable[player2].GamePoints = 6;
            tournament.TournamentTable[player3].GamePoints = 10;
            tournament.TournamentTable[player4].GamePoints = 10;
            tournament.TournamentTable[player5].GamePoints = 2;

            tournament.AllocateTournamentPoints();

            // act
            var resultList = tournament.RankPlayers();

            // assert
            Assert.That(resultList[0].Key, Is.EqualTo(player3));
            Assert.That(resultList[0].Value.TournamentPoints, Is.EqualTo(10));

            Assert.That(resultList[1].Key, Is.EqualTo(player4));
            Assert.That(resultList[1].Value.TournamentPoints, Is.EqualTo(10));

            Assert.That(resultList[2].Key, Is.EqualTo(player2));
            Assert.That(resultList[2].Value.TournamentPoints, Is.EqualTo(6));

            Assert.That(resultList[3].Key, Is.EqualTo(player1));
            Assert.That(resultList[3].Value.TournamentPoints, Is.EqualTo(4));

            Assert.That(resultList[4].Key, Is.EqualTo(player5));
            Assert.That(resultList[4].Value.TournamentPoints, Is.EqualTo(2));
        }
    }
}
