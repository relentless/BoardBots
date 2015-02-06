using System;
using BoardBots.Shared;

namespace BoardBots.ChattyBot
{
    public class Chatty: IPlayer
    {
        public BoardPosition TakeTurn(IPlayerBoard board) {
            Console.WriteLine("************** I'm a ChattyBot, hear me ROAR!  ************");
            return BoardPosition.At(2, 2);
        }
    }
}
