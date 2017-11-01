using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace NoMoreTombs
{
    public static class TombsConfig
    {
        public const int ConfigVersion = 1;
        public static bool NoTombstones = true;
        public static bool NoDeathMessage = false;

        static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "NoMoreTombs.json");
        static Preferences Config = new Preferences(ConfigPath);

        public static void Load()
        {
           if (!ReadConfig())
            {
                ErrorLogger.Log("NoMoreTombs: Failed to read config file! Recreating config...");
                CreateConfig();
            }
        }

        static bool ReadConfig()
        {
            if (!Config.Load())
                return false;

            int fileVersion = 0;
            Config.Get("ConfigVersion", ref fileVersion);

            if (fileVersion != ConfigVersion)
                return false;

            Config.Get("NoTombstones", ref NoTombstones);
            Config.Get("NoDeathMessage", ref NoDeathMessage);
            return true;
        }

        static void CreateConfig()
        {
            Config.Clear();
            Config.Put("ConfigVersion", ConfigVersion);
            Config.Put("NoTombstones", NoTombstones);
            Config.Put("NoDeathMessage", NoDeathMessage);
            Config.Save();
        }
    }
}
