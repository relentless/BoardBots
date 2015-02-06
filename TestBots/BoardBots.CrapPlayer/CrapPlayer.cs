using BoardBots.Shared;

namespace BoardBots.CrapPlayer
{
    public class CrapPlayer: IPlayer
    {
        public CrapPlayer() {
        }

        public BoardPosition TakeTurn(IPlayerBoard board) {
            return new BoardPosition(0, 0);
        }
    }
}
