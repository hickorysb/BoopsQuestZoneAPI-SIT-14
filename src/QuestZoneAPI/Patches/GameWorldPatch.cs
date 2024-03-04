using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestZoneAPI.Patches
{
    public class GameWorldPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        public static List<ZoneClass> GetZones()
        {
            var zones = Helpers.WebRequestHelper.Get<List<ZoneClass>>("/quests/zones/getZones");
            Logger.LogInfo(zones.First().zoneName);
            return zones;
        }

        public static void CreatePlaceItemZone(ZoneClass zone)
        {
            GameObject questZone = new GameObject();

            BoxCollider collider = questZone.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            Vector3 position = new Vector3(float.Parse(zone.position.x), float.Parse(zone.position.y), float.Parse(zone.position.z));
            questZone.transform.position = position;
            EFT.Interactive.PlaceItemTrigger scriptComp = questZone.AddComponent<EFT.Interactive.PlaceItemTrigger>();
            scriptComp.SetId(zone.zoneId);

            questZone.layer = LayerMask.NameToLayer("Triggers");
            questZone.name = zone.zoneId;
        }

        public static void CreateVisitZone(ZoneClass zone)
        {
            GameObject questZone = new GameObject();

            BoxCollider collider = questZone.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            Vector3 position = new Vector3(float.Parse(zone.position.x), float.Parse(zone.position.y), float.Parse(zone.position.z));
            questZone.transform.position = position;
            EFT.Interactive.ExperienceTrigger scriptComp = questZone.AddComponent<EFT.Interactive.ExperienceTrigger>();
            scriptComp.SetId(zone.zoneId);

            questZone.layer = LayerMask.NameToLayer("Triggers");
            questZone.name = zone.zoneId;
        }

        public static void AddZones(List<ZoneClass> zones, string currentLocation)
        {
            foreach (ZoneClass zone in zones)
            {
                if (zone.zoneLocation.ToLower() == currentLocation.ToLower())
                {
                    switch(Enum.Parse(typeof(ZoneType), zone.zoneType))
                    {
                        case ZoneType.PlaceItem:
                            Logger.LogInfo(zone.position.x);
                            Logger.LogInfo(zone.rotation.y);
                            CreatePlaceItemZone(zone);

                            break;
                        case ZoneType.Visit:
                            Logger.LogInfo(zone.position.x);
                            Logger.LogInfo(zone.rotation.y);
                            CreateVisitZone(zone);
                            break;
                        default:
                            Logger.LogInfo(zone.position.x);
                            Logger.LogInfo(zone.rotation.y);
                            CreateVisitZone(zone);
                            break;
                    }
                } else
                {
                    Logger.LogInfo("Zone not in current location");
                }
            }
        }

        [PatchPostfix]
        static void PatchPostfix(GameWorld __instance)
        {
            Logger.LogInfo("GameWorld.OnGameStarted");
            try
            {
                string loc = __instance.MainPlayer.Location;
                Logger.LogInfo(loc);
                
                List<ZoneClass> zones = GetZones();
                AddZones(zones, loc);
            } catch (Exception ex)
            {
                Logger.LogInfo("GameWorld Error: " + ex.Message);
            }
        }
    }
}
