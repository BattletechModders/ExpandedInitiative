using System.Collections.Generic;
using UnityEngine;

namespace ExpandedInitiative {

    public static class ModIcons {
        public const string Stopwatch = "@ei_stopwatch";
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
            public int UnitLight = 3;
            public int UnitMedium = 5;
            public int UnitHeavy = 7;
            public int UnitNone = 9;
        }
        public TurretPhaseOpts TurretPhases = new TurretPhaseOpts();

        // Mech Bay
        public const string LT_TT_BASE = "TOOLTIP_BASE_INIT";
        public const string LT_TT_COMPONENT = "TOOLTIP_COMPONENTS";
        public const string LT_TT_LANCE = "TOOLTIP_LANCE";
        public const string LT_TT_PILOT = "TOOLTIP_PILOT";

        public const string LT_MB_TT_SUMMARY = "MECHBAY_TEXT_SUMMARY";
        public const string LT_LC_TT_SUMMARY = "LANCE_TEXT_SUMMARY";

        public Dictionary<string, string> LocalizedText = new Dictionary<string, string>() {
            { LT_TT_BASE, "  Base: {0}" },
            { LT_TT_COMPONENT, "  Components: <color=#{0}>{1:+0;-#}</color>" },
            { LT_TT_LANCE, "  Lance: <color=#{0}>{1:+0;-#}</color>" },
            { LT_TT_PILOT, "  Pilot: <color=#{0}>{1:+0;-#}</color>" },
            { LT_MB_TT_SUMMARY, "<b>Initiative Modifiers</b>\n{0}\n<b>TOTAL</b>: {1}" },
            { LT_LC_TT_SUMMARY, "<b>Initiative Modifiers</b>\n{0}\n<b>TOTAL</b>: {1}" }
        };

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
