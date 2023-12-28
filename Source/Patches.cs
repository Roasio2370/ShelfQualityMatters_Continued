using HarmonyLib;
using RimWorld;
using Verse;

namespace ShelfQualityMatters
{
    [HarmonyPatch(typeof(Building), "get_MaxItemsInCell")]
    public class Patches
    {
        public static bool Prefix(Building __instance, ref int __result)
        {
            if (!(__instance is Building_Storage)) {
                return true;
            }
            if (!__instance.TryGetQuality(out var qc))
            {
                return true;
            }
            __result = QualityToStorage(qc);
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