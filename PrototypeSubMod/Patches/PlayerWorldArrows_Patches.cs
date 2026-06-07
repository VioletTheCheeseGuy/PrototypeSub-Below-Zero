using HarmonyLib;
using Nautilus.Extensions;
using PrototypeSubMod.LightDistortionField;
using PrototypeSubMod.Teleporter;
using Story;
using System;
using UnityEngine;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(PlayerWorldArrows))]
public class PlayerWorldArrows_Patches
{
    [HarmonyPatch(nameof(PlayerWorldArrows.CreateWorldArrows)), HarmonyPostfix]
    private static void CreateWorldArrows_Postfix(PlayerWorldArrows __instance)
    {
        CreateRadialWheelArrow(__instance);
        CreateInterceptorMapArrow(__instance);
    }

    private static void CreateRadialWheelArrow(PlayerWorldArrows instance)
    {
        var radialWheelTT = (TechType)Enum.Parse(typeof(TechType), "ProtoRadialWheel");

        var arrow = new PlayerWorldArrows.PlayerWorldArrow
        {
            inInventory = false,
            underwaterOnly = false,
            objectTechType = radialWheelTT,
            arrowText = "ProtoRadialHint",
            customGoal = "ProtoOpenRadialWheel",
            priority = 0f,
            arrowOffset = new Vector3(0f, -120f, 0f),
            offsetIsLocal = true,
            localScale = 150f,
            button = null
        };

        instance.data.arrows.Add(arrow);

        instance.worldArrows[instance.worldArrows.Count - 1].gameConditionDelegate = (ref Transform transform) =>
        {
            if (Player.main.GetMode() != Player.Mode.Piloting) return false;
            
            foreach (var effectHandler in CloakEffectHandler.EffectHandlers)
            {
                var subRoot = effectHandler.GetComponentInParent<SubRoot>();
                if (subRoot && subRoot.GetComponent<CyclopsMotorMode>().engineOn)
                {
                    transform = subRoot.transform.Find("PrototypeHUD/MiddleStatus/RadialHintTarget");
                    return true;
                }
            }

            return false;
        };
    }

    private static void CreateInterceptorMapArrow(PlayerWorldArrows instance)
    {
        var interceptorMapTT = (TechType)Enum.Parse(typeof(TechType), "ProtoInterceptorMap");

        var arrow = new PlayerWorldArrows.PlayerWorldArrow
        {
            inInventory = false,
            underwaterOnly = false,
            objectTechType = interceptorMapTT,
            arrowText = "ProtoInterceptorMapHint",
            customGoal = "ProtoOpenInterceptorMap",
            priority = 0f,
            arrowOffset = new Vector3(0f, -0.2f, 0f),
            offsetIsLocal = true,
            localScale = 0.75f,
            button = null
        };

        instance.data.arrows.Add(arrow);

        instance.worldArrows[instance.worldArrows.Count - 1].gameConditionDelegate = (ref Transform transform) =>
        {
            if (Player.main.currentSub == null) return false;
            
            foreach (var effectHandler in CloakEffectHandler.EffectHandlers)
            {
                var subRoot = effectHandler.GetComponentInParent<SubRoot>();
                if (Player.main.currentSub != subRoot) continue;

                var teleporterManager = subRoot.GetComponentInChildren<ProtoTeleporterManager>();
                if (!teleporterManager.GetUpgradeInstalled()) continue;

                transform = teleporterManager.transform.Find("MapOpenHint");

                if (!StoryGoalManager.main.IsGoalComplete(("ArchwayOverrideHint")))
                {
                    StoryGoalManager.main.OnGoalComplete(("ArchwayOverrideHint"));
                }
                return true;
            }

            return false;
        };
    }
}