using System.Collections.Generic;
using UnityEngine;

namespace ExpandedInitiative {

    public static class ModIcons {
        public const string Stopwatch = "@sbi_stopwatch";
    }

    public static class ModStats {
        public const string BaseInitiative = "BaseInitiative";
    }

    public class ModConfig {
        // If true, extra logging will be printed
        public bool Debug = false;

        // If true, all the logs will be printed
        public bool Trace = false;

        public class TurretPhaseOpts {
            int UnitLight = 7;
            int UnitMedium = 5;
            int UnitHeavy = 3;
            int UnitNone = 1;
        }
        public TurretPhaseOpts TurretPhases = new TurretPhaseOpts();

    //    public const string LT_TT_TITLE = "TOOLTIP_TITLE_INITIATIVE";
    //    public const string LT_TT_MECH_TONNAGE = "TOOLTIP_MECH_TONNAGE";
    //    public const string LT_TT_COMPONENTS = "TOOLTIP_COMPONENTS";
    //    public const string LT_TT_ENGINES = "TOOLTIP_ENGINES";
    //    public const string LT_TT_LEG_DESTROYED = "TOOLTIP_LEG_DESTROYED";
    //    public const string LT_TT_PRONE = "TOOLTIP_PRONE";
    //    public const string LT_TT_SHUTDOWN = "TOOLTIP_SHUTDOWN";
    //    public const string LT_TT_MELEE_IMPACT = "TOOLTIP_MELEE_IMPACT";
    //    public const string LT_TT_TACTICS = "TOOLTIP_TACTICS";
    //    public const string LT_TT_INSPIRED = "TOOLTIP_INSPIRED";
    //    public const string LT_TT_FRESH_INJURY = "TOOLTIP_FRESH_INJURY";
    //    public const string LT_TT_PAINFUL_INJURY = "TOOLTIP_PAINFUL_INJURY";
    //    public const string LT_TT_HESITATION = "TOOLTIP_HESITATION";
    //    public const string LT_TT_CALLED_SHOT_TARG = "TOOLTIP_CALLED_SHOT_TARGET";
    //    public const string LT_TT_VIGILANCE = "TOOLTIP_VIGILANCE";
    //    public const string LT_TT_RANDOM = "TOOLTIP_RANDOM";
    //    public const string LT_TT_EXPECTED = "TOOLTIP_EXPECTED";
    //    public const string LT_TT_HOVER = "TOOLTIP_HOVER";

    //    // Floaties
    //    public const string LT_FT_INJURY_NOW = "FLOATIE_INJURY_NOW";
    //    public const string LT_FT_INJURY_LATER = "FLOATIE_INJURY_LATER";
    //    public const string LT_FT_KNOCKDOWN_NOW = "FLOATIE_KNOCKDOWN_NOW";
    //    public const string LT_FT_KNOCKDOWN_LATER = "FLOATIE_KNOCKDOWN_LATER";
    //    public const string LT_FT_CALLED_SHOT_NOW = "FLOATIE_CALLED_SHOT_NOW";
    //    public const string LT_FT_CALLED_SHOT_LATER = "FLOATIE_CALLED_SHOT_LATER";
    //    public const string LT_FT_MELEE_IMPACT_NOW = "FLOATIE_MELEE_IMPACT_NOW";
    //    public const string LT_FT_MELEE_IMPACT_LATER = "FLOATIE_MELEE_IMPACT_LATER";
    //    public const string LT_FT_VIGILANCE = "FLOATIE_VIGILANCE";

    //    // Mech Bay
    //    public const string LT_MB_TONNAGE = "MECHBAY_TONNAGE";
    //    public const string LT_MB_COMPONENTS = "MECHBAY_COMPONENTS";
    //    public const string LT_MB_ENGINES = "MECHBAY_ENGINES";
    //    public const string LT_MB_LANCE = "MECHBAY_LANCE";
    //    public const string LT_MB_TACTICS = "MECHBAY_TACTICS";
    //    public const string LT_MB_TOTAL = "MECHBAY_TOTAL";
    //    public const string LT_MB_RANDOM = "MECHBAY_RANDOM";
    //    public const string LT_MB_EXPECTED_NO_PILOT = "MECHBAY_EXPECTED_NO_PILOT";
    //    public const string LT_MB_EXPECTED = "MECHBAY_EXPECTED";

    //    public Dictionary<string, string> LocalizedText = new Dictionary<string, string>() {
    //        // Tooltip
    //        { LT_TT_TITLE, "INITIATIVE" },
    //        { LT_TT_MECH_TONNAGE, "MECH => tonnage: {0}" },
    //        { LT_TT_COMPONENTS, "<color=#{0}>{1:+0} components</color>" },
    //        { LT_TT_ENGINES, "<color=#{0}>{1:+0} engine</color>" },
    //        { LT_TT_LEG_DESTROYED, "<color=#FF0000>{0} Leg Destroyed</color>" },
    //        { LT_TT_PRONE, "<color=#FF0000>{0} Prone</color>" },
    //        { LT_TT_SHUTDOWN, "<color=#FF0000>{0} Shutdown</color>" },
    //        { LT_TT_MELEE_IMPACT, "<color=#FF0000>-{0} Melee Impact</color>" },
    //        { LT_TT_TACTICS, "PILOT => Tactics: <color=#00FF00>{0:+0}</color>" },
    //        { LT_TT_INSPIRED, "<color=#00FF00>+1 to +3 Inspired</color>" },
    //        { LT_TT_FRESH_INJURY, "<color=#FF0000>-{0} Fresh Injury</color>" },
    //        { LT_TT_PAINFUL_INJURY, "<color=#FF0000>-{0} Painful Injury</color>" },
    //        { LT_TT_HESITATION, "<color=#FF0000>-{0} Hesitation</color>" },
    //        { LT_TT_CALLED_SHOT_TARG, "<color=#FF0000>-{0} Called Shot Target</color>" },
    //        { LT_TT_VIGILANCE, "<color=#00FF00>-{0} Vigilance</color>" },
    //        { LT_TT_RANDOM, "\nRANDOM => <color=#FF0000>-{0} to -{1}</color> <i>(reduced by tactics)</i>" },
    //        { LT_TT_EXPECTED, "EXPECTED PHASE: <b>{0} to {1}</b>" },
    //        { LT_TT_HOVER, "<i>Hover initiative in Mechlab & Deploy for details.</i>" },

    //        // Floaties
    //        { LT_FT_INJURY_LATER, "OUCH! -{0} INITIATIVE" },
    //        { LT_FT_INJURY_NOW, "OUCH! -{0} INITIATIVE NEXT ROUND" },
    //        { LT_FT_KNOCKDOWN_NOW, "GOING DOWN! -{0} INITIATIVE" },
    //        { LT_FT_KNOCKDOWN_LATER, "GOING DOWN! -{0} INITIATIVE NEXT ROUND" },
    //        { LT_FT_CALLED_SHOT_NOW, "CALLED SHOT! -{0} INITIATIVE" },
    //        { LT_FT_CALLED_SHOT_LATER, "CALLED SHOT! -{0} INITIATIVE NEXT ROUND" },
    //        { LT_FT_MELEE_IMPACT_NOW, "CLANG! -{0} INITIATIVE NEXT ROUND" },
    //        { LT_FT_MELEE_IMPACT_LATER, "CLANG! -{0} INITIATIVE NEXT ROUND" },
    //        { LT_FT_VIGILANCE, "VIGILANCE! +{0} INITIATIVE NEXT ROUND!" },

    //        // Mech Bay
    //        { LT_MB_TONNAGE, "<b>BASE</b>: {0} (<i>from tonnage</i>)" },
    //        { LT_MB_COMPONENTS, "<space=2em><color=#{0}>{1:+0;-#} components</color>" },
    //        { LT_MB_ENGINES, "<space=2em><color=#{0}>{1:+0;-#} engine</color>" },
    //        { LT_MB_LANCE, "<space=2em><color=#{0}>{1:+0;-#} lance</color>" },
    //        { LT_MB_TACTICS, "<space=2em>{0:+0;-#} tactics" },
    //        { LT_MB_TOTAL, "<b>TOTAL</b>: {0}" },
    //        { LT_MB_RANDOM, "<space=2em><color=#FF0000>-{0} to -{1} random</color> (<i>reduced by tactics</i>)" },
    //        { LT_MB_EXPECTED_NO_PILOT, "<b>EXPECTED PHASE</b>: {0}" },
    //        { LT_MB_EXPECTED, "<b>EXPECTED PHASE</b>: {0} to {1}" },
    //};

        // Colors for the UI elements
        /* No affiliation
         default color is: UILookAndColorConstants.PhaseCurrentFill.color
            RGBA(0.843, 0.843, 0.843, 1.000) -> 215, 215, 215 (gray)
            still to activate: 59, 177, 67 -> 0.231, 0.694, 0.262
            already activated: 11, 102, 35 -> 0.043, 0.4, 0.137
        */
        public float[] ColorFriendlyUnactivated = new float[] { 0.231f, 0.694f, 0.262f, 1.0f };
        public float[] ColorFriendlyAlreadyActivated = new float[] { 0.043f, 0.4f, 0.137f, 1.0f };
        public Color FriendlyUnactivated;
        public Color FriendlyAlreadyActivated;

        /* Allied
         default color is: UILookAndColorConstants.AlliedUI.color            
            RGBA(0.522, 0.859, 0.965, 1.000) -> 133, 219, 246 (light blue)
            still to activate: 133, 219, 246 -> 0.521, 0.858, 0.964
            already activated: 88, 139, 174 -> 0.345, 0.545, 0.682
        */
        public float[] ColorAlliedUnactivated = new float[] { 0.521f, 0.858f, 0.964f, 1.0f };
        public float[] ColorAlliedAlreadyActivated = new float[] { 0.345f, 0.545f, 0.682f, 1.0f };
        public Color AlliedUnactivated;
        public Color AlliedAlreadyActivated;

        /* Neutral
         default color is: UILookAndColorConstants.NeutralUI.color            
            RGBA(0.631, 0.631, 0.631, 1.000) -> 161, 161, 161 (gray)
            still to activate: 217, 221, 220 -> 0.850, 0.866, 0.862
            already activated: 119, 123, 126 -> 0.466, 0.482, 0.494    
        */
        public float[] ColorNeutralUnactivated = new float[] { 0.850f, 0.866f, 0.862f, 1.0f };
        public float[] ColorNeutralAlreadyActivated = new float[] { 0.466f, 0.482f, 0.494f, 1.0f };
        public Color NeutralUnactivated;
        public Color NeutralAlreadyActivated;

        /* Enemy
         default color is: UILookAndColorConstants.EnemyUI.color            
            RGBA(0.941, 0.259, 0.157, 1.000) -> 240, 66, 40 (light red)
            still to activate: 255, 8, 0 -> 1, 0.031, 0
            already activated: 124, 10, 2 -> 0.486, 0.039, 0.007
        */
        public float[] ColorEnemyUnactivated = new float[] { 1.0f, 0.031f, 0f, 1.0f };
        public float[] ColorEnemyAlreadyActivated = new float[] { 0.486f, 0.039f, 0.007f, 1.0f };
        public Color EnemyUnactivated;
        public Color EnemyAlreadyActivated;

        public void InitializeColors() {
            FriendlyUnactivated = new Color(ColorFriendlyUnactivated[0], ColorFriendlyUnactivated[1], ColorFriendlyUnactivated[2], ColorFriendlyUnactivated[3]);
            FriendlyAlreadyActivated = new Color(ColorFriendlyAlreadyActivated[0], ColorFriendlyAlreadyActivated[1], ColorFriendlyAlreadyActivated[2], ColorFriendlyAlreadyActivated[3]);

            AlliedUnactivated = new Color(ColorAlliedUnactivated[0], ColorAlliedUnactivated[1], ColorAlliedUnactivated[2], ColorAlliedUnactivated[3]);
            AlliedAlreadyActivated = new Color(ColorAlliedAlreadyActivated[0], ColorAlliedAlreadyActivated[1], ColorAlliedAlreadyActivated[2], ColorAlliedAlreadyActivated[3]);

            NeutralUnactivated = new Color(ColorNeutralUnactivated[0], ColorNeutralUnactivated[1], ColorNeutralUnactivated[2], ColorNeutralUnactivated[3]);
            NeutralAlreadyActivated = new Color(ColorNeutralAlreadyActivated[0], ColorNeutralAlreadyActivated[1], ColorNeutralAlreadyActivated[2], ColorNeutralAlreadyActivated[3]);

            EnemyUnactivated = new Color(ColorEnemyUnactivated[0], ColorEnemyUnactivated[1], ColorEnemyUnactivated[2], ColorEnemyUnactivated[3]);
            EnemyAlreadyActivated = new Color(ColorEnemyAlreadyActivated[0], ColorEnemyAlreadyActivated[1], ColorEnemyAlreadyActivated[2], ColorEnemyAlreadyActivated[3]);
        }

        public void LogConfig() {
            Mod.Log.Info("=== MOD CONFIG BEGIN ===");
            Mod.Log.Info($"  DEBUG:{this.Debug} Trace:{this.Trace}");

            // TODO: FIXME
            //Mod.Log.Info($"  Turret Tonnage -> Light:{TurretTonnageTagUnitLight} Medium:{TurretTonnageTagUnitMedium} Heavy:{TurretTonnageTagUnitHeavy} None:{TurretTonnageTagUnitNone}");

            Mod.Log.Info("=== MOD CONFIG END ===");
        }
    }
}
