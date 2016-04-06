using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using Mario_s_Lib.DataBases;

namespace Mario_s_Lib
{
    public static class Initializer
    {
        private static readonly Menu FirstMenu = MainMenu.AddMenu("Mario`s Lib Options", "marioalibb");
        public static Menu SettingsMenu = FirstMenu.AddSubMenu("• Danger Settings", "dangersettings");
        public static void InitiliazeDangerHandler()
        {

            DangerHandler.Init();

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