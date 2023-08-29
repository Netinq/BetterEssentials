using Life;
using Life.Network;
using Life.VehicleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.VehiclesCmd
{
    public class Stow : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/stowvehicle", new string[] { "/stow" }, "Ranger le véhicule le plus proche", "/stowvehicle", (player, args) =>
            {
                if (player.IsAdmin && player.account.adminLevel >= 2)
                {
                    Vehicle vehicle = player.GetClosestVehicle(5f);

                    if (vehicle != null && vehicle.vehicleDbId > 0) Nova.v.StowVehicle(vehicle.vehicleDbId);
                    else
                    {
                        FakeVehicle fake = player.GetClosestFakeVehicle(5f);
                        if (fake != null)Nova.v.StowVehicle(fake.vehicleDbId);
                    }
                }
            });
        }
    }
}
