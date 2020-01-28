using BattleTech;

namespace ExpandedInitiative {
    public static class ModState {

        public static CombatGameState Combat;
        public static void Reset() {
            // Reinitialize state
            Combat = null;
        }
    }
}
