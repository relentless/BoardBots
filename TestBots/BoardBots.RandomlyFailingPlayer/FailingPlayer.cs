using System;
using BoardBots.Shared;

namespace BoardBots.RandomlyFailingPlayer {
    public class FailingPlayer : IPlayer {
        private Random randomGenerator = new Random(DateTime.Now.Millisecond);
        public BoardPosition TakeTurn(IPlayerBoard board) {

            switch (randomGenerator.Next(3)) {
                case 0: return new BoardPosition(randomGenerator.Next(int.MinValue, int.MaxValue), randomGenerator.Next(int.MinValue, int.MaxValue));
                case 1: while (true) { };
                case 2: throw new Exception("Random error!");
            }

            return new BoardPosition(0, 0);
        }
    }
}
