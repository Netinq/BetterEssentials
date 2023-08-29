using BetterEssentials.Utils;
using Life.Network;

namespace BetterEssentials.Commands.Informations
{
    public class Player : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/playerinfos", new string[] { "/pi", "/playerinfo" }, "Récupérer les informations du joueur le plus proche", "/playerinfos", (player, args) =>
            {
                if (player.IsAdmin)
                {
                    Life.Network.Player closest = player.GetClosestPlayer();
                    if (closest != null)
                    {
                        BoardBuilder board = new BoardBuilder() { title = $"{closest.character.Firstname} {closest.character.Lastname}" };
                        board.AppendLine("ID Personnage", player.character.Id.ToString())
                        .AppendLine("ID Entreprise", player.character.BizId.ToString())
                        .AppendLine("Vie", $"{player.character.Health}%")
                        .AppendLine("Faim", $"{player.character.Hunger}%");

                        board.PrintToPlayer(player);
                    }

                    ChatUtils.SendError(player, "Aucun joueur proche !");
                }
            });
        }
    }
}
