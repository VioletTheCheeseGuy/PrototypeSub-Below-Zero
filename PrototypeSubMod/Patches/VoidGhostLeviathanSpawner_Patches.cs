using HarmonyLib;
using Story;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(VoidLeviathansSpawner))]
public class VoidGhostLeviathanSpawnerPatch
{
    [HarmonyPatch(nameof(VoidLeviathansSpawner.UpdateSpawn))]
    [HarmonyPrefix]
    private static bool UpdateSpawn_Prefix()
    {
        StoryGoalManager storyGoalManager = StoryGoalManager.main;
        return !storyGoalManager.IsGoalComplete("HullFacilityWormTerminalEncy");
    }
    
    [HarmonyPatch(nameof(VoidLeviathansSpawner.IsPlayerInVoid))]
    [HarmonyPrefix]
    private static bool IsPlayerInVoid_Prefix()
    {
        StoryGoalManager storyGoalManager = StoryGoalManager.main;
        return !storyGoalManager.IsGoalComplete("HullFacilityWormTerminalEncy");
    }
}