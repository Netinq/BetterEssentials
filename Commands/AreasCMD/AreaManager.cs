using BetterEssentials.Utils;
using Life;
using Life.AreaSystem;
using Life.Network;
using Life.PermissionSystem;
using Life.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.AreasCmd
{
    class AreaManager : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/terrain", new string[] { "/t" }, "Gérer un terrain", "terrain", (player, args) =>
            {
                if (!player.IsAdmin || player.account.adminLevel < 5) return;

                LifeArea area = Nova.a.GetAreaById(player.setup.areaId);
                if (area == null)
                {
                    ChatUtils.SendError(player, "Vous n'êtes pas sur un terrain !");
                    return;
                }

                UIPanel panel = new UIPanel($"Terrain N°{area.areaId}", UIPanel.PanelType.Tab)
                   .AddButton("Fermer", (ui) => player.ClosePanel(ui))
                   .AddButton("Sélectionner", (ui) => ui.SelectTab())
                   .AddTabLine("Supprimer co-propriétaires", (ui) =>
                   {
                       area.permissions = new Permissions() { coOwners = new List<Entity>() };
                       area.Save();
                       ChatUtils.SendSuccess(player, "Les co-propriétaires du terrain ont été supprimés.");
                   })
                   .AddTabLine("Modifier propriétaire", (ui) =>
                   {
                       UIPanel panelOwner = new UIPanel("Définir le propriétaire", UIPanel.PanelType.Input)
                       .SetInputPlaceholder("ID Personnage")
                       .AddButton("Fermer", (ui2) => player.ClosePanel(ui2))
                       .AddButton("Valider", (ui2) =>
                       {
                            int ownerId = int.Parse(ui2.inputText);
                            area.permissions = new Permissions() { owner = new Entity() { characterId = ownerId }, coOwners = new List<Entity>() };
                            area.Save();
                            player.ClosePanel(ui2);
                           ChatUtils.SendSuccess(player, "Propriétaire modifié.");
                       });
                       player.ShowPanelUI(panelOwner);
                   })
                    .AddTabLine("Modifier prix location", (ui) =>
                    {
                        UIPanel pricePanel = new UIPanel("Modifier le prix de location", UIPanel.PanelType.Input)
                        .SetInputPlaceholder($"{area.rentPrice}€")
                        .AddButton("Fermer", (ui2) => player.ClosePanel(ui2))
                        .AddButton("Valider", (ui2) =>
                        {
                            int price = int.Parse(ui2.inputText);
                            area.rentPrice = price;
                            if (area.rentPrice > 0) area.isRentable = true;
                            else area.isRentable = false;
                            area.Save();
                            player.ClosePanel(ui2);
                            ChatUtils.SendSuccess(player, "Tarif de location modifié.");
                        });

                        player.ShowPanelUI(pricePanel);
                    })
                   .AddTabLine("Modifier prix", (ui) =>
                   {
                       UIPanel pricePanel = new UIPanel("Modifier le prix", UIPanel.PanelType.Input)
                       .SetInputPlaceholder($"{area.price}€")
                       .AddButton("Fermer", (ui2) => player.ClosePanel(ui2))
                       .AddButton("Valider", (ui2) =>
                       {
                            int price = int.Parse(ui2.inputText);
                            area.price = price;
                            area.Save();
                            player.ClosePanel(ui2);
                           ChatUtils.SendSuccess(player, "Tarif du terrain modifié.");
                       });
                       player.ShowPanelUI(pricePanel);
                   });
                player.ShowPanelUI(panel);
            });
        }
    }
}
