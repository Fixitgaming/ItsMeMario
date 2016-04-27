using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Spells;
using SharpDX;

namespace Mario_s_Lib
{
    public static class Spells
    {
        public static bool CanCastSpell(this Obj_AI_Base target, Spell.SpellBase spell)
        {
            if (spell == null || target == null) return false;
            return target.IsValidTarget(spell.Range) && spell.IsReady();
        }

        public static bool CanCastSpell(this Vector3 target, Spell.SpellBase spell)
        {
            if (spell == null || target == null) return false;
            return target.IsInRange(Player.Instance, spell.Range) && spell.IsReady();
        }

        public static bool CanCastSpell(this Obj_AI_Base target, Spell.Active spell)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCastSpell(asBase);
        }

        public static bool CanCastSpell(this Obj_AI_Base target, Spell.Skillshot spell, int hitchancePercent = 75)
        {
            var asBase = spell as Spell.SpellBase;
            var pred = spell.GetPrediction(target);
            return target.CanCastSpell(asBase) && pred.HitChancePercent >= 75;
        }

        public static bool CanCastSpell(this Obj_AI_Base target, Spell.Chargeable spell)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCastSpell(asBase);
        }

        public static bool CanCastSpell(this Obj_AI_Base target, Spell.Ranged spell)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCastSpell(asBase);
        }

        public static bool CanCastSpell(this Obj_AI_Base target, Spell.Targeted spell)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCastSpell(asBase);
        }

        public static bool TryToCast(this Spell.SpellBase spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.SpellBase spell, Vector3 pos, Menu m)
        {
            if (pos == Vector3.Zero) return false;
            return pos.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast(pos);
        }

        public static bool TryToCast(this Spell.Active spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast();
        }

        public static bool TryToCast(this Spell.Skillshot spell, Obj_AI_Base target, Menu m, int percent = 75)
        {
            if (target == null) return false;
            return target.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Targeted spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Chargeable spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Ranged spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCastSpell(spell) && m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") && spell.Cast(target);
        }

        public static Spell.Skillshot GetSkillShotData(this SpellSlot slot, SkillShotType skillType)
        {
            var spellData = SpellDatabase.GetSpellInfoList(Player.Instance).FirstOrDefault(s => s.Slot == slot);
            if (spellData != null)
            {
                return new Spell.Skillshot(slot, (uint)spellData.Range, skillType, (int)(1000 * spellData.Delay), (int)spellData.MissileSpeed, (int)spellData.Radius)
                {
                    AllowedCollisionCount = slot.GetCollisionCount()
                };
            }
            return null;
        }

        private static int GetCollisionCount(this SpellSlot slot)
        {
            var getDBValue = DataBases.CollisionCount.CollisionCountDB.FirstOrDefault(v => v.Slot == slot && v.Champion == Player.Instance.Hero);
            return getDBValue?.Count ?? 0;
        }

        public static float GetSmallestRange(this List<Spell.SpellBase> spells)
        {
            var spell = spells.OrderBy(s => s.Range).FirstOrDefault();
            return spell?.Range ?? 0f;
        }

        public static float GetHighestRange(this List<Spell.SpellBase> spells)
        {
            var spell = spells.OrderByDescending(s => s.Range).FirstOrDefault();
            return spell?.Range ?? 0f;
        }
    }
}