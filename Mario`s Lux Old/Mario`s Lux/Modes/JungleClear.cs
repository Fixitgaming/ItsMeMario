using static Mario_s_Lux.SpellsManager;
using static Mario_s_Lux.Menus;

namespace Mario_s_Lux.Modes
{
    internal class JungleClear
    {
        public static void Execute()
        {
            Q.TryToCast(Q.GetJungleMinion(), JungleClearMenu);
            E.TryToCast(E.GetJungleMinion(), JungleClearMenu);
        }
    }
}
