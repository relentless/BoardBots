using BoardBots.Shared;
using System;

namespace BoardBots.PrivatePlayer
{
    public class Container {
        private class PrivatePlayer : IPlayer {
            public BoardPosition TakeTurn(IPlayerBoard board) {
                return BoardPosition.At(0, 0);
            }
        }
    }
}
