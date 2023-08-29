using Life;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterEssentials.Enums
{
    public static class HexColors
    {
        public const string BLANC = "#ffffff";
        public const string GRIS = "#555555";
        public const string GRIS_CLAIR = "#777777";
        public const string GRIS_FONCE = "#333333";
        public const string NOIR = "#000000";
        public const string ROUGE = "#ff0000";
        public const string ROUGE_FONCE = "#7b0000";
        public const string JAUNE = "#ffff00";
        public const string JAUNE_FONCE = "#ffff00";
        public const string VERT = "#00ff00";
        public const string VERT_FONCE = "#007b00";
        public const string BLEU = "#0000ff";
        public const string BLEU_FONCE = "#00007b";
        public const string ROSE = "#ff00ff";
        public const string VIOLET = "#7b007b";

        public static List<string> Values()
        {
            List<string> list = new List<string>
            {
                BLANC,
                GRIS,
                GRIS_CLAIR,
                GRIS_FONCE,
                NOIR,
                ROUGE,
                ROUGE_FONCE,
                JAUNE,
                JAUNE_FONCE,
                VERT,
                VERT_FONCE,
                BLEU,
                BLEU_FONCE,
                ROSE,
                VIOLET
            };
            return list;
        }
    }
}