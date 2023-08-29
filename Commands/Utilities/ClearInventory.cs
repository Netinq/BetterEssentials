using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Utilities
{
    public class ClearInventory : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/clearinventory", new string[] { "/clearinv" }, "Vider son inventaire", "/clearinv", (player, args) =>
            {
                if (player.IsAdmin) player.setup.inventory.Clear();
            });
        }
    }
}
