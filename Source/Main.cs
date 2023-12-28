using Verse;

namespace ShelfQualityMatters
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new HarmonyLib.Harmony($"MadaraUchiha.{nameof(ShelfQualityMatters)}");
            harmony.PatchAll();
        }
    }
}