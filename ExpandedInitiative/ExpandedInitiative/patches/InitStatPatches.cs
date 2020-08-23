using BattleTech;
using Harmony;
using HBS.Collections;
using System;
using us.frostraptor.modUtils;

namespace ExpandedInitiative.patches {

    // InitEffectStats fires in the middle of InitStats, allowing us to manipulate baseInit before any status effects are applied.
    [HarmonyPatch(typeof(Mech), "InitEffectStats")]
    public static class Mech_InitEffectStats {
        public static void Postfix(Mech __instance) {
            int initPhase = PhaseHelper.TonnageToPhase((int)Math.Ceiling(__instance.tonnage));
            Mod.Log.Debug?.Write($"Setting baseInit for mech: {CombatantUtils.Label(__instance)} to {initPhase}");

            // We have to remove the stat, then set it to the default value we want. We do this because when the history is reverted (from effect expiriation) 
            //   it reverts to the value initally set on the statistic
            __instance.StatCollection.RemoveStatistic(ModStats.BaseInitiative);
            __instance.StatCollection.AddStatistic<int>(ModStats.BaseInitiative, initPhase);
        }
    }

    [HarmonyPatch(typeof(Vehicle), "InitEffectStats")]
    public static class Vehicle_InitEffectStats {
        public static void Postfix(Vehicle __instance) {
            int initPhase = PhaseHelper.TonnageToPhase((int)Math.Ceiling(__instance.tonnage));
            Mod.Log.Debug?.Write($"Setting baseInit for vehicle: {CombatantUtils.Label(__instance)} to {initPhase}");
            __instance.StatCollection.Set<int>(ModStats.BaseInitiative, initPhase);
        }
    }

    [HarmonyPatch(typeof(Turret), "InitEffectStats")]
    public static class Turret_InitEffectStats {
        public static void Postfix(Turret __instance) {
            int init = Mod.Config.TurretPhases.UnitNone;
            TagSet actorTags = __instance.GetTags();
            if (actorTags != null && actorTags.Contains("unit_light")) {
                init = Mod.Config.TurretPhases.UnitLight;
            } else if (actorTags != null && actorTags.Contains("unit_medium")) {
                init = Mod.Config.TurretPhases.UnitMedium;
            } else if (actorTags != null && actorTags.Contains("unit_heavy")) {
                init = Mod.Config.TurretPhases.UnitHeavy;
            } 
         
            Mod.Log.Debug?.Write($"Setting baseInit for turret: {CombatantUtils.Label(__instance)} to {init}");
            __instance.StatCollection.Set<int>(ModStats.BaseInitiative, init);
        }
    }
}
