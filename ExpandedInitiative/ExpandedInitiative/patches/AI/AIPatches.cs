using BattleTech;
using Harmony;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandedInitiative.patches {

    /* This patch is required because AdditionalDamageFromFriendsGainedByAttackingEvasiveTarget has hardcoded bounds for the initiative phases.
     *   This is normally unused in RougeTech, because Float_PipStripAttackProbabilityThreshold normally is set to 1. When this is true, no
     *   values will pass through AttackEvaluator and end up calling this method. 
     *   
     *   However, there are suspicions that this behavior pushes the AI towards non-ranged attack behaviors. To allow testing of that theory, this 
     *     method replaces the logic to support all phases in this mod.
     */
    [HarmonyPatch()]
    public static class AttackEvaluator_AdditionalDamageFromFriendsGainedByAttackingEvasiveTarget {

        public static MethodInfo TargetMethod() {
            return AccessTools.Method(typeof(AttackEvaluator), "AdditionalDamageFromFriendsGainedByAttackingEvasiveTarget");
        }

        private static bool Prefix(ref float __result, AbstractActor unit, AbstractActor target, Vector3 targetPosition, int pipsRemoved) {
            Mod.Log.Trace?.Write("AE:ADFP:pre - entered.");

            Mod.Log.Trace?.Write($"  ---- AE_ADFP: Building list.");
            List<AbstractActor> list = new List<AbstractActor>();
            Dictionary<int, List<AbstractActor>> dictionary = new Dictionary<int, List<AbstractActor>>();
            int i;
            for (i = Mod.MinPhase; i <= Mod.MaxPhase; i++) {
                dictionary[i] = new List<AbstractActor>();
            }

            Mod.Log.Trace?.Write($"  ---- AE_ADFP: Mapping lance to init.");
            for (int j = 0; j < unit.lance.unitGuids.Count; j++) {
                string text = unit.lance.unitGuids[j];
                if (!(text == unit.GUID)) {
                    AbstractActor itemByGUID = unit.Combat.ItemRegistry.GetItemByGUID<AbstractActor>(text);
                    int initiative = itemByGUID.Initiative;
                    dictionary[initiative].Add(itemByGUID);
                }
            }

            Mod.Log.Trace?.Write($"  ---- AE_ADFP: Mapping all actors.");
            int currentPhase = unit.Combat.TurnDirector.CurrentPhase;
            for (int k = 0; k < dictionary[currentPhase].Count; k++) {
                AbstractActor abstractActor = dictionary[currentPhase][k];
                if (!abstractActor.HasActivatedThisRound) {
                    list.Add(abstractActor);
                }
            }

            Mod.Log.Trace?.Write($"  ---- AE_ADFP: actors for init");
            i = currentPhase;
            while (target.Initiative != i) {
                i++;
                if (i > Mod.MaxPhase) {
                    i = Mod.MinPhase;
                }
                for (int l = 0; l < dictionary[i].Count; l++) {
                    AbstractActor item = dictionary[i][l];
                    list.Add(item);
                }
            }

            Mod.Log.Trace?.Write($"  ---- AE_ADFP: Calulating");
            float num = 0f;
            float num2 = 0f;
            int evasivePipsCurrent = target.EvasivePipsCurrent;
            int evasivePipsCurrent2 = Mathf.Max(0, evasivePipsCurrent - pipsRemoved);
            Mod.Log.Debug?.Write($"  ---- AE_ADFP: Assumes we can remove: {evasivePipsCurrent2} pips ");
            for (int m = 0; m < list.Count; m++) {
                AbstractActor abstractActor2 = list[m];
                for (int n = 0; n < abstractActor2.Weapons.Count; n++) {
                    Weapon weapon = abstractActor2.Weapons[n];
                    if (weapon.CanFire && abstractActor2.HasLOFToTargetUnit(target, weapon)) {
                        target.EvasivePipsCurrent = evasivePipsCurrent;
                        float toHitFromPosition = weapon.GetToHitFromPosition(target, 1, abstractActor2.CurrentPosition, target.CurrentPosition, true, true, false);
                        num2 += toHitFromPosition * weapon.DamagePerShot * (float)weapon.ShotsWhenFired;
                        target.EvasivePipsCurrent = evasivePipsCurrent2;
                        toHitFromPosition = weapon.GetToHitFromPosition(target, 1, abstractActor2.CurrentPosition, target.CurrentPosition, true, true, false);
                        num += toHitFromPosition * weapon.DamagePerShot * (float)weapon.ShotsWhenFired;
                    }
                }
            }
            target.EvasivePipsCurrent = evasivePipsCurrent;
            __result = num - num2;

            return false;
        }
    }
}
