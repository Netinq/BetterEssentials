using BetterEssentials.Utils;
using Life;
using Life.DB;
using Life.Network;
using Life.PermissionSystem;
using Life.VehicleSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Informations
{
    public class Vehicle : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/vehicleinfo", new string[] { "/vi" }, "/vehicleinfo", "Informations du véhicule le plus proche", async (player, args) =>
            {
                if (player.IsAdmin)
                {
                    if (player.isInGame)
                    {
                        Life.VehicleSystem.Vehicle closestVehicle = player.GetClosestVehicle();
                        if (closestVehicle == null)
                        {
                            FakeVehicle fakeVehicle = player.GetClosestFakeVehicle();
                            if (fakeVehicle == null) { ChatUtils.SendError(player, "Il n'a a pas de véhicule à proximité !"); return; }
                            int vehicleDbId = closestVehicle.vehicleDbId;
                            Vehicles vehicle = await (from v in LifeDB.db.Table<Vehicles>() where v.Id == vehicleDbId select v).FirstOrDefaultAsync();
                            VehicleUtils.PrintVehicleInformations(player, vehicle);
                        }
                        else
                        {
                            int vehicleDbId = closestVehicle.vehicleDbId;
                            Vehicles vehicle = await (from v in LifeDB.db.Table<Vehicles>() where v.Id == vehicleDbId select v).FirstOrDefaultAsync();
                            VehicleUtils.PrintVehicleInformations(player, vehicle);
                        }
                    }
                }
            });
        }
    }
}
