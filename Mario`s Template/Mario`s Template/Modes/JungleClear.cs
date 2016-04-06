using Mario_s_Lib;
using static Mario_s_Template.Menus;
using static Mario_s_Template.SpellsManager;

namespace Mario_s_Template.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class JungleClear
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
            Q.TryToCast(Q.GetJungleMinion(), JungleClearMenu);
            W.TryToCast(Q.GetJungleMinion(), JungleClearMenu);
            E.TryToCast(Q.GetJungleMinion(), JungleClearMenu);
            R.TryToCast(Q.GetJungleMinion(), JungleClearMenu);
        }
    }
}