using BetterEssentials.Utils;
using Life.DB;
using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Informations
{
    public class Bizs : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/bizlist", new string[] { "/bl" }, "Liste des entreprises", "/bizlist", async (player, args) =>
            {
                if (player.IsAdmin)
                {
                    List<Life.DB.Bizs> bizs = await LifeDB.db.Table<Life.DB.Bizs>().ToListAsync();
                    BoardBuilder board = new BoardBuilder() { title = "Entreprises" };
                    foreach (Life.DB.Bizs biz in bizs) {
                        board.AppendLine(biz.Id.ToString(), biz.BizName);
                    }
                    board.PrintToPlayer(player);
                }
            });
        }
    }
}
