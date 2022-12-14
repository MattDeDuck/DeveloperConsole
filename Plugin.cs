using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PotionCraft.ManagersSystem.Debug;
using PotionCraft.ManagersSystem.Game;
using PotionCraft.SceneLoader;


namespace DeveloperConsole
{
    [BepInPlugin(PLUGIN_GUID, "PotionCraftDevConsole", "1.0.2.1")]
    [BepInProcess("Potion Craft.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "com.mattdeduck.potioncraftdevconsole";

        public static ManualLogSource Log { get; private set; }

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Log.LogInfo($"Plugin {PLUGIN_GUID}: Patch Succeeded!");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(GameManager), "Start")]
        public static void Start_Postfix()
        {
            ObjectsLoader.AddLast("SaveLoadManager.SaveNewGameState", () => EnableDevConsole());
        }

        public static void EnableDevConsole()
        {
            DebugManager.isDeveloperMode = true;
            Log.LogInfo("Dev Console is enabled");
        }
    }
}