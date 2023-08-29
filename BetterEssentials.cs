using BetterEssentials.Broadcast;
using Life;
using Life.Network;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BetterEssentials
{
    public class BetterEssentials : Plugin
    {
        private LifeServer server;
        public Configuration configuration;

        public static string directoryPath;
        public static string configurationPath;

        private readonly CommandManager commandManager = new CommandManager();
        private readonly JoinLeave joinLeave = new JoinLeave();

        public BetterEssentials(IGameAPI game) : base(game) { }

        public override void OnPluginInit()
        {
            base.OnPluginInit();

            this.server = Nova.server;

            InitDirectory();
            commandManager.Init(this, server);
            joinLeave.Init(this, server);
        }

        private void InitDirectory()
        {
            directoryPath = $"{pluginsPath}/BetterEssentials";
            configurationPath = $"{directoryPath}/config.json";

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            if (!File.Exists(configurationPath))
            {
                Configuration configuration = new Configuration();
                configuration.Save();
            } else
            {
                configuration = Configuration.FromFile();
            }
        }
    }
}