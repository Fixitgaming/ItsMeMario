using static Mario_s_Lux.SpellsManager;
using static Mario_s_Lux.Menus;

namespace Mario_s_Lux.Modes
{
    internal class LastHit
    {
        public static void Execute()
        {
            Q.TryToCast(Q.GetLastHitMinion(), LasthitMenu);
            E.TryToCast(E.GetLastHitMinion(), LasthitMenu);
        }
    }
}
