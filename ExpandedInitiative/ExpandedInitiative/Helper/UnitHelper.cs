namespace ExpandedInitiative
{
    public static class UnitHelper
    {

        // Calculate the initiative modifiers from all components based upon a MechDef. For whatever reason they 
        //  reverse the modifier right out of the gate, such that these values are positives automatically
        public static int GetNormalizedComponentModifier(MechDef mechDef)
        {
            int unitInit = 0;
            if (mechDef.Inventory != null)
            {
                MechComponentRef[] inventory = mechDef.Inventory;
                foreach (MechComponentRef mechComponentRef in inventory)
                {
                    if (mechComponentRef.Def != null && mechComponentRef.Def.statusEffects != null)
                    {
                        EffectData[] statusEffects = mechComponentRef.Def.statusEffects;
                        foreach (EffectData effect in statusEffects)
                        {
                            if (MechStatisticsRules.GetInitiativeModifierFromEffectData(effect, true, null) == 0)
                            {
                                unitInit += MechStatisticsRules.GetInitiativeModifierFromEffectData(effect, false, null);
                            }
                        }
                    }
                }
            }

            Mod.Log.Debug?.Write($"Normalized BaseInit for mechDef:{mechDef.Name} is unitInit:{unitInit}");
            return unitInit;
        }
    }
}
