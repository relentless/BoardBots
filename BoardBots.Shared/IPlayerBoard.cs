using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardBots.Shared {
    /// <summary>
    /// Represents the game board as seen by a particukar player
    /// </summary>
    public interface IPlayerBoard {
        PlayerToken TokenAt(BoardPosition position);
    }
}
