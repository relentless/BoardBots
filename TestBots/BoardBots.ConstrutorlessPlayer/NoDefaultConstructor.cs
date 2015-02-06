using BoardBots.Shared;

namespace BoardBots.ConstrutorlessPlayer
{
    public class NoDefaultConstructor: IPlayer
    {
        public NoDefaultConstructor(int someVal) {
        }

        public BoardPosition TakeTurn(IPlayerBoard board) {
            return BoardPosition.At(0, 0);
        }
    }
}
