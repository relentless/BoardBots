using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BoardBots.Shared;

namespace BoardBots.Tests {
    [TestFixture]
    public class TournamentExtensions_Tests {
        [Test]
        public void AsTable_SingleResult_ReturnedInTable() {
            // arrange
            var score = new List<KeyValuePair<IPlayer, TournamentStats>>() {
                new KeyValuePair<IPlayer, TournamentStats> (
                    new ScoringPlayer(), 
                    new TournamentStats{
                        GamesPlayed = 1,
                        GamesWon = 2,
                        GamesDrawn = 3,
                        GamesLost = 4,
                        TournamentPoints = 5
                    })
            };

            // act
            var output = score.AsTable();

            // assert
            Assert.That(output, Is.EqualTo(@"
┌────────────────────────────┬────────┬─────┬───────┬──────┬────────┐
│ Player                     │ Played │ Won │ Drawn │ Lost │ Points │
├────────────────────────────┼────────┼─────┼───────┼──────┼────────┤
│ ScoringPlayer              │      1 │   2 │     3 │    4 │      5 │
└────────────────────────────┴────────┴─────┴───────┴──────┴────────┘"));
        }

        [Test]
        public void AsTable_LongValues_StillFormattedCorrectly() {
            // arrange
            var score = new List<KeyValuePair<IPlayer, TournamentStats>>() {
                new KeyValuePair<IPlayer, TournamentStats> (
                    new VeryLongNamePlayer(), 
                    new TournamentStats{
                        GamesPlayed = 111,
                        GamesWon = 222,
                        GamesDrawn = 333,
                        GamesLost = 444,
                        TournamentPoints = 5555
                    })
            };

            // act
            var output = score.AsTable();

            // assert
            Assert.That(output, Is.EqualTo(@"
┌────────────────────────────┬────────┬─────┬───────┬──────┬────────┐
│ Player                     │ Played │ Won │ Drawn │ Lost │ Points │
├────────────────────────────┼────────┼─────┼───────┼──────┼────────┤
│ VeryLongNamePlayer         │    111 │ 222 │   333 │  444 │   5555 │
└────────────────────────────┴────────┴─────┴───────┴──────┴────────┘"));
        }

        [Test]
        public void AsTable_ValuesTooLong_Truncated() {
            // arrange
            var score = new List<KeyValuePair<IPlayer, TournamentStats>>() {
                new KeyValuePair<IPlayer, TournamentStats> (
                    new FarTooLongVeryLongNamePlayer(), 
                    new TournamentStats{
                        GamesPlayed = 1111,
                        GamesWon = 2222,
                        GamesDrawn = 3333,
                        GamesLost = 4444,
                        TournamentPoints = 55555
                    })
            };

            // act
            var output = score.AsTable();

            // assert
            Assert.That(output, Is.EqualTo(@"
┌────────────────────────────┬────────┬─────┬───────┬──────┬────────┐
│ Player                     │ Played │ Won │ Drawn │ Lost │ Points │
├────────────────────────────┼────────┼─────┼───────┼──────┼────────┤
│ FarTooLongVeryLongNamePlay │    111 │ 222 │   333 │  444 │   5555 │
└────────────────────────────┴────────┴─────┴───────┴──────┴────────┘"));
        }

        [Test]
        public void AsTable_MultipleResults_DisplayedCorrectly() {
            // arrange
            var score = new List<KeyValuePair<IPlayer, TournamentStats>>() {
                new KeyValuePair<IPlayer, TournamentStats> (
                    new VeryLongNamePlayer(), 
                    new TournamentStats{
                        GamesPlayed = 111,
                        GamesWon = 222,
                        GamesDrawn = 333,
                        GamesLost = 444,
                        TournamentPoints = 5555
                    }),
                new KeyValuePair<IPlayer, TournamentStats> (
                    new ScoringPlayer(), 
                    new TournamentStats{
                        GamesPlayed = 1,
                        GamesWon = 2,
                        GamesDrawn = 3,
                        GamesLost = 4,
                        TournamentPoints = 5
                    })
            };

            // act
            var output = score.AsTable();

            // assert
            Assert.That(output, Is.EqualTo(@"
┌────────────────────────────┬────────┬─────┬───────┬──────┬────────┐
│ Player                     │ Played │ Won │ Drawn │ Lost │ Points │
├────────────────────────────┼────────┼─────┼───────┼──────┼────────┤
│ VeryLongNamePlayer         │    111 │ 222 │   333 │  444 │   5555 │
├────────────────────────────┼────────┼─────┼───────┼──────┼────────┤
│ ScoringPlayer              │      1 │   2 │     3 │    4 │      5 │
└────────────────────────────┴────────┴─────┴───────┴──────┴────────┘"));
        }
    }

    internal class ScoringPlayer : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    internal class VeryLongNamePlayer : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }

    internal class FarTooLongVeryLongNamePlayer : IPlayer {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            throw new NotImplementedException();
        }
    }
}
