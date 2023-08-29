using Life.Network;
using Life.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.Administration
{
    public class SetAdmin : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/setadmin", new string[] {"/setadm"}, "Défini le joueur le plus proche en admin", "/setadmin", (player, args) =>
            {
                if (player.IsAdmin && player.account.adminLevel >= 8)
                {
                    Player closest = player.GetClosestPlayer();
                    if (closest != null)
                    {
                        UIPanel panel = new UIPanel($"Définir {closest.character.Firstname} {closest.character.Lastname} administrateur", UIPanel.PanelType.Input)
                        .SetInputPlaceholder("Rang admin (1 - 10)")
                        .AddButton("Fermer", (ui) =>
                        {
                            player.ClosePanel(ui);
                        })
                        .AddButton("Valider", (ui) =>
                        {
                            closest.account.adminLevel = int.Parse(ui.inputText);
                            closest.Save();
                            player.ClosePanel(ui);
                        });

                        player.ShowPanelUI(panel);

                    }
                }
            });
        }
    }
}
