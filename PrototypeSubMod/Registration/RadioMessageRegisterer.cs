using Nautilus.Handlers;
using PrototypeSubMod.Patches;
using Story;

namespace PrototypeSubMod.Registration;

public static class RadioMessageRegisterer
{
    public static void Register()
    {
        #region Transmissions

        RegisterMessage("ProtoRadioMessage1", "ProtoRadioMessage1");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoRadioMessage1", Story.GoalType.Radio, 500, "PlayerFirstPPTInteraction");
        RegisterMessage("ProtoRadioMessage2", "ProtoRadioMessage2");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoRadioMessage2", Story.GoalType.Radio, 1000, "ProtoRadioMessage1");
        RegisterMessage("ProtoRadioMessage3", "ProtoRadioMessage3");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoRadioMessage3", Story.GoalType.Radio, 1000, "ProtoRadioMessage2", "HullFacilityWormTerminalEncy");
        
        RegisterMessage("ProtoRadioMessage4", "ProtoRadioMessage3");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("ProtoRadioMessage4", Story.GoalType.Radio, 500, "OnCalibrationRunCompleted", "TransmissionDeviceUnlock");
        
        // Wyrm messages
        RegisterMessage("WyrmRadioMessageActivated", "WyrmRadioMessageActivated");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("WyrmRadioMessageActivated", Story.GoalType.Radio, 10, "HullFacilityWormTerminalEncy");
        
        RegisterMessage("WyrmRadioMessageVoid", "WyrmRadioMessageVoid");
        Nautilus.Handlers.StoryGoalHandler.RegisterCompoundGoal("PDA_OnEnterVoidWyrmActivated", Story.GoalType.PDA, 0f, "WyrmRadioMessageVoid");
        #endregion
    }

    private static void RegisterMessage(string key, string audioAssetName)
    {
        Nautilus.Handlers.StoryGoalHandler.RegisterBiomeGoal(key, Story.GoalType.Radio, "Unobtanium", 1);
        PDALog_Patches.entries.Add((audioAssetName, key));
    }
}