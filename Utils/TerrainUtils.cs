using Life;
using Life.AreaSystem;
using Life.DB;
using Life.Network;
using Life.PermissionSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Utils
{
    public class TerrainUtils
    {
        public static async void PrintAreaInformations(Player player, LifeArea area)
        {
            BoardBuilder board = new BoardBuilder()
            {
                title = $"Terrain N°{area.areaId}"
            };
            
            if (area.permissions != null)
            {
                if (area.permissions.HasOwner()) await board.AppendOwner(area.permissions.owner);
                if (area.permissions.coOwners.Count > 0) await board.AppendCoOwners(area.permissions.coOwners);
            }
            if (area.bizId > 0) board.AppendBiz(area.bizId);
            if (area.isRentable)
            {
                if (area.rentPrice > 0) board.AppendLine("Location", $"{area.rentPrice}€");
            }
            if (area.price > 0) board.AppendLine("Prix", $"{area.price}€");

            board.PrintToPlayer(player);
        }
    }
}
