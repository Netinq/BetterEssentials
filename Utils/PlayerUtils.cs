using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Utils
{
    public class PlayerUtils
    {
        public static string ToName(Player player)
        {
            return $"{player.character.Firstname} {player.character.Lastname}";
        }
    }
}
