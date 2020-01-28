using BattleTech;
using BattleTech.UI;
using BattleTech.UI.Tooltips;
using Harmony;
using Localize;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExpandedInitiative {

    // Sets the initiative value in the mech-bay
    [HarmonyPatch(typeof(MechBayMechInfoWidget), "SetInitiative")]
    [HarmonyPatch(new Type[] { })]
    public static class MechBayMechInfoWidget_SetInitiative {
        public static void Postfix(MechBayMechInfoWidget __instance, MechDef ___selectedMech,
            GameObject ___initiativeObj, TextMeshProUGUI ___initiativeText, HBSTooltip ___initiativeTooltip) {
            Mod.Log.Trace("MBMIW:SI:post - entered.");

            if (___initiativeObj == null || ___initiativeText == null) {
                return;
            }

            if (___selectedMech == null) {
                ___initiativeObj.SetActive(true);
                ___initiativeText.SetText("-");
            } else {
                List<string> details = new List<string>();

                // Static initiative from tonnage

                int initPhase = PhaseHelper.TonnageToPhase((int)Math.Ceiling(___selectedMech.Chassis.Tonnage));
                int initLabelVal = (Mod.MaxPhase + 1) - initPhase;
                details.Add(new Text(Mod.Config.LocalizedText[ModConfig.LT_TT_BASE], new object[] { initLabelVal }).ToString());

                // Any bonuses from equipment
                int componentBonus = UnitHelper.GetNormalizedComponentModifier(___selectedMech);
                string componentColor = "FFFFFF";
                if (componentBonus > 0) { componentColor = "00FF00"; }
                if (componentBonus < 0) { componentColor = "FF0000"; }
                details.Add(new Text(Mod.Config.LocalizedText[ModConfig.LT_TT_COMPONENT], new object[] { componentColor, componentBonus }).ToString());
                Mod.Log.Debug($"Component bonus is: {componentBonus}");

                // No Lance bonus
                // No Pilot bonus

                // --- Badge ---
                int summaryInitLabel = initLabelVal + componentBonus;
                if (summaryInitLabel > Mod.MaxPhase) { summaryInitLabel = Mod.MaxPhase; }
                else if (summaryInitLabel < Mod.MinPhase) { summaryInitLabel = Mod.MinPhase; }

                ___initiativeObj.SetActive(true);
                ___initiativeText.SetText($"{summaryInitLabel}");

                // --- Tooltip ---
                string tooltipTitle = $"{___selectedMech.Name}";
                string detailsS = String.Join("\n", details.ToArray());
                string tooltipText = new Text(Mod.Config.LocalizedText[ModConfig.LT_MB_TT_SUMMARY], new object[] { detailsS, summaryInitLabel }).ToString();
                BaseDescriptionDef initiativeData = new BaseDescriptionDef("MB_MIW_MECH_TT", tooltipTitle, tooltipText, null);
                ___initiativeTooltip.enabled = true;
                ___initiativeTooltip.SetDefaultStateData(TooltipUtilities.GetStateDataFromObject(initiativeData));
            }
        }
    }

    // Sets the initiative value in the lance loading screen. Because we want it to be understandable to players, 
    //  invert the values so they reflect the phase IDs, not the actual value
    [HarmonyPatch(typeof(LanceLoadoutSlot), "RefreshInitiativeData")]
    [HarmonyPatch(new Type[] { })]
    public static class LanceLoadoutSlot_RefreshInitiativeData {
        public static void Postfix(LanceLoadoutSlot __instance, GameObject ___initiativeObj, TextMeshProUGUI ___initiativeText,
            UIColorRefTracker ___initiativeColor, HBSTooltip ___initiativeTooltip, LanceConfiguratorPanel ___LC) {
            Mod.Log.Trace("LLS:RID:post - entered.");

            if (___initiativeObj == null || ___initiativeText == null || ___initiativeColor == null || ___initiativeTooltip == null) {
                return;
            }

            bool bothSelected = __instance.SelectedMech != null && __instance.SelectedPilot != null;
            if (!bothSelected) {
                ___initiativeText.SetText("-");
                ___initiativeColor.SetUIColor(UIColor.MedGray);
            } else {
                // --- MECH ---
                MechDef selectedMechDef = __instance.SelectedMech.MechDef;
                List<string> details = new List<string>();

                // Static initiative from tonnage
                int initPhase = PhaseHelper.TonnageToPhase((int)Math.Ceiling(__instance.SelectedMech.MechDef.Chassis.Tonnage));
                int initLabelVal = (Mod.MaxPhase + 1) - initPhase;
                details.Add(new Text(Mod.Config.LocalizedText[ModConfig.LT_TT_BASE], new object[] { initLabelVal }).ToString());

                // Any bonuses from equipment
                int componentBonus = UnitHelper.GetNormalizedComponentModifier(__instance.SelectedMech.MechDef);
                string componentColor = "FFFFFF";
                if (componentBonus > 0) { componentColor = "00FF00"; }
                if (componentBonus < 0) { componentColor = "FF0000"; }
                details.Add(new Text(Mod.Config.LocalizedText[ModConfig.LT_TT_COMPONENT], new object[] { componentColor, componentBonus }).ToString()); 
                Mod.Log.Debug($"Component bonus is: {componentBonus}");

                // --- LANCE ---
                int lanceBonus = 0;
                if (___LC != null) {
                    lanceBonus = ___LC.lanceInitiativeModifier;
                }
                string lanceColor = "FFFFFF";
                if (lanceBonus > 0) { lanceColor = "00FF00"; }
                if (lanceBonus < 0) { lanceColor = "FF0000"; }
                details.Add(new Text(Mod.Config.LocalizedText[ModConfig.LT_TT_LANCE], new object[] { lanceColor, lanceBonus }).ToString());

                // --- PILOT ---
                // TODO: Get pilot modifiers from abilities - coordinate with BD
                Pilot selectedPilot = __instance.SelectedPilot.Pilot;
                int pilotBonus = 0;
                string pilotColor = "FFFFFF";
                if (pilotBonus > 0) { pilotColor = "00FF00"; }
                if (pilotBonus < 0) { pilotColor = "FF0000"; }
                details.Add(new Text(Mod.Config.LocalizedText[ModConfig.LT_TT_PILOT], new object[] { pilotColor, pilotBonus }).ToString());

                // --- Badge ---
                int summaryInitLabel = initLabelVal + componentBonus + pilotBonus + lanceBonus;
                if (summaryInitLabel > Mod.MaxPhase) { summaryInitLabel = Mod.MaxPhase; } 
                else if (summaryInitLabel < Mod.MinPhase) { summaryInitLabel = Mod.MinPhase; }
                ___initiativeText.SetText($"{summaryInitLabel}");
                ___initiativeText.color = Color.black;
                ___initiativeColor.SetUIColor(UIColor.White);

                // --- Tooltip ---
                string tooltipTitle = $"{selectedMechDef.Name}: {selectedPilot.Name}";
                string detailsS = String.Join("\n", details.ToArray());
                string tooltipText = new Text(Mod.Config.LocalizedText[ModConfig.LT_LC_TT_SUMMARY], new object[] { detailsS, summaryInitLabel }).ToString();
                BaseDescriptionDef initiativeData = new BaseDescriptionDef("LLS_MECH_TT", tooltipTitle, tooltipText, null);
                ___initiativeTooltip.enabled = true;
                ___initiativeTooltip.SetDefaultStateData(TooltipUtilities.GetStateDataFromObject(initiativeData));
            }
        }

    }
}