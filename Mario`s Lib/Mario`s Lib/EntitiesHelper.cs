using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Mario_s_Lib
{
    public static class EntitiesHelper
    {
        public static Obj_AI_Base GetJungleMinion(this Spell.SpellBase spell)
        {
            return
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(spell.Range));
        }

        public static int CountEnemyLaneMinions(this Obj_AI_Base target, float range = 100)
        {
            return EntityManager.MinionsAndMonsters.GetLaneMinions().Count(m => m.Distance(target) <= range && m.IsEnemy);
        }

        public static int CountAllyLaneMinions(this Obj_AI_Base target, float range = 100)
        {
            return EntityManager.MinionsAndMonsters.GetLaneMinions().Count(m => m.Distance(target) <= range && m.IsAlly);
        }

        public static int CountJungleMinions(this Obj_AI_Base target, float range = 100)
        {
            return EntityManager.MinionsAndMonsters.GetJungleMonsters().Count(m => m.Distance(target) <= range);
        }

        /// <summary>
        ///     Get nearest ally
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static AIHeroClient GetNearestAlly(float range = 700)
        {
            return EntityManager.Heroes.Allies.OrderBy(a => a.Distance(Player.Instance))
                .FirstOrDefault(ally => ally.IsInRange(Player.Instance, range));
        }

        /// <summary>
        ///     Get the nearest and lowest health ally
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static AIHeroClient GetNearestLowestAlly(float range = 700)
        {
            return
                EntityManager.Heroes.Allies.OrderBy(a => a.Distance(Player.Instance))
                    .ThenBy(a => a.Health)
                    .FirstOrDefault(ally => ally.IsInRange(Player.Instance, range));
        }

        public static AIHeroClient GetThebestTarget(this Spell.SpellBase spell)
        {
            return
                EntityManager.Heroes.Enemies.OrderBy(e => e.Health)
                    .ThenByDescending(TargetSelector.GetPriority)
                    .ThenBy(e => e.FlatArmorMod)
                    .ThenBy(e => e.FlatMagicReduction)
                    .FirstOrDefault(e => e.IsValidTarget(spell.Range) && !e.HasUndyingBuff());
        }

    }
}