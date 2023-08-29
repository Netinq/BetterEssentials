using BetterEssentials.Utils;
using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Administration
{
    public class ServiceAdmin : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/serviceadmin", new string[] { "/sa" }, "Activation du service admin", "/serviceadmin", (player, args) =>
            {
                if (player.IsAdmin) 
                {
                    if (player.serviceAdmin) 
                    {
                        player.SetAdminService(false);
                        ChatUtils.SendError(player, "Service admin désactivé !");
                    } else
                    {
                        player.SetAdminService(true);
                        ChatUtils.SendSuccess(player, "Service admin activé !");
                    }
                    player.serviceAdmin = !player.serviceAdmin;
                }
            });
        }
    }
}
