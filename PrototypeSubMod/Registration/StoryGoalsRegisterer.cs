using System;
using Nautilus.Handlers;
using Nautilus.Utility;
using PrototypeSubMod.Facilities.Hull;
using PrototypeSubMod.Prefabs;
using PrototypeSubMod.Prefabs.AlienBuildingBlock;
using PrototypeSubMod.Prefabs.FacilityProps;
using PrototypeSubMod.Prefabs.Factors;
using PrototypeSubMod.PrototypeStory;
using Story;
using UnityEngine;

namespace PrototypeSubMod.Registration;

internal static class StoryGoalsRegisterer
{
    public static void Register()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        
        #region Precursor Ingot Pickup Unlock

        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("Ency_ProtoPrecursorIngot", Story.GoalType.Encyclopedia, PrecursorIngot_Craftable.prefabInfo.TechType);
        
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_ProtoPrecursorIngot", () =>
        {
            KnownTech.Add(PrecursorIngot_Craftable.prefabInfo.TechType,false);
            PDAEncyclopedia.Add("ProtoPrecursorIngot", true, false);
        });
        #endregion

        #region Photon Beacon Pickup Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("DeployableLightPickup", Story.GoalType.Encyclopedia, DeployableLight_Craftable.prefabInfo.TechType);

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("DeployableLightPickup", () =>
        {
            KnownTech.Add(DeployableLight_Craftable.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("ProtoDeployableLightEncy", true, false);
        });
        #endregion

        #region Phase Gate Items Pickup
        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("Ency_ProtoPhaseGateStructure", Story.GoalType.Encyclopedia, ProtoPhaseGateStructure.PrefabInfo.TechType);

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_ProtoPhaseGateStructure", () =>
        {
            KnownTech.Add(ProtoPhaseGateStructure.PrefabInfo.TechType, false);
            PDAEncyclopedia.Add("ProtoPhaseGateStructure", true, false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("Ency_ProtoPhaseGateStabilizer", Story.GoalType.Encyclopedia, ProtoPhaseGateStabilizer.PrefabInfo.TechType);

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_ProtoPhaseGateStabilizer", () =>
        {
            KnownTech.Add(ProtoPhaseGateStabilizer.PrefabInfo.TechType, false);
            PDAEncyclopedia.Add("ProtoPhaseGateStabilizer", true, false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("Ency_ProtoPhaseGateTransmitter", Story.GoalType.Encyclopedia, ProtoPhaseGateTransmitter.PrefabInfo.TechType);

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_ProtoPhaseGateTransmitter", () =>
        {
            KnownTech.Add(ProtoPhaseGateTransmitter.PrefabInfo.TechType, false);
            PDAEncyclopedia.Add("ProtoPhaseGateTransmitter", true, false);
        });
        #endregion

        #region PPT First Interaction
        PPTStoryManager.RegisterGoals();
        #endregion

        #region Interceptor Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnInterceptorTestDataDownloaded", () =>
        {
            PDALog.Add("OnInterceptorTestDataDownloaded", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("InterceptorTestEncy", Story.GoalType.Encyclopedia, 15f, new[] { "OnInterceptorTestDataDownloaded" });
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("InterceptorTestEncy", () =>
        {
            PDAEncyclopedia.Add("InterceptorTestEncy", true, false);
        });
        #endregion

        #region Disable Defense Cloak
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnDefenseCloakDisabled", () =>
        {
            PDALog.Add("OnDefenseCloakDisabled",false);
            FMODUWE.PlayOneShot(AudioUtils.GetFmodAsset("EngineAllBreachesRepaired"), Player.main.transform.position);
        });
        #endregion

        #region Moonpool Enter
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterDefenseMoonpool", () =>
        {
            PDALog.Add("OnEnterDefenseMoonpool",false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterLocationGoal("OnEnterDefenseMoonpool", Story.GoalType.PDA, new Vector3(819, -463, -1115), 15, 0);
        #endregion

        #region Moonpool Open Disallowed
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnMoonpoolNoPrototype", () =>
        {
            PDALog.Add("OnMoonpoolNoPrototype", false);
        });
        #endregion

        #region On Approach Defense Beacon
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnApproachDefenseFacility", () =>
        {
            PDALog.Add("OnApproachDefenseFacility", false);
        });
        #endregion

        #region Orion Logs
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_OrionFacilityLogs", () =>
        {
            PDAEncyclopedia.Add("OrionFacilityLogsEncy", true, false);
        });
        #endregion

        #region Facility Locations
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_ProtoFacilitiesEncy", () =>
        {
            PDAEncyclopedia.Add("ProtoFacilitiesEncy", true, false);
        });
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ProtoFacilityLocationsHint", () =>
        {
            PDALog.Add("ProtoFacilityLocationsHint", true);
        });
        #endregion

        #region Defense Audit Logs
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("DefenseFacilityAuditEncy", Story.GoalType.Encyclopedia, 7f, "OnDisableDefenseCloak");

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("DefenseFacilityAuditEncy", () =>
        {
            PDAEncyclopedia.Add("DefenseFacilityAuditEncy", true, false);
            
            KnownTech.Add(DefenseFacilityKey.prefabInfo.TechType, false);

            PDAEncyclopedia.Add("DefenseFacilityKey", true, false);
        });
        #endregion

        #region Engine Audit Logs
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("EngineFacilityAuditEncy", () =>
        {
            PDAEncyclopedia.Add("EngineFacilityAuditEncy", true, false);
        });
        #endregion

        #region Enter Sub First Time

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterSubFirstTime", null);

        #endregion

        #region Hull Facility Logs
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("HullFacilityLogsEncy", () =>
        {
            PDAEncyclopedia.Add("HullFacilityLogsEncy", true, false);
        });
        #endregion
        
        #region Hull Facility Orion Data
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OrionEndeavorsEncy", () =>
        {
            PDAEncyclopedia.Add("OrionEndeavorsEncy", true, false);
        });
        #endregion

        #region Alien Building Block Info
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("AlienBuildingBlockEncy", () =>
        {
            PDAEncyclopedia.Add("AlienBuildingBlockEncy", true, false);
        });
        
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("IonCubeUnlock", () =>
        {
            KnownTech.Add(TechType.PrecursorIonCrystal, false);
        });
        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("IonCubeUnlock", Story.GoalType.Story, AlienBuildingBlock.prefabInfo.TechType);
        #endregion
        
        #region On Enter Engine Facility
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterEngineFacility", () =>
        {
            PDALog.Add("OnEnterEngineFacility", false);
        });
        #endregion

        #region Dead Zone Mapping Initiative Project Data
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("HullFacilityWormTerminalEncy", () =>
        {
            PDAEncyclopedia.Add("HullFacilityWormTerminalEncy", true, false);
            
            // Set up new void goals
            
            Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterVoidWyrmActive", () =>
            {
                PDALog.Add("OnEnterVoidWyrmActive", false);
            });
            
            Nautilus.Handlers.StoryGoalHandler.RegisterBiomeGoal("OnEnterVoidWyrmActive", Story.GoalType.PDA, "void", 5f);
            
            Nautilus.Handlers.StoryGoalHandler.RegisterBiomeGoal("WyrmRadioMessageVoid",  Story.GoalType.Radio, "void", 40f);
            
        });
        #endregion

        #region Fragmentation Terminal
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("FragmentationTerminalEncy", () =>
        {
            PDAEncyclopedia.Add("FragmentationTerminalEncy", true, false);
        });
        #endregion

        #region Animate Entropy Terminal
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("AnimateEntropyTerminalEncy", () =>
        {
            PDAEncyclopedia.Add("AnimateEntropyTerminalEncy", true, false);
        });
        #endregion

        #region Interceptor Facility Locked
        Nautilus.Handlers.StoryGoalHandler.RegisterLocationGoal("OnApproachInterceptorFacility", Story.GoalType.Story,
            new Vector3(547, -709, 955), 400, 1);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnApproachInterceptorFacility", () =>
        {
            if (!Plugin.GlobalSaveData.EngineFacilityPointsRepaired)
            {
                PDALog.Add("ProtoRevisitInterceptorFacility", false);
            }
        });
        #endregion

        #region Transmission Device Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("TransmissionDeviceUnlock", () =>
        {
            KnownTech.Add(ProtoTransmissionDevice.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("TransmissionTerminalEncy", true, false);
        });
        #endregion

        #region Precursor Suit Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("PrecursorSuitTerminal", () =>
        {
            KnownTech.Add(PrecursorSuit.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("PrecursorSuitTerminalEncy", true, false);
        });
        #endregion

        #region Tether Factor Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("TetherFactorTerminal", () =>
        {
            KnownTech.Add(TetherFactor.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("TetherFactorEncy", true, false);
        });
        #endregion

        #region Biomechanics Factor Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("BiomechanicsFactorTerminal", () =>
        {
            KnownTech.Add(BiomechanicsFactor.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("BiomechanicsFactorTerminalEncy", true, false);
        });
        #endregion
        
        #region Color Factor Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ColorFactorTerminal", () =>
        {
            KnownTech.Add(SuitColorFactor.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("SuitColorFactorEncy", true, false);
        });
        #endregion

        #region Propulsion Gloves Terminal
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("PrecursorPropulsionGlovesTerminal", () =>
        {
            KnownTech.Add(PrecursorPropulsionGloves.PrefabInfo.TechType, false);
            PDAEncyclopedia.Add("PrecursorPropulsionGlovesTerminalEncy", true, false);
        });
        #endregion

        #region Precursor Suit Pickup
        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("OnPrecursorSuitPickup", Story.GoalType.PDA, PrecursorSuit.prefabInfo.TechType);

        #endregion

        #region Hoverfish Plush Unlock
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnHoverfishPlushUnlocked", () =>
        {
            KnownTech.Add(HoverfishPlush.prefabInfo.TechType, false);
            PDALog.Add("OnHoverfishPlushUnlocked", false);
        });
        #endregion

        #region Survivor PDA 1
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("SurvivorPDA1", () =>
        {
            PDAEncyclopedia.Add("SurvivorPDA1Ency", true, false);
        });
        #endregion

        #region Survivor PDA 2
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("SurvivorPDA2", () =>
        {
            PDAEncyclopedia.Add("SurvivorPDA2Ency", true, false);
        });
        #endregion

        #region Number Puzzle Entry Voiceline
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterProtoNumberPuzzle", () =>
        {
            PDALog.Add("OnEnterProtoNumberPuzzle", false);

            if (StoryGoalManager.main.IsGoalComplete("PlayerFirstPPTInteraction"))
            {
                StoryGoalManager.main.OnGoalComplete("OnEnterProtoNumberPuzzle_ProfileFound");
            }
            else
            {
                StoryGoalManager.main.OnGoalComplete("OnEnterProtoNumberPuzzle_ProfileNotFound");
            }
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterProtoNumberPuzzle_ProfileFound", () =>
        {
            PDALog.Add("OnEnterProtoNumberPuzzle_ProfileFound", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterProtoNumberPuzzle_ProfileNotFound", () =>
        {
            PDALog.Add("OnEnterProtoNumberPuzzle_ProfileNotFound", false);
        });

        #endregion

        #region Bearing Puzzle Entry Voiceline
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterProtoBearingPuzzle", () =>
        {
            PDALog.Add("OnEnterProtoBearingPuzzle", false);
        });
        #endregion

        #region Lifepod 3 PDA
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ProtoLifepod3PDA", () =>
        {
            PDAEncyclopedia.Add("Lifepod3PDAEncy", true, false);
        });
        #endregion

        #region Number Puzzle Completion
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Ency_ProtoNumbers", () =>
        {
            PDAEncyclopedia.Add("ProtoNumbersEncy", true, false);
        });
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ProtoNumbersHint", () =>
        {
            PDALog.Add("ProtoNumbersHint", true);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("Ency_ProtoNumbers", Story.GoalType.Story, 15f,
            "ProtoNumberPuzzleComplete");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoNumbersHint", Story.GoalType.Story, 10f,
            "ProtoNumberPuzzleComplete");
        #endregion

        #region Bearing Puzzle Completion
        
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ProtoBearingsEncy", () =>
        {
            PDAEncyclopedia.Add("ProtoBearingsEncy", true, false);
        });
        
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoBearingsEncy", Story.GoalType.Encyclopedia, 20f,
            "ProtoBearingPuzzleComplete");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoBearingsHint", Story.GoalType.PDA, 10f,
            "ProtoBearingPuzzleComplete");

        #endregion

        #region Calibration Site Completion

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoCalibrationCodeEncy", Story.GoalType.Encyclopedia, 15f,
            "OnCalibrationRunCompleted");

        #endregion
        
        #region On Almanite Material Identified
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnAlmaniteIdentified", () =>
        {
            PDALog.Add("OnAlmaniteIdentified", false);
        });
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("AlmaniteEncy", () =>
        {
            PDAEncyclopedia.Add("AlmaniteEncy", true,false);
        });
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("AlmaniteEncy", Story.GoalType.Story, 5f, "OnAlmaniteIdentified");
        #endregion

        #region Transmission Device First Loaded
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("TransmissionDeviceFirstLoaded", () =>
        {
            PDALog.Add("Proto_OnTransmissionDeviceFirstLoaded", false);
        });
        #endregion

        #region Engine Facility Scream + PDA hint
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("EngineScream", () =>
        {
            FMODUWE.PlayOneShot(AudioUtils.GetFmodAsset("EngineScream"), Plugin.FACILITY_POSITIONS["EngineFacility"]);
        });

        var engineFacilityReturnHint = CustomPing.CreatePing("EngineFacilityReturnPing", Plugin.HintPingType);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("EngineFacilityReturnHint", () =>
        {
            PDALog.Add("EngineFacilityReturnHint", false);
            UWE.CoroutineHost.StartCoroutine(PuzzleHintRegistration.SpawnPrefab(engineFacilityReturnHint,
                Plugin.FACILITY_POSITIONS["EngineFacility"]));
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("EngineScream", Story.GoalType.Story, 1000f, "HullFacilityWormTerminalEncy");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("EngineFacilityReturnHint", Story.GoalType.PDA, 10f, "EngineScream");
        #endregion

        #region Fins first installed
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("FinsFirstInstalled", () =>
        {
            var hintText = Language.main.Get("ProtoDockVehicleHint");
            Hint.main.message.SetText(hintText, TextAnchor.MiddleCenter);
            Hint.main.message.Show();
        });
        #endregion

        #region Extra worms

        var grandReefPing = CustomPing.CreatePing("ProtoGrandReefPing", Plugin.HintPingType);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnScannedSparseReefWyrms", () =>
        {
            PDALog.Add("OnSparseReefWyrmScanned", false);
            UWE.CoroutineHost.StartCoroutine(
                PuzzleHintRegistration.SpawnPrefab(grandReefPing, new Vector3(-1147, -445, -1110)));
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnScannedGrassyWyrms", () =>
        {
            PDALog.Add("OnGrassyWyrmScanned", false);
        });
        #endregion
        
        #region Archway override installed
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ArchwayOverrideHint", () =>
        {
            var hintText = Language.main.Get("ArchwayOverrideHint");
            Hint.main.message.SetText(hintText, TextAnchor.MiddleCenter);
            Hint.main.message.Show();
        });
        #endregion
        
        #region Bad ending voicelines
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterStoryEndProximity", () =>
        {
            PDALog.Add("BadEndingIntro", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Proto_DeadZoneMappingImminent", () =>
        {
            PDALog.Add("Proto_DeadZoneMappingImminent", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Proto_ReadyingDetectors", () =>
        {
            PDALog.Add("Proto_ReadyingDetectors", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Proto_PleaseDoNotProceed", () =>
        {
            PDALog.Add("Proto_PleaseDoNotProceed", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("Proto_DeadZoneMappingInitialized", () =>
        {
            PDALog.Add("Proto_DeadZoneMappingInitialized", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("Proto_DeadZoneMappingImminent", Story.GoalType.Story, 10, "OnEnterStoryEndProximity");

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("Proto_ReadyingDetectors", Story.GoalType.Story, 10, "Proto_DeadZoneMappingImminent");

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("Proto_PleaseDoNotProceed", Story.GoalType.Story, 10, "Proto_ReadyingDetectors");

        #endregion

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("HullFacilityTeleporterUnlocked", () =>
        {
            FMODUWE.PlayOneShot(AudioUtils.GetFmodAsset("EngineAllBreachesRepaired"), Player.main.transform.position);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OrionSurgicalRoomTome", () =>
        {
            FMODUWE.PlayOneShot(AudioUtils.GetFmodAsset("HullFacilityOrionTone"), Player.main.transform.position);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("HullFacilityActivateWorm", () => WormSpawnEvent.TimeWormsEnabled = Time.time);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("PrototypeCrafted", () =>
        {
            var finType1 = (TechType)Enum.Parse(typeof(TechType), "ProtoFinUpgrade1");
            KnownTech.Add(finType1, false);
            
            var relayType1 = (TechType)Enum.Parse(typeof(TechType), "ProtoRelayUpgrade1");
            KnownTech.Add(relayType1, false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("SubUpgradeHint", () =>
        {
            var hintText = Language.main.Get("SubUpgradeHint");
            Hint.main.message.SetText(hintText, TextAnchor.MiddleCenter);
            Hint.main.message.Show();
        });
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("SubUpgradeHint", Story.GoalType.Story, 35, "PrototypeCrafted");

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("LocatorFactorTerminal", () =>
        {
            KnownTech.Add(LocatorFactor.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("LocatorFactorTerminalEncy", true, false);
        });
        
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("Ency_ProtoFacilitiesEncy", Story.GoalType.Story, 156,
            "PrototypeCrafted");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoFacilityLocationsHint", Story.GoalType.Story, 150,
            "PrototypeCrafted");

        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("OnPickupDefenseTablet", Story.GoalType.Story,
            DefenseFacilityKey.prefabInfo.TechType);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnPickupDefenseTablet", () =>
        {
            KnownTech.Add(DefenseFacilityKey.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("DefenseFacilityTabletEncy", true, false);
        });
        
        Nautilus.Handlers.StoryGoalHandler.RegisterItemGoal("OnPickupIonPrism", Story.GoalType.Story,
            IonPrism_Craftable.prefabInfo.TechType);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnPickupIonPrism", () =>
        {
            KnownTech.Add(IonPrism_Craftable.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("ProtoIonPrismEncy", true, false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterLocationGoal("ProtoApproachEngineFacility", Story.GoalType.Story, new Vector3(-530, -465, 1530), 300,
            3);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ProtoApproachEngineFacility", () =>
        {
            PDALog.Add("ProtoApproachEngineFacility", false);
        });

        #region Interceptor tablet

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("InterceptorFacilityTabletEncy", () =>
        {
            PDALog.Add("InterceptorFacilityTabletUnlock", false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("InterceptorFacilityTabletEncyUnlock", () =>
        {
            KnownTech.Add(InterceptorFacilityKey.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("InterceptorFacilityTabletEncy", true, false);
        });

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("InterceptorFacilityTabletEncyUnlock", Story.GoalType.Encyclopedia, 7, "InterceptorFacilityTabletEncy");
        #endregion

        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("UnlockEngineFacilityKey", Story.GoalType.Story, 16,
            "ProtoApproachEngineFacility");
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("UnlockEngineFacilityKey", () =>
        {
            KnownTech.Add(EngineFacilityKey.prefabInfo.TechType, false);
            PDAEncyclopedia.Add("EngineFacilityTabletEncy", true, false);
        });
        

        Nautilus.Handlers.StoryGoalHandler.RegisterBiomeGoal("OnEnterPrecursorGun", Story.GoalType.PDA, "Precursor_Gun_OuterRooms", 0, delay: 20);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnEnterPrecursorGun", () =>
        {
            PDALog.Add("OnEnterPrecursorGun", false);
        });
        
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("OnApproachPPT", () =>
        {
            PDALog.Add("Proto_ApproachTerminal", false);
        });
        
        Nautilus.Handlers.StoryGoalHandler.RegisterBiomeGoal("ProtoOnEnterGrandReef", Story.GoalType.PDA, "grandReef", 10);
        Nautilus.Handlers.StoryGoalHandler.RegisterCustomEvent("ProtoOnEnterGrandReef", () =>
        {
            PDALog.Add("ProtoOnEnterGrandReef", false);
        });
        
        sw.Stop();
        Plugin.Logger.LogInfo($"Story goals registered in {sw.ElapsedMilliseconds}ms");
    }
}
