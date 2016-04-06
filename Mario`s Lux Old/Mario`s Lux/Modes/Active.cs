using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using static Mario_s_Lux.EManager;
using static Mario_s_Lux.SpellsManager;
using static Mario_s_Lux.Menus;

namespace Mario_s_Lux.Modes
{
    internal class Active
    {
        public static void Execute()
        {
            var orbMode = Orbwalker.ActiveModesFlags;

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (GetE != null && GetE.CountEnemiesInRange(350) >= 1 && Player.GetSpell(SpellSlot.E).ToggleState >= 1)
                {
                    var enemy =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            e => e.IsInRange(GetE, 320) && e.IsInAutoAttackRange(Player.Instance) && e.HasPassive());
                    if (enemy == null)
                    {
                        E.Cast(Player.Instance);
                    }
                }
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                if (GetE != null && GetE.CountEnemiesInRange(350) >= 1 && Player.GetSpell(SpellSlot.E).ToggleState >= 1)
                {
                    E.Cast(Player.Instance); 
                }
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                LaneClear.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                LastHit.Execute();
            }

            if (KillStealMenu.GetCheckBoxValue("rUse"))
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                   
                if(target == null)return;

                if (R.IsReady())
                {
                    var passiveDamage = target.HasPassive() ? target.GetPassiveDamage() : 0f;
                    var rDamage = target.GetDamage(SpellSlot.R) + passiveDamage;

                    var predictedHealth = Prediction.Health.GetPrediction(target, R.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        var pred = R.GetPrediction(target);
                        if (pred.HitChancePercent >= 90)
                        {
                            R.Cast(target);
                        }
                    }
                }
            }
        }
    }
}
