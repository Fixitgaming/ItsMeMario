using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using Mario_s_Lib.DataBases;

namespace Mario_s_Lib
{
    public static class Initializer
    {
        public static Menu SettingsMenu;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">Your first menu</param>
        public static void InitiliazeDangerHandler(this Menu m)
        {
            DangerHandler.Init();

            SettingsMenu = m.AddSubMenu("• Danger Settings", "dangersettings");
            SettingsMenu.AddGroupLabel("Danger Options");
            SettingsMenu.CreateSlider("Add [{0}%] to the item HP% slider if the spell is dangerous", "dangerSlider", 10, 10, 50);
            SettingsMenu.AddLabel("Dont mess with the options if you dont know what they do");
            SettingsMenu.CreateSlider("Extra range to be safe of a skillshot", "saferange", 110, 80, 180);

            //Spells Menu
            SettingsMenu.AddGroupLabel("Spells to consider");
            SettingsMenu.AddLabel("Disable/Enable dangerous spells");
            foreach (var s in EntityManager.Heroes.Enemies.SelectMany(e => DangerousSpells.Spells.Where(s => s.Hero == e.Hero)))
            {
                SettingsMenu.CreateCheckBox(s.Hero + "`s " + s.Slot, "dangSpell" + s.Hero + s.Slot);
            }
        }
    }
}