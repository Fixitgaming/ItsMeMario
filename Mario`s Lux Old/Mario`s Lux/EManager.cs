using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;

namespace Mario_s_Lux
{
    internal class EManager
    {
        public static List<Obj_GeneralParticleEmitter> EObjets = new List<Obj_GeneralParticleEmitter>();

        public static Obj_GeneralParticleEmitter GetE => EObjets.FirstOrDefault();
         
        public static void InitializeEManager()
        {
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        public static string TeamName()
        {
            switch (Player.Instance.Team)
            {
                case GameObjectTeam.Order:
                    return "green";
                case GameObjectTeam.Chaos:
                    return "red";
            }
            return null;
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var test = sender as Obj_GeneralParticleEmitter;
            if (test != null && test.Name.ToLower().Contains("lux_base_e_tar_aoe_"))
            {
                EObjets.Add(test);
            }
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var test = sender as Obj_GeneralParticleEmitter;
            if (test != null && test.Name.ToLower().Contains("lux_base_e_tar_aoe_"))
            {
                EObjets.Remove(test);
            }
        }
    }
}
