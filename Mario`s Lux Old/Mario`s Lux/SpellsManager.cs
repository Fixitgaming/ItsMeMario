using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Mario_s_Lux
{
    public static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1290, SkillShotType.Linear, 250, 1200, 70)
            {
                AllowedCollisionCount = 1
            };
            W = new Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear, 0, 1400, 85)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Circular, 250, 1400, 335)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Skillshot(SpellSlot.R, 3100, SkillShotType.Circular, 1000, int.MaxValue, 110)
            {
                AllowedCollisionCount = int.MaxValue
            };
        }

        #region Damages

        public static float GetDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            const DamageType damageType = DamageType.Magical;
            var AD = Player.Instance.FlatPhysicalDamageMod;
            var AP = Player.Instance.FlatMagicDamageMod;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                    {
                        dmg += new float[] {60, 110, 160, 210, 260}[sLevel] + 0.7f*AP;
                    }
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                    {
                        dmg += new float[] {0, 0, 0, 0, 0}[sLevel] + 0f*AD;
                    }
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                    {
                        dmg += new float[] {60, 105, 150, 195, 240}[sLevel] + 0.6f*AP;
                    }
                    break;
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        dmg += new float[] {300, 400, 500}[sLevel] + 0.75f*AP;
                    }
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }

        public static float GetTotalDamage(this Obj_AI_Base target)
        {
            var dmg =
                Player.Spells.Where(
                    s => (s.Slot == SpellSlot.Q) || (s.Slot == SpellSlot.W) || (s.Slot == SpellSlot.E) || (s.Slot == SpellSlot.R) && s.IsReady)
                    .Sum(s => target.GetDamage(s.Slot));

            return dmg + Player.Instance.GetAutoAttackDamage(target);
        }

        public static float GetPassiveDamage(this Obj_AI_Base target)
        {
            var rawDamage = new float[] {18, 26, 34, 42, 50, 58, 66, 74, 82, 90, 98, 106, 114, 122, 130, 138, 146, 154}[Player.Instance.Level] +
                            0.2f*Player.Instance.FlatMagicDamageMod;

            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, rawDamage);
        }

        public static bool HasPassive(this Obj_AI_Base target)
        {
            return target.HasBuff("luxilluminatingfraulein");
        }

        #endregion Damages
    }
}
