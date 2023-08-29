using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BetterEssentials
{
    [System.Serializable]
    public class Configuration
    {
        public bool joinMessage = true;
        public bool leaveMessage = true;
     
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(BetterEssentials.configurationPath, json);
        }

        public string toJSON()
        {
            return JsonUtility.ToJson(this);
        }

        public static Configuration FromFile()
        {
            string json = File.ReadAllText(BetterEssentials.configurationPath);
            try
            {
                Configuration config = JsonUtility.FromJson<Configuration>(json);
                return config;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
    }
}
