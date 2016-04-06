using static Mario_s_Lux.SpellsManager;
using static Mario_s_Lux.Menus;

namespace Mario_s_Lux.Modes
{
    internal class LaneClear
    {
        public static void Execute()
        {
            Q.TryToCast(Q.GetLastHitMinion(), LaneClearMenu);

            if (E.IsReady())
            {
                E.Cast(E.GetBestCircularFarmPosition(4));
            }
        }
    }
}
