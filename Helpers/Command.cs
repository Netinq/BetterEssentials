using BetterEssentials.Commands;
using BetterEssentials.Commands.Administration;
using BetterEssentials.Commands.AreasCmd;
using BetterEssentials.Commands.Informations;
using BetterEssentials.Commands.ServerCmd;
using BetterEssentials.Commands.Utilities;
using BetterEssentials.Commands.VehiclesCmd;
using Life.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials
{
    public abstract class Command : Base
    {
        public override void Init(BetterEssentials instance, LifeServer server)
        {
            base.Init(instance, server);
            CreateCommand().Register();
        }

        public abstract SChatCommand CreateCommand();
    }

    public class CommandManager : Base
    {
        // Administration
        private readonly ServiceAdmin serviceAdmin = new ServiceAdmin();
        private readonly SetAdmin setAdmin = new SetAdmin();

        // AreaCMD
        private readonly AreaManager areaManager = new AreaManager();

        // Informations
        private readonly Bizs biz = new Bizs();
        private readonly Commands.Informations.Player player = new Commands.Informations.Player();
        private readonly Terrain terrain = new Terrain();
        private readonly Vehicle vehicle = new Vehicle();

        // ServerCMD
        private readonly Day day = new Day();
        private readonly Night night = new Night();

        // Utilities
        private readonly ClearInventory clearInventory = new ClearInventory();
        private readonly Help help = new Help();
        private readonly Me me = new Me();

        // VehiclesCMD
        private readonly Destroy destroy = new Destroy();
        private readonly Stow stow = new Stow();
        private readonly Menu menu = new Menu();

        public override void Init(BetterEssentials instance, LifeServer server)
        {
            base.Init(instance, server);
            terrain.Init(instance, server);
            vehicle.Init(instance, server);
            serviceAdmin.Init(instance, server);
            setAdmin.Init(instance, server);
            player.Init(instance, server);
            clearInventory.Init(instance, server);
            destroy.Init(instance, server);
            stow.Init(instance, server);
            menu.Init(instance, server);
            biz.Init(instance, server);
            day.Init(instance, server);
            night.Init(instance, server);
            areaManager.Init(instance, server);
            help.Init(instance, server);
            me.Init(instance, server);
        }
    }
}
