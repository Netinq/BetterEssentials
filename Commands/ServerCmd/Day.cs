using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.ServerCmd
{
    public class Night : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/night", "Définir le jour", "/night", (player, args) =>
            {
                if (!player.IsAdmin) return;
                EnviroSkyMgr.instance.SetTimeOfDay(20f);
            });
        }
    }
}
