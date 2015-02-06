using System;
using BoardBots.Shared;

namespace BoardBots.RandomPlayer
{
    public class RandomPlayer : IPlayer {
        private Random randomGenerator = new Random(DateTime.Now.Millisecond);
        public BoardPosition TakeTurn(IPlayerBoard board) {
            return new BoardPosition(
                randomGenerator.Next(3),
                randomGenerator.Next(3)
                );
        }
    }
}
