using EloBuddy;
using EloBuddy.SDK;
using Mario_s_Lib;
using static Mario_s_Template.Menus;
using static Mario_s_Template.SpellsManager;

namespace Mario_s_Template.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class Combo
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(SpellList.GetHighestRange(), DamageType.Mixed);

            Q.TryToCast(target, ComboMenu);
            W.TryToCast(target, ComboMenu);
            E.TryToCast(target, ComboMenu);
            R.TryToCast(target, ComboMenu);
        }
    }
}