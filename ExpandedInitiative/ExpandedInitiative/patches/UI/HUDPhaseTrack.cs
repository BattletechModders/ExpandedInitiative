using BattleTech;
using BattleTech.UI;
using Harmony;
using System;
using System.Collections.Generic;

namespace ExpandedInitiative {

    [HarmonyPatch(typeof(CombatHUDPhaseTrack), "RefreshPhaseColors")]
    public static class CombatHUDPhaseTrack_RefreshPhaseColors {
        public static bool Prefix(CombatHUDPhaseTrack __instance, bool isPlayer, Hostility hostility, int ___currentPhase, CombatHUDPhaseBar[] ___phaseBars) {
            Mod.Log.Debug("CHUDPT::RPC - entered.");

            if (__instance == null || ___phaseBars == null) { return true; }
            if (!ModState.Combat.TurnDirector.IsInterleaved) { return true; }

            // TODO: FIX HARDCODED VALUE
            // Reconcile phase (from 1 - X) with display (X to 1)
            int initNum = (Mod.MaxPhase + 1) - ___currentPhase;
            int[] phaseBounds = PhaseHelper.CalcPhaseIconBounds(___currentPhase);
            Mod.Log.Debug($" For currentPhase: {___currentPhase}  phaseBounds are: [ {phaseBounds[0]} {phaseBounds[1]} {phaseBounds[2]} {phaseBounds[3]} {phaseBounds[4]} ]");

            for (int i = 0; i < 5; i++) {
                if (phaseBounds[i] > initNum) {
                    Mod.Log.Debug($" Setting phase: {phaseBounds[i]} as past phase.");
                    ___phaseBars[i].IndicatePastPhase();
                } else if (phaseBounds[i] == initNum) {
                    Mod.Log.Debug($" Setting phase: {phaseBounds[i]} as current phase.");
                    ___phaseBars[i].IndicateCurrentPhase(isPlayer, hostility);
                } else {
                    Mod.Log.Debug($" Setting phase: {phaseBounds[i]} as future phase.");
                    ___phaseBars[i].IndicateFuturePhase(isPlayer, hostility);
                }
                ___phaseBars[i].Text.SetText($"{phaseBounds[i]}");

            }

            if (phaseBounds[0] != Mod.MaxPhase) {
                ___phaseBars[0].Text.SetText("P");
            }
            if (phaseBounds[4] != Mod.MinPhase) {
                ___phaseBars[4].Text.SetText("F");
            }
                        
            return false;
        }
    }

    [HarmonyPatch(typeof(CombatHUDPhaseTrack), "SetTrackerPhase")]
    [HarmonyPatch(new Type[] { typeof(CombatHUDIconTracker), typeof(int) })]
    public static class CombatHUDPhaseTrack_SetTrackerPhase {
        public static bool Prefix(CombatHUDPhaseTrack __instance, CombatHUDIconTracker tracker, int phase, int ___currentPhase, List<CombatHUDPhaseIcons> ___PhaseIcons) {
            Mod.Log.Trace($"CHUDPT:STP - entered at phase: {phase}.");

            int[] bounds = PhaseHelper.CalcPhaseIconBounds(___currentPhase);
            int phaseAsInit = (Mod.MaxPhase + 1) - phase;
            Mod.Log.Trace($"Phase {phase} is init {phaseAsInit} within currentPhase: {___currentPhase} with bounds: {bounds[0]}-{bounds[4]}");

            if (phaseAsInit > bounds[1]) {
                Mod.Log.Trace($"  -- Phase icon is higher than {bounds[1]}, setting to P phase.");
                ___PhaseIcons[0].AddIconTrackerToPhase(tracker);
            } else if (phaseAsInit < bounds[3]) {
                Mod.Log.Trace($"  -- Phase icon is higher than {bounds[3]}, setting to F phase.");
                ___PhaseIcons[4].AddIconTrackerToPhase(tracker);
            } else {
                for (int i = 0; i < 5; i++) {
                    if (bounds[i] == phaseAsInit) {
                        Mod.Log.Trace($"  -- Setting phase icon for phaseAsInit: {phaseAsInit} / bounds: {bounds[i]} at index {i}");
                        ___PhaseIcons[i].AddIconTrackerToPhase(tracker);
                    }
                }
            }

            return false;
        }
    }

}
