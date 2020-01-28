using BattleTech;
using ExpandedInitiative.Helper;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using us.frostraptor.modUtils;

namespace ExpandedInitiative.patches {

    [HarmonyPatch(typeof(Mech), "InitStats")]
    public static class Mech_InitStats {
        public static void Postfix(Mech __instance) {
            int init = __instance.InitFromTonnage();
            Mod.Log.Debug($"Setting baseInit for actor: {CombatantUtils.Label(__instance)} to {init}");
            __instance.StatCollection.Set<int>(ModStats.BaseInitiative, init);
        }
    }

    [HarmonyPatch(typeof(Vehicle), "InitStats")]
    public static class Vehicle_InitStats {
        public static void Postfix(Vehicle __instance) {
            int init = __instance.InitFromTonnage();
            Mod.Log.Debug($"Setting baseInit for actor: {CombatantUtils.Label(__instance)} to {init}");
            __instance.StatCollection.Set<int>(ModStats.BaseInitiative, init);
        }
    }
}
