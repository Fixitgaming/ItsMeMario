using EloBuddy;
using EloBuddy.SDK;
using static Mario_s_Lux.SpellsManager;

namespace Mario_s_Lux.Modes
{
    internal class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);

            if (target == null) return;

            Q.TryToCast(target, Menus.HarassMenu, 85);
            E.TryToCast(target, Menus.HarassMenu, 85);
        }
    }
}
