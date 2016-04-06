using EloBuddy;
using EloBuddy.SDK;
using static Mario_s_Lux.SpellsManager;
using static Mario_s_Lux.Menus;

namespace Mario_s_Lux.Modes
{
    internal class Combo
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Magical);

            if (target == null) return;

            Q.TryToCast(target, ComboMenu, 90);

            if (Player.GetSpell(SpellSlot.E).ToggleState <= 0 && ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() )
            {
                if (Player.Instance.CountEnemiesInRange(E.Range) <= 2)
                {
                    E.TryToCast(target, ComboMenu, 85);
                }
                else
                {
                    E.Cast(E.GetBestCircularCastPosition(2));
                }
            }

            if (!ComboMenu.GetCheckBoxValue("smartCombo")) return;

            if (target.HasBuffOfType(BuffType.Snare) || target.HasBuffOfType(BuffType.Stun))
            {
                if (R.IsReady() && !E.IsReady())
                {
                    var passiveDamage = target.HasPassive() ? target.GetPassiveDamage() : 0f;
                    var rDamage = target.GetDamage(SpellSlot.R) + passiveDamage;

                    var predictedHealth = Prediction.Health.GetPrediction(target, R.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        R.Cast(target);
                    }
                }
                if (E.IsReady() && R.IsReady())
                {
                    var passiveDamage = target.HasPassive() ? target.GetPassiveDamage() : 0f;
                    var totalDamage = target.GetDamage(SpellSlot.E) + target.GetDamage(SpellSlot.R) + passiveDamage;

                    var predictedHealth = Prediction.Health.GetPrediction(target, R.CastDelay + Game.Ping);

                    if (predictedHealth <= totalDamage)
                    {
                        E.Cast(target);
                        R.Cast(target);
                    }
                }
            }
        }
    }
}
