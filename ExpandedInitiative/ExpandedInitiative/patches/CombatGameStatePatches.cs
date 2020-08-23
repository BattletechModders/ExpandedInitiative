using BattleTech;
using Harmony;

namespace ExpandedInitiative.patches {

    [HarmonyPatch(typeof(CombatGameState), "_Init")]
    public static class CombatGameState__Init {
        public static void Postfix(CombatGameState __instance) {
            Mod.Log.Debug?.Write("Caching CombatGameState");
            ModState.Combat = __instance;
        }
    }

    [HarmonyPatch(typeof(CombatGameState), "OnCombatGameDestroyed")]
    public static class CombatGameState_OnCombatGameDestroyed {
        public static void Postfix(CombatGameState __instance) {
            Mod.Log.Debug?.Write("Clearing cached copy of CombatGameState");
            ModState.Combat = null;
        }
    }

}
