using BepInEx;

namespace QuestZoneAPI
{
    [BepInPlugin(Helpers.Constants.pluginGuid, Helpers.Constants.pluginName, Helpers.Constants.pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Starting");

            new Patches.GameWorldPatch().Enable();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {Helpers.Constants.pluginName} is loaded!");
        }
    }
}
