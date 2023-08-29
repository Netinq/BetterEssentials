using Life;
using Life.AreaSystem;
using Life.DB;
using Life.Network;
using Life.PermissionSystem;
using Life.VehicleSystem;
using Mirror;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BetterEssentials.Utils
{
    public class VehicleUtils
    {
        public async static void PrintVehicleInformations(Player player, Vehicles vehicle)
        {
            string modelName = Nova.v.vehicleModels[vehicle.ModelId].vehicleName;
            BoardBuilder board = new BoardBuilder() { title = $"{modelName}" };
            board.AppendLine("ID", vehicle.Id.ToString());
            board.AppendLine("ID Modèle", vehicle.ModelId.ToString());

            if (vehicle.Permissions != null)
            {
                Permissions permissions = JsonUtility.FromJson<Permissions>(vehicle.Permissions);
                if (permissions != null)
                {
                    if (permissions.HasOwner()) await board.AppendOwner(permissions.owner);
                    if (permissions.coOwners.Count > 0) await board.AppendCoOwners(permissions.coOwners);
                }
                if (vehicle.BizId > 0) board.AppendBiz(vehicle.BizId);
            }

            board.PrintToPlayer(player);
        }

        public static async void GenerateVehicleByModelId(int modelId, Player player, Permissions permissions)
        {
            Life.DB.Vehicles vehicles = await LifeDB.CreateVehicle(modelId, JsonUtility.ToJson(permissions));
            LifeVehicle vehicle = Nova.v.GetVehicle(vehicles.Id);
            vehicle.Save();
            Nova.v.SpawnVehicle(vehicle, player.setup.transform.position, player.setup.transform.rotation);
        }

        public static void ColorVehicle(uint vehicleId, string hexColor)
        {
            Color color = Nova.HexToColor(hexColor);
            Vehicle component = NetworkServer.spawned[vehicleId].GetComponent<Vehicle>();
            component.Networkcolor = color;
            LifeVehicle vehicle = Nova.v.GetVehicle(component.vehicleDbId);
            if (vehicle != null)
            {
                vehicle.color = Nova.ColorToHex(color);
                vehicle.Save();
            }
        }
    }
}
