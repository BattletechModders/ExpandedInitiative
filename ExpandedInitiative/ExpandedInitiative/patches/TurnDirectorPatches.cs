﻿using BattleTech;
using Harmony;
using System;

namespace ExpandedInitiative.patches {

    [HarmonyPatch(typeof(TurnDirector), MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(CombatGameState) })]
    public static class TurnDirector_ctor {
        public static void Postfix(TurnDirector __instance) {
            Mod.Log.Debug("TD:ctor:post - entered.");
            Mod.Log.Debug($" TurnDirector init with phases: {__instance.FirstPhase} / {__instance.LastPhase}");
            
            Traverse firstT = Traverse.Create(__instance).Property("FirstPhase");
            firstT.SetValue(Mod.MinPhase);

            Traverse lastT = Traverse.Create(__instance).Property("LastPhase");
            lastT.SetValue(Mod.MaxPhase);

            Mod.Log.Debug($" TurnDirector updated to phases: {__instance.FirstPhase} / {__instance.LastPhase}");
        }
    }

    //[HarmonyPatch(typeof(TurnDirector), "OnCombatGameDestroyed")]
    //public static class TurnDirector_OnCombatGameDestroyed {
    //    public static void Postfix(TurnDirector __instance) {
    //        Mod.Log.Trace("TD:OCGD:post - entered.");
    //        Mod.Log.Debug($" TurnDirector - Combat complete, destroying initiative map.");
    //        ActorInitiativeHolder.OnCombatComplete();
    //    }
    //}
}