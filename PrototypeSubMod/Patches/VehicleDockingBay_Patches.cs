using HarmonyLib;
using PrototypeSubMod.Docking;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(VehicleDockingBay))]
public class VehicleDockingBay_Patches
{
    //[HarmonyPatch(nameof(VehicleDockingBay)), HarmonyPrefix]
    private static void DockVehicle_Prefix(VehicleDockingBay __instance)
    {
        var manager = __instance.GetComponentInChildren<ProtoDockingManager>();
        if (!manager) return;

        manager.SetPlayersVehicle(Player.main.GetVehicle());
    }
}