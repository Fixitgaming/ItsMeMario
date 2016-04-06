using System.Collections.Generic;
using EloBuddy;

namespace Mario_s_Lib.DataBases
{
    public class ProtectSpell
    {
        public Champion Champ;
        public SpellSlot Slot;

        public ProtectSpell(Champion champ, SpellSlot slot)
        {
            Champ = champ;
            Slot = slot;
        }
    }

    public static class ProtectSpells
    {
        public static List<ProtectSpell> Spells = new List<ProtectSpell>
        {
            new ProtectSpell(Champion.Alistar, SpellSlot.E),
            new ProtectSpell(Champion.Bard, SpellSlot.W),
            new ProtectSpell(Champion.Janna, SpellSlot.E),
            new ProtectSpell(Champion.Karma, SpellSlot.E),
            new ProtectSpell(Champion.Kayle, SpellSlot.W),
            new ProtectSpell(Champion.LeeSin, SpellSlot.W),
            new ProtectSpell(Champion.Lulu, SpellSlot.E),
            new ProtectSpell(Champion.Lux, SpellSlot.W),
            new ProtectSpell(Champion.Morgana, SpellSlot.E),
            new ProtectSpell(Champion.Nami, SpellSlot.W),
            new ProtectSpell(Champion.Nidalee, SpellSlot.E),
            new ProtectSpell(Champion.Orianna, SpellSlot.E),
            new ProtectSpell(Champion.Sona, SpellSlot.W),
            new ProtectSpell(Champion.Soraka, SpellSlot.W),
            new ProtectSpell(Champion.TahmKench, SpellSlot.W),
            new ProtectSpell(Champion.Taric, SpellSlot.Q),
            new ProtectSpell(Champion.Thresh, SpellSlot.W),
            new ProtectSpell(Champion.Zilean, SpellSlot.E),
            new ProtectSpell(Champion.Shen, SpellSlot.R)
        };
    }
}