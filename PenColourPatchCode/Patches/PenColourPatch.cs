using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Screens.Map;

namespace PenColourPatch.PenColourPatchCode.Patches;

[HarmonyPatch(typeof(NMapDrawings), "CreateLineForPlayer")]
public class PenColourPatch
{
    private static double id;
    
    static void Prefix(Player player)
    {
        id = (double) player.NetId;
        // MainFile.Logger.Info($"Player NetID: {player.NetId}.");
    }
    
    static void Postfix(ref Line2D __result)
    {
        // Generate a small random colour deviation +/- the original value using the player id
        float deviation = 0.2f;
        
        float red = (float) ((Math.Cos(id) - 0.5) * deviation);
        float green = (float) ((Math.Sin(id) - 0.5) * deviation);
        float blue = (float) ((((Math.Cos(id) + Math.Sin(id)) / 2) - 0.5) * deviation);
        
        MainFile.Logger.Info($"ID: {id}, Red: {red:F}, Green: {green:F}, Blue: {blue:F}");
        
        __result.DefaultColor += new Color(red, green, blue);
    }
}