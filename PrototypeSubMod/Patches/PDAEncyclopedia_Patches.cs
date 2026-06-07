using System;
using System.Collections.Generic;
using HarmonyLib;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(PDAEncyclopedia))]
public class PDAEncyclopedia_Patches
{
    public static Dictionary<string, Action> EncyclopediaUnlockEvents = new();

    [HarmonyPatch(typeof(PDAEncyclopedia),
    nameof(PDAEncyclopedia.Add),
    new Type[] { typeof(string), typeof(PDAEncyclopedia.Entry), typeof(bool) })]
    private static void Add_Postfix(string key)
    {
        if (EncyclopediaUnlockEvents.TryGetValue(key, out var action))
        {
            action();
        }
    }
}