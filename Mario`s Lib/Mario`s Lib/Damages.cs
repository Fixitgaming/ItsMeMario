using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Mario_s_Lib
{
    public static class Damages
    {
        public static float GetAlliesDamagesNear(this Obj_AI_Base target, float percent = 0.7f, int range = 700, int delay = 250)
        {
            var dmg = 0f;
            var slots = new[] {SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R};

            foreach (var a in EntityManager.Heroes.Allies.Where(a => a.IsInRange(target, range)))
            {
                dmg += a.GetAutoAttackDamage(target);
                dmg += a.Spellbook.Spells.Where(s => slots.Contains(s.Slot) && s.IsReady).Sum(s => a.GetSpellDamage(target, s.Slot));
            }
            return dmg*percent;
        }

        public static float GetEnemiesDamagesNear(this Obj_AI_Base target, float percent = 0.7f, int range = 700, int delay = 250)
        {
            var dmg = 0f;
            var slots = new[] {SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R};

            foreach (var a in EntityManager.Heroes.Allies.Where(a => a.IsInRange(target, range)))
            {
                dmg += a.GetAutoAttackDamage(target);
                dmg += a.Spellbook.Spells.Where(s => slots.Contains(s.Slot) && s.IsReady).Sum(s => a.GetSpellDamage(target, s.Slot));
            }
            return dmg*percent;
        }

        public static float GetTotalDamageEBDB(this Obj_AI_Base target)
        {
            var slots = new[] {SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R};
            var dmg =
                Player.Instance.Spellbook.Spells.Where(s => s.IsReady && slots.Contains(s.Slot))
                    .Sum(s => Player.Instance.GetSpellDamage(target, s.Slot));
            var aaDmg = Orbwalker.CanAutoAttack ? Player.Instance.GetAutoAttackDamage(target) : 0f;
            return dmg + aaDmg;
        }

        public static float GetEchoLudenDamage(this Obj_AI_Base target)
        {
            var dmg = 0f;
            var echo = new Item(ItemId.Ludens_Echo);

            if (echo.IsOwned() && Player.GetBuff("itemmagicshankcharge").Count == 100)
            {
                dmg += Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, (float) (100 + 0.1*Player.Instance.FlatMagicDamageMod));
            }
            return dmg;
        }

        public static float GetSheenDamage(this Obj_AI_Base target)
        {
            var sheenItems = new List<Item>
            {
                new Item(ItemId.Lich_Bane),
                new Item(ItemId.Trinity_Force),
                new Item(ItemId.Iceborn_Gauntlet),
                new Item(ItemId.Sheen)
            };
            var item = sheenItems.FirstOrDefault(i => i.IsReady() && i.IsOwned());
            if (item != null)
            {
                var AD = Player.Instance.FlatPhysicalDamageMod;
                var AP = Player.Instance.FlatMagicDamageMod;
                switch (item.Id)
                {
                    case ItemId.Lich_Bane:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, AD*0.75f + AP*0.5f);
                    case ItemId.Trinity_Force:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, AD*2f);
                    case ItemId.Iceborn_Gauntlet:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, AD*1.25f);
                    case ItemId.Sheen:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, AD*1f);
                }
            }

            return 0f;
        }
    }
}