using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using Mario_s_Lux.Modes;

namespace Mario_s_Lux
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Lux") return;
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();
            EManager.InitializeEManager();
        }
    }
}
