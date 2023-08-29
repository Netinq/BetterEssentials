using BetterEssentials.Commands.Informations;
using BetterEssentials.Enums;
using BetterEssentials.Utils;
using Life;
using Life.Network;
using Life.PermissionSystem;
using Life.UI;
using Life.VehicleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Commands.VehiclesCmd
{
    public class Menu : Command
    {
        public override SChatCommand CreateCommand()
        {
            return new SChatCommand("/vehicle", new string[] { "/v" }, "Menu des véhicules", "/vehicle", (player, args) =>
            {
                if (!player.IsAdmin || player.account.adminLevel < 2) return;

                    UIPanel vAdminPanel = new UIPanel("Gestion véhicule", UIPanel.PanelType.Tab)
                    .AddButton("Fermer", (ui) => player.ClosePanel(ui))
                    .AddButton("Sélectionner", (ui) => ui.SelectTab())
                    .AddTabLine("Définir id entreprise", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) {ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous");return;}

                        UIPanel idEntreprisePanel = new UIPanel("Définir id entreprise", UIPanel.PanelType.Input)
                        .AddButton("Fermer", (ui2) => player.ClosePanel(ui2))
                        .AddButton("Valider", (ui2) =>
                        {
                            int entId = int.Parse(ui2.inputText);

                            vehicle.bizId = entId;
                            LifeVehicle lifeVehicle = Nova.v.GetVehicle(vehicle.vehicleDbId);

                            lifeVehicle.bizId = entId;
                            lifeVehicle.Save();

                            player.ClosePanel(ui2);
                            ChatUtils.SendSuccess(player, "Entreprise modifié !");
                        });
                        player.ShowPanelUI(idEntreprisePanel);
                    })
                    .AddTabLine("Définir propriétaire", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }

                        UIPanel idProprietairePanel = new UIPanel("Définir id propriétaire", UIPanel.PanelType.Input)
                        .AddButton("Fermer", (ui2) =>player.ClosePanel(ui2))
                        .AddButton("Valider", (ui2) =>
                        {
                            int ownerId = int.Parse(ui2.inputText);

                            LifeVehicle lifeVehicle = Nova.v.GetVehicle(vehicle.vehicleDbId);

                            lifeVehicle.permissions.owner = new Entity() { characterId = ownerId };
                            lifeVehicle.Save();

                            player.ClosePanel(ui2);
                            ChatUtils.SendSuccess(player, "Propriétaire modifié !");
                        });
                        player.ShowPanelUI(idProprietairePanel);
                    })
                    .AddTabLine("Créer véhicule", (ui) =>
                    {
                        UIPanel vPanel = new UIPanel("Véhicule", UIPanel.PanelType.Tab)
                            .AddButton("Fermer", (ui1) => player.ClosePanel(ui1))
                            .AddButton("Sélectionner", (ui1) => ui1.SelectTab());
                            for (int i = 0; i < Nova.v.vehicleModels.Length; i++)
                            {
                                Life.VehicleSystem.Vehicle vehicle = Nova.v.vehicleModels[i];
                                if (!vehicle.isDeprecated)
                                {
                                    int vehId = i;
                                    vPanel.AddTabLine(vehicle.vehicleName, (ui1) =>
                                    {
                                        int vehicleId = vehId;
                                        Permissions permissions = new Permissions(){owner = new Entity(){characterId = player.character.Id}};
                                        VehicleUtils.GenerateVehicleByModelId(vehicleId, player, permissions);
                                    });
                                }

                            }
                        player.ShowPanelUI(vPanel);
                    })
                    .AddTabLine("Créer véhicule par id", (ui) =>
                    {
                        UIPanel createVehicle = new UIPanel("Créer véhicule", UIPanel.PanelType.Input)
                        .AddButton("Fermer", (ui2) => player.ClosePanel(ui2))
                        .AddButton("Valider", (ui2) =>
                        {
                            int vehicleId = int.Parse(ui2.inputText);

                            if (vehicleId >= Nova.v.vehiclesModelName.Length) return;

                            Permissions permissions = new Permissions() { owner = new Entity() { characterId = player.character.Id } };
                            VehicleUtils.GenerateVehicleByModelId(vehicleId, player, permissions);
                        });

                        player.ShowPanelUI(createVehicle);
                    })
                    .AddTabLine("Définir la couleur", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }

                        UIPanel vehColorPanel = new UIPanel("Définir la couleur du véhicule", UIPanel.PanelType.Tab)
                        .AddButton("Fermer", (ui1) =>
                        {
                            player.ClosePanel(ui1);
                        })
                        .AddButton("Valider", (ui1) =>
                        {
                            ui1.SelectTab();
                        });

                        foreach(string hexColor in HexColors.Values())
                        {
                            vehColorPanel.AddTabLine($"<color={hexColor}>{hexColor}</color>", (ui1) => VehicleUtils.ColorVehicle(vehicle.netId, hexColor));
                        }

                        player.ShowPanelUI(vehColorPanel);
                    })
                    .AddTabLine("Définir la couleur (hexadécimal)", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }

                        UIPanel hexColorPanel = new UIPanel("Modifier la couleur", UIPanel.PanelType.Input)
                            .AddButton("Annuler", (ui2) => player.ClosePanel(ui2))
                            .AddButton("Valider", (ui2) =>
                            {
                                VehicleUtils.ColorVehicle(vehicle.netId, ui2.inputText);
                            })
                            .SetInputPlaceholder("#ffffff");

                        player.ShowPanelUI(hexColorPanel);
                    })
                    .AddTabLine("Refuel", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }
                        else vehicle.fuel = 100f;
                    })
                    .AddTabLine("Réparer", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }
                        else {
                            vehicle.Repair();
                            player.ClosePanel(ui);
                            ChatUtils.SendSuccess(player, "Véhicule réparé !");
                        }
                    })
                    .AddTabLine("Ranger", (ui) =>
                    {
                        Life.VehicleSystem.Vehicle vehicle = player.GetClosestVehicle();
                        if (vehicle == null) { ChatUtils.SendError(player, "Il n'y a pas de véhicule proche de vous"); return; }
                        if (vehicle.vehicleDbId > 0)Nova.v.StowVehicle(vehicle.vehicleDbId);
                    });

                player.ShowPanelUI(vAdminPanel);
            });
        }
    }
}
