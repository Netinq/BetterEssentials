using BetterEssentials.Utils;
using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Utilities
{
    public class Help : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/help", new string[] { "/h" }, "Liste des commandes", "/help", (player, args) =>
            {
                if (!player.IsAdmin) return;
                int pageIndex = 0;
                const int limitByPage = 10;

                if (args.Length > 0) if (int.TryParse(args[0], out int n)) pageIndex = n - 1;

                List<SChatCommand> commands = server.chat.commands;
                BoardBuilder board = new BoardBuilder() { title = "Liste des commandes" };

                for (int i = limitByPage * pageIndex; i < (limitByPage * pageIndex) + limitByPage; i++)
                {
                    if (commands.Count <= i) break;

                    SChatCommand command = commands[i];
                    board.AppendLine(command.usage, command.description);
                }
                board.AppendLine("Page", $"({pageIndex + 1}/{Math.Ceiling(commands.Count / Convert.ToDecimal(limitByPage))})");
                board.PrintToPlayer(player);
            });
        }
    }
}
