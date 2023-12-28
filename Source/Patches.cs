using HarmonyLib;
using RimWorld;
using Verse;

namespace ShelfQualityMatters
{
    [HarmonyPatch(typeof(Building_Storage), nameof(Building_Storage.SpawnSetup))]
    public class Patches
    {
        public static void Prefix(Building_Storage __instance) {
            Log.Message("Gonna try get quality");
            if (!__instance.TryGetQuality(out var qc))
            {
                return;
            }
            Log.Message($"The quality I found is {qc}");
            __instance.def.building.maxItemsInCell = QualityToStorage(qc);
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