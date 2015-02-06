using BoardBots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBots {
    public static class PlayerExtensions {
        public static string GetName(this IPlayer player) {
            return player.GetType().Name;
        }
    }
}
