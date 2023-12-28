using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ShelfQualityMatters
{
    [HarmonyPatch(typeof(Building), "get_MaxItemsInCell")]
    public class Patches
    {
        private static readonly Dictionary<int, QualityCategory> thingIdToQualityCache = new Dictionary<int, QualityCategory>();
        public static bool Prefix(Building __instance, ref int __result)
        {
            // Don't care about nonstorage buildings
            if (!(__instance is Building_Storage))
            {
                return true;
            }

            // cached?
            if (thingIdToQualityCache.TryGetValue(__instance.thingIDNumber, out var value))
            {
                __result = QualityToStorage(value);
                return false;
            }

            // not cached, try the expensive call
            if (!__instance.TryGetQuality(out var qc))
            {
                // item has no quality for some reason, resume original function call
                return true;
            }

            __result = QualityToStorage(qc);
            thingIdToQualityCache[__instance.thingIDNumber] = qc;
            return false;
        }
        private static int QualityToStorage(QualityCategory quality)
        {
            if (quality == QualityCategory.Legendary)
            {
                return 10;
            }

            return (int)quality + 1;
        }
    }
}