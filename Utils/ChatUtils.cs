using Life.Network;
using Life;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static BetterEssentials.Utils.ChatUtils;
using Life.AreaSystem;
using Life.DB;
using Life.PermissionSystem;
using Steamworks.Data;

namespace BetterEssentials.Utils
{
    public class ChatUtils
    {
        public static void SendError(Player player, string error)
        {
            player.SendText($"[BetterEssentials] <color={LifeServer.COLOR_RED}>{error}</color>");
        }

        public static void SendSuccess(Player player, string message)
        {
            player.SendText($"[BetterEssentials] <color={LifeServer.COLOR_GREEN}>{message}</color>");
        }
        public static void SendMessage(Player player, string message)
        {
            player.SendText($"[BetterEssentials] {message}");
        }

        public class BoardLine
        {
            public string name;
            public string content;
        }

    }

    public class BoardBuilder
    {
        public string title;
        public List<BoardLine> lines = new List<BoardLine>();

        public BoardBuilder AppendLine(string name, string content)
        {
            return AppendLine(new BoardLine()
            {
                name = name,
                content = content
            });
        }
        public BoardBuilder AppendLine(BoardLine boardLine)
        {
            lines.Add(boardLine);
            return this;
        }

        public async Task<BoardBuilder> AppendOwner(Entity owner)
        {
            Characters ownerCharacter = await LifeDB.FetchCharacter(owner.characterId);
            if (ownerCharacter != null)
            {
                string lastConnection = "Inconnu";
                long seconds = Nova.UnixTimeNow() - ownerCharacter.LastDisconnect;
                long minutes = seconds / 60;
                if (minutes > 60)
                {
                    long hours = minutes / 60;
                    if (hours > 48)
                    {
                        long days = hours / 24;
                        lastConnection = $"Il y a plus de {days} jours.";
                    }
                    else lastConnection = $"Il y a {hours}h.";
                }
                else lastConnection = $"Il y a {minutes} min.";

                AppendLine("Propriétaire", $"{ownerCharacter.Firstname} {ownerCharacter.Lastname} ({lastConnection})");
            }
            return this;
        }

        public async Task<BoardBuilder> AppendCoOwners(List<Entity> coOwners)
        {
            StringBuilder coOwnerLineContent = new StringBuilder();
            foreach (Entity entity in coOwners)
            {
                string characterName = await LifeDB.FetchCharacterName(entity.characterId);
                if (coOwnerLineContent.Length > 0) coOwnerLineContent.Append(", ");
                coOwnerLineContent.Append($"{characterName}");
            }
            AppendLine("Co-Propriétaire(s)", coOwnerLineContent.ToString());
            return this;
        }

        public BoardBuilder AppendBiz(int bizId)
        {
            Bizs biz = LifeDB.FetchBiz(bizId).Result;
            AppendLine("Entreprise", biz.BizName);
            return this;
        }

        public void PrintToPlayer(Player player)
        {
            player.SendText($"<color={LifeServer.COLOR_BLUE}>< === > {title} < === ></color>");
            foreach (BoardLine line in lines)
            {
                player.SendText($"<color={LifeServer.COLOR_BLUE}>{line.name} :</color> {line.content}");
            }
            player.SendText($"<color={ LifeServer.COLOR_BLUE}>< === > {title} < === ></color>");
        }
    }
}
