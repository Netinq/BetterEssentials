using BetterEssentials.Utils;
using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Utilities
{
    public class Me : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/me", new string[] { "/m" }, "Décrire une action", "/me <texte>", (player, args) =>
            {
                string message = "";
                if (args.Length > 0)
                {
                    for (int i = 0; i < args.Length; i++) message += $" {args[i]}";
                    server.SendLocalText($"<color={LifeServer.COLOR_ME}>{player.character.Firstname} {player.character.Lastname}{message}</color>", 10f, player.setup.transform.position);
                }
                else ChatUtils.SendError(player, "/me <texte>");
            });
        }
    }
}
