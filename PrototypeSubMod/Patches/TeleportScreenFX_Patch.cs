using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeSubMod.Patches
{
    [HarmonyPatch(typeof(TeleportScreenFXController), nameof(TeleportScreenFXController.StartTeleport))]
    public static class TeleportScreenFX_StartTeleport_Override
    {
        public static bool Prefix(TeleportScreenFXController __instance)
        {
            __instance.isFadingIn = true;
            return false;
        }
    }
}
