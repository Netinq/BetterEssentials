using BetterEssentials.Utils;
using Life;
using Life.Network;
using Life.VehicleSystem;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.VehiclesCmd
{
    public class Destroy : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/destroy", new string[] {}, "Détruire le véhicule le plus proche", "/destroy", (player, args) =>
            {
                if (player.IsAdmin)
                {
                    Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                    if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }
                    else if (vehicle.quickOwner > 0) Nova.v.DestroyVehicle(vehicle.vehicleDbId);
                }
            });
        }
    }
}
