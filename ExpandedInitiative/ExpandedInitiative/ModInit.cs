using IRBTModUtils.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;

namespace ExpandedInitiative
{
    public class Mod
    {

        public const string HarmonyPackage = "us.frostraptor.ExpandedInitiative";
        public const string LogName = "expanded_initiative";

        public static DeferringLogger Log;
        public static string ModDir;
        public static ModConfig Config;

        public const int MaxPhase = 10;
        public const int MinPhase = 1;
        public static readonly Random Random = new Random();

        public static void Init(string modDirectory, string settingsJSON)
        {
            ModDir = modDirectory;

            Exception configE;
            try
            {
                Config = JsonConvert.DeserializeObject<ModConfig>(settingsJSON);
            }
            catch (Exception e)
            {
                configE = e;
                Config = new ModConfig();
            }
            finally
            {
                Config.InitializeColors();
            }

            Log = new DeferringLogger(modDirectory, Mod.LogName, Config.Debug, Config.Trace);

            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            Log.Info?.Write($"Assembly version: {fvi.ProductVersion}");

            Log.Debug?.Write($"ModDir is:{modDirectory}");
            Log.Debug?.Write($"mod.json settings are:({settingsJSON})");
            Mod.Config.LogConfig();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), HarmonyPackage);
        }

    }
}
