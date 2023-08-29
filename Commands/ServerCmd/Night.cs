using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.ServerCmd
{
    public class Day : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/day", "Définir le jour", "/day", (player, args) =>
            {
                if (!player.IsAdmin) return;
                EnviroSkyMgr.instance.SetTimeOfDay(11f);
            });
        }
    }
}
