using BetterEssentials;
using BetterEssentials.Utils;
using Life;
using Life.AreaSystem;
using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands
{
    public class Terrain : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/terraininfo", new string[] { "/ti" }, "Information du terrain", "/terraininfo", (player, args) =>
            {
                if (player.IsAdmin)
                {
                    if (player.isInGame && player.setup.areaId > 0)
                    {
                        LifeArea area = Nova.a.GetAreaById(player.setup.areaId);

                        if (area != null) TerrainUtils.PrintAreaInformations(player, area);
                        else ChatUtils.SendError(player, "Vous n'êtes pas sur un terrain !");
                    }
                }
            });
        }
    }
}
