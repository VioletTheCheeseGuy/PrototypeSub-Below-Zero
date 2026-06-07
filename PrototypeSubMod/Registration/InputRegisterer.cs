using Nautilus.Handlers;

namespace PrototypeSubMod.Registration;

public static class InputRegisterer
{
    public static GameInput.Button TetherSubButton;
    public static GameInput.Button TetherMarkerButton;
    public static GameInput.Button LocatorButton;
    
    public static void Register()
    {
        TetherSubButton = EnumHandler.AddEntry<GameInput.Button>("ProtoTetherSubButton");
        
        TetherMarkerButton = EnumHandler.AddEntry<GameInput.Button>("ProtoTetherMarkerButton");
        
        LocatorButton = EnumHandler.AddEntry<GameInput.Button>("ProtoLocatorPingButton");
    
    }
}