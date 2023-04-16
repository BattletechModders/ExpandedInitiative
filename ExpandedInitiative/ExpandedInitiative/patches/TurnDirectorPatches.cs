using System;

namespace ExpandedInitiative.patches
{

    [HarmonyPatch(typeof(TurnDirector), MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(CombatGameState) })]
    public static class TurnDirector_ctor
    {
        public static void Postfix(TurnDirector __instance)
        {
            Mod.Log.Debug?.Write("TD:ctor:post - entered.");
            Mod.Log.Debug?.Write($" TurnDirector init with phases: {__instance.FirstPhase} / {__instance.LastPhase}");

            Traverse firstT = Traverse.Create(__instance).Property("FirstPhase");
            firstT.SetValue(Mod.MinPhase);

            Traverse lastT = Traverse.Create(__instance).Property("LastPhase");
            lastT.SetValue(Mod.MaxPhase);

            Mod.Log.Debug?.Write($" TurnDirector updated to phases: {__instance.FirstPhase} / {__instance.LastPhase}");
        }
    }

    [HarmonyPatch(typeof(TurnDirector), "BeginNewRound")]
    public static class TurnDirector_BeginNewRound
    {
        public static void Postfix(int round)
        {
            Mod.Log.Debug?.Write($"  == Beginning round: {round} ==");
        }
    }

}
