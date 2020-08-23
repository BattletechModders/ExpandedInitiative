using BattleTech;
using Harmony;
using us.frostraptor.modUtils;

namespace ExpandedInitiative {

    //[HarmonyPatch(typeof(AbstractActor), "OnNewRound")]
    //public static class AbstractActor_OnNewRound {
    //    public static void Postfix(AbstractActor __instance, int round) {
    //        Mod.Log.Trace?.Write("AA:ONR - entered.");
    //        Mod.Log.Debug?.Write($"  AbstractActor starting new round {round}, recalculating initiative element for actor:{__instance.DisplayName}");
    //        ActorInitiativeHolder.OnRoundBegin(__instance);
    //    }
    //}

    [HarmonyPatch(typeof(AbstractActor), "InitiativeToString")]
    public static class AbstractActor_InitiativeToString {
        public static void Postfix(AbstractActor __instance, ref string __result, int initiative) {
            Mod.Log.Trace?.Write($"AA:ITS - entered for init:{initiative}.");
            switch (initiative) {
                case 1:
                    __result = "10";
                    break;
                case 2:
                    __result = "9";
                    break;
                case 3:
                    __result = "8";
                    break;
                case 4:
                    __result = "7";
                    break;
                case 5:
                    __result = "6";
                    break;
                case 6:
                    __result = "5";
                    break;
                case 7:
                    __result = "4";
                    break;
                case 8:
                    __result = "3";
                    break;
                case 9:
                    __result = "2";
                    break;
                case 10:
                    __result = "1";
                    break;
                default:
                    if (initiative > Mod.MaxPhase) {
                        // This looks weird, but it's the only place we can intercept a negative init that I've found.
                        if (__instance != null) { __instance.Initiative = Mod.MaxPhase; }
                        Mod.Log.Error?.Write($"Bad initiative of {initiative} detected for actor: {CombatantUtils.Label(__instance)}! Setting to {Mod.MaxPhase}");
                        __result = "1";
                    } else if (initiative < Mod.MinPhase) {
                        // This looks weird, but it's the only place we can intercept a negative init that I've found.
                        if (__instance != null) { __instance.Initiative = Mod.MinPhase; }
                        Mod.Log.Error?.Write($"Bad initiative of {initiative} detected for actor: {CombatantUtils.Label(__instance)}! Setting to {Mod.MinPhase}");
                        __result = "10";
                    }
                    break;
            }
            Mod.Log.Trace?.Write($" returning {__result} for initiative {initiative}");
        }
    }

    // TODO: This is likely inlined and doesn't work- confirm
    [HarmonyPatch(typeof(AbstractActor))]
    [HarmonyPatch("HasValidInitiative", MethodType.Getter)]
    public static class AbstractActor_HasValidInitiative {
        public static void Postfix(AbstractActor __instance, bool __result) {
            Mod.Log.Debug?.Write("AA:HVI - entered.");
            bool isValid = __instance.Initiative >= Mod.MinPhase && __instance.Initiative <= Mod.MaxPhase;
            if (!isValid) {
                Mod.Log.Info?.Write($"Actor:{CombatantUtils.Label(__instance)} has invalid initiative {__instance.Initiative}!");
            }
            __result = isValid;
            Mod.Log.Debug?.Write($"AbstractActor:HasValidInitiative returning {__result} for {__instance.Initiative}");
        }
    }

    [HarmonyPatch(typeof(AbstractActor))]
    [HarmonyPatch("BaseInitiative", MethodType.Getter)]
    public static class AbstractActor_BaseInitiative {
        public static void Postfix(AbstractActor __instance, ref int __result, StatCollection ___statCollection) {
            Mod.Log.Trace?.Write("AA:BI - entered.");

            Mod.Log.Debug?.Write($"Actor:({CombatantUtils.Label(__instance)}) has raw result: {__result}");
            if (__instance.Combat.TurnDirector.IsInterleaved) {
                int baseInit = ___statCollection.GetValue<int>("BaseInitiative");
                int phaseMod = ___statCollection.GetValue<int>("PhaseModifier");
                int modifiedInit = baseInit + phaseMod;

                if (modifiedInit < Mod.MinPhase) {
                    Mod.Log.Info?.Write($"Actor:({CombatantUtils.Label(__instance)}) being set to {Mod.MinPhase} due to BaseInit:{baseInit} + PhaseMod:{phaseMod}");
                    __result = Mod.MinPhase;
                } else if (modifiedInit > Mod.MaxPhase) {
                    Mod.Log.Info?.Write($"Actor:({CombatantUtils.Label(__instance)}) being set to {Mod.MaxPhase} due to BaseInit:{baseInit} + PhaseMod:{phaseMod}");
                    __result = Mod.MaxPhase;
                } else {
                    Mod.Log.Info?.Write($"Actor:({CombatantUtils.Label(__instance)}) has stats BaseInit:{baseInit} + PhaseMod:{phaseMod} = modifiedInit:{modifiedInit}.");
                    __result = modifiedInit;
                }
            } else {
                Mod.Log.Info?.Write($"Actor:({CombatantUtils.Label(__instance)}) is non-interleaved, returning phase: {Mod.MaxPhase}.");
                __result = Mod.MaxPhase;
            }

        }
    }

    [HarmonyPatch(typeof(AbstractActor), "ForceUnitToLastPhase")]
    public static class AbstractActor_ForceUnitToLastPhase {
        public static void Postfix(AbstractActor __instance) {
            Mod.Log.Info?.Write($" FORCING ACTOR: {CombatantUtils.Label(__instance)} TO LAST COMBAT ROUND!");
        }
    }

    [HarmonyPatch(typeof(AbstractActor), "ForceUnitOnePhaseDown")]
    public static class AbstractActor_ForceUnitOnePhaseDown {
        public static void Postfix(AbstractActor __instance) {
            Mod.Log.Info?.Write($" FORCING ACTOR: {CombatantUtils.Label(__instance)} ONE PHASE DOWN!");
        }
    }

    [HarmonyPatch(typeof(AbstractActor), "ForceUnitOnePhaseUp")]
    public static class AbstractActor_ForceUnitOnePhaseUp {
        public static void Postfix(AbstractActor __instance) {
            Mod.Log.Info?.Write($" FORCING ACTOR: {CombatantUtils.Label(__instance)} ONE PHASE UP!");
        }
    }

}
