using BattleTech;
using Harmony;
using IRBTModUtils.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedInitiative.patches
{
    [HarmonyPatch(typeof(Team), "AddUnit")]
    public static class Team_AddUnit
    {
        public static void Postfix(Team __instance, AbstractActor unit)
        {

            if (__instance.Combat.TurnDirector.CurrentRound > 1)
            {
                if (__instance.Combat.TurnDirector.IsInterleaved)
                {
                    // We are spawning reinforcements. Force them to the last init phase
                    Mod.Log.Info?.Write($"Forcing freshly spawned unit: {unit.DistinctId()} to last phase.");
                    unit.ForceUnitToLastPhase();
                }
                else
                {
                    Mod.Log.Info?.Write($"Non-interleaved mode, allowing spawned unit: {unit.DistinctId()} to use normal init.");
                }

            }
        }
    }
}
