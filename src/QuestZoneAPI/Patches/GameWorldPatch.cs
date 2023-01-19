using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace QuestZoneAPI.Patches
{
    public class GameWorldPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        static void PatchPostfix(GameWorld __instance)
        {
            Logger.LogInfo("GameWorld.OnGameStarted");
            try
            {
                GameObject customQuestZone = new GameObject();

                BoxCollider collider = customQuestZone.AddComponent<BoxCollider>();
                collider.isTrigger = true;

                customQuestZone.transform.position = new Vector3(106.7446f, -6.8813f, -109.6872f);

                EFT.Interactive.PlaceItemTrigger scriptComp = customQuestZone.AddComponent<EFT.Interactive.PlaceItemTrigger>();
                scriptComp.SetId("demo");
                
                customQuestZone.layer = LayerMask.NameToLayer("Triggers");
                customQuestZone.name = "demoZone";
            } catch (Exception ex)
            {
                Logger.LogInfo("GameWorld Error: " + ex.Message);
            }
        }
    }
}
