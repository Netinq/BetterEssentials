using BetterEssentials.Utils;
using Life;
using Life.Network;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Broadcast
{
    public class JoinLeave : Base
    {
        public override void Init(BetterEssentials instance, LifeServer server)
        {
            if (instance.configuration.joinMessage || instance.configuration.leaveMessage)
            {
                base.Init(instance, server);
            }
        }

        public override void OnPlayerSpawnCharacter(Player player)
        {
            if (instance.configuration.joinMessage)
            {
                base.OnPlayerSpawnCharacter(player);
                server.SendMessageToAll($"{PlayerUtils.ToName(player)} à rejoint la ville !");
            }
        }

        public override void OnPlayerDisconnect(NetworkConnection conn)
        {
            if (instance.configuration.leaveMessage)
            { 
                base.OnPlayerDisconnect(conn);
                Player player = server.GetPlayer(conn);
                server.SendMessageToAll($"{PlayerUtils.ToName(player)} à quitté la ville !");
            }
        }
    }
}
