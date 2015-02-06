namespace BoardBots.Shared {
    public class BoardPosition {
        private int _column;
        private int _row;

        public BoardPosition(int column, int row) {
            _column = column;
            _row = row;
        }

        public static BoardPosition At(int column, int row) {
            return new BoardPosition(column, row);
        }

        public int Column {
            get {
                return _column;
            }
        }
        public int Row { 
            get{
                return _row;
            }
        }
    }
}
