using BattleTech;
using BattleTech.UI;
using DG.Tweening;
using Harmony;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExpandedInitiative {

    // === CombatHUDPortrait === : The mechwarrior picture in the bottom tray

    // Corrects the init overlay displayed on the Mechwarrior
    [HarmonyPatch(typeof(CombatHUDPortrait), "Init")]
    [HarmonyPatch(new Type[] { typeof(CombatGameState), typeof(CombatHUD), typeof(UnityEngine.UI.LayoutElement), typeof(HBSDOTweenToggle) })]
    public static class CombatHUDPortrait_Init {
        public static void Postfix(CombatHUDPortrait __instance, ref TextMeshProUGUI ___ioText, ref DOTweenAnimation ___initiativeOverlay) {
            //SkillBasedInit.Logger.Log($"CombatHUDPortrait:Init:post - Init");
            ___ioText.enableWordWrapping = false;
            ___initiativeOverlay.isActive = false;
        }
    }

    [HarmonyPatch(typeof(CombatHUDPortrait), "IndicatePastPhase")]
    [HarmonyPatch(new Type[] { })]
    public static class CombatHUDPortrait_IndicatePastPhase {
        public static void Postfix(CombatHUDPortrait __instance, TextMeshProUGUI ___ioText) {
            __instance.NumberFlagFill.color = Color.white;
            __instance.NumberFlagText.color = Color.white;
            __instance.NumberFlagOutline.color = Color.white;
            ___ioText.color = Color.white;

            Transform frameT = __instance.FilledHolder.transform.Find("mw_Frame");
            if (frameT != null) {
                GameObject frame = frameT.gameObject;
                Transform nameRectT = frameT.transform.Find("mw_NameRect");
                if (nameRectT != null) {
                    GameObject nameRect = nameRectT.gameObject;
                    Image nameImg = nameRect.GetComponent<Image>();
                    nameImg.color = Mod.Config.FriendlyAlreadyActivated;
                }
            }
        }
    }

    [HarmonyPatch(typeof(CombatHUDPortrait), "IndicateCurrentPhase")]
    [HarmonyPatch(new Type[] { })]
    public static class CombatHUDPortrait_IndicateCurrentPhase {
        public static void Postfix(CombatHUDPortrait __instance, TextMeshProUGUI ___ioText) {
            __instance.NumberFlagFill.color = Color.white;
            __instance.NumberFlagOutline.color = Color.white;
            __instance.NumberFlagText.color = Color.white;
            ___ioText.color = Color.white;

            Transform frameT = __instance.FilledHolder.transform.Find("mw_Frame");
            if (frameT != null) {
                GameObject frame = frameT.gameObject;
                Transform nameRectT = frameT.transform.Find("mw_NameRect");
                if (nameRectT != null) {
                    GameObject nameRect = nameRectT.gameObject;
                    Image nameImg = nameRect.GetComponent<Image>();

                    if (__instance.DisplayedActor.HasActivatedThisRound) {
                        nameImg.color = Mod.Config.FriendlyAlreadyActivated;
                    } else {
                        nameImg.color = Mod.Config.FriendlyUnactivated;
                    }

                }
            }
        }
    }

    [HarmonyPatch(typeof(CombatHUDPortrait), "IndicateFuturePhase")]
    [HarmonyPatch(new Type[] { })]
    public static class CombatHUDPortrait_IndicateFuturePhase {
        public static void Postfix(CombatHUDPortrait __instance, TextMeshProUGUI ___ioText) {
            __instance.NumberFlagFill.color = Color.white;
            __instance.NumberFlagOutline.color = Color.white;
            __instance.NumberFlagText.color = Color.white;
            ___ioText.color = Color.white;

            Transform frameT = __instance.FilledHolder.transform.Find("mw_Frame");
            if (frameT != null) {
                GameObject frame = frameT.gameObject;
                Transform nameRectT = frameT.transform.Find("mw_NameRect");
                if (nameRectT != null) {
                    GameObject nameRect = nameRectT.gameObject;
                    Image imgByCmp = nameRect.GetComponent<Image>();
                    imgByCmp.color = Mod.Config.FriendlyUnactivated;

                }
            }
        }
    }

    // === CombatHUDPhaseDisplay === : The floating badget next to a mech
    [HarmonyPatch(typeof(CombatHUDPhaseDisplay), "RefreshInfo")]
    [HarmonyPatch(new Type[] { })]
    public static class CombatHUDPhaseDisplay_RefreshInfo {
        public static void Postfix(CombatHUDPhaseDisplay __instance, ref TextMeshProUGUI ___NumText) {
            //SkillBasedInit.Logger.Log($"CombatHUDPhaseDisplay:RefreshInfo:post - Init");
            ___NumText.enableWordWrapping = false;
            ___NumText.fontSize = 18;
        }
    }

    [HarmonyPatch(typeof(CombatHUDPhaseDisplay), "IndicatePastPhase")]
    [HarmonyPatch(new Type[] { })]
    public static class CombatHUDPhaseDisplay_IndicatePastPhase {
        public static void Postfix(CombatHUDPhaseDisplay __instance) {
            //SkillBasedInit.Logger.LogIfDebug($"CombatHUDPhaseDisplay:IndicatePastPhase:post - init");
            __instance.FlagOutline.color = Color.white;
            __instance.NumText.color = Color.white;

            Hostility hostility = __instance.Combat.HostilityMatrix.GetHostility(__instance.DisplayedActor.team, __instance.Combat.LocalPlayerTeam);
            bool isPlayer = __instance.DisplayedActor.team == __instance.Combat.LocalPlayerTeam;

            Color color = Mod.Config.FriendlyAlreadyActivated;
            if (hostility == Hostility.ENEMY) {
                color = Mod.Config.EnemyAlreadyActivated;
            } else {
                if (!isPlayer) {
                    switch (hostility) {
                        case Hostility.FRIENDLY:
                            color = Mod.Config.AlliedAlreadyActivated;
                            break;
                        case Hostility.NEUTRAL:
                            color = Mod.Config.NeutralAlreadyActivated;
                            break;
                    }
                }
            }
            __instance.FlagFillImage.color = color;

        }
    }

    [HarmonyPatch(typeof(CombatHUDPhaseDisplay), "IndicateCurrentPhase")]
    [HarmonyPatch(new Type[] { typeof(bool), typeof(Hostility) })]
    public static class CombatHUDPhaseDisplay_IndicateCurrentPhase {
        public static void Postfix(CombatHUDPhaseDisplay __instance, bool isPlayer, Hostility hostility) {
            //SkillBasedInit.Logger.LogIfDebug($"CombatHUDPhaseDisplay:IndicateCurrentPhase:post - init");
            __instance.FlagOutline.color = Color.white;
            __instance.NumText.color = Color.white;

            Color color = Mod.Config.FriendlyUnactivated;
            if (hostility == Hostility.ENEMY) {
                color = Mod.Config.EnemyUnactivated;
            } else {
                if (!isPlayer) {
                    switch (hostility) {
                        case Hostility.FRIENDLY:
                            color = Mod.Config.AlliedUnactivated;
                            break;
                        case Hostility.NEUTRAL:
                            color = Mod.Config.NeutralUnactivated;
                            break;
                    }
                }
            }
            __instance.FlagFillImage.color = color;
        }
    }

    [HarmonyPatch(typeof(CombatHUDPhaseDisplay), "IndicateFuturePhase")]
    [HarmonyPatch(new Type[] { })]
    public static class CombatHUDPhaseDisplay_IndicateFuturePhase {
        public static void Postfix(CombatHUDPhaseDisplay __instance) {
            //SkillBasedInit.Logger.LogIfDebug($"CombatHUDPhaseDisplay:IndicateFuturePhase:post - init");

            __instance.FlagOutline.color = Color.white;
            __instance.NumText.color = Color.white;

            Hostility hostility = __instance.Combat.HostilityMatrix.GetHostility(__instance.DisplayedActor.team, __instance.Combat.LocalPlayerTeam);
            bool isPlayer = __instance.DisplayedActor.team == __instance.Combat.LocalPlayerTeam;

            Color color = Mod.Config.FriendlyUnactivated;
            if (hostility == Hostility.ENEMY) {
                color = Mod.Config.EnemyUnactivated;
            } else {
                if (!isPlayer) {
                    switch (hostility) {
                        case Hostility.FRIENDLY:
                            color = Mod.Config.AlliedUnactivated;
                            break;
                        case Hostility.NEUTRAL:
                            color = Mod.Config.NeutralUnactivated;
                            break;
                    }
                }
            }
            __instance.FlagFillImage.color = color;
        }
    }

}
