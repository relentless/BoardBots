using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBots.Shared
{
    public interface IPlayer
    {
        BoardPosition TakeTurn(IPlayerBoard board);
    }
}
