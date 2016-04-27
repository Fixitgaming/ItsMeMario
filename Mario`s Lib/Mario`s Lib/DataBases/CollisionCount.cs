using System.Collections.Generic;
using EloBuddy;

namespace Mario_s_Lib.DataBases
{
    public class CollisionCountOBJ
    {
        public Champion Champion;
        public SpellSlot Slot;
        public int Count;

        public CollisionCountOBJ(Champion champ, SpellSlot slot, int count)
        {
            Champion = champ;
            Slot = slot;
            Count = count;
        }
    }

    public static class CollisionCount
    {
        public static List<CollisionCountOBJ> CollisionCountDB = new List<CollisionCountOBJ>
        {
            new CollisionCountOBJ(Champion.Aatrox, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Aatrox, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.Ahri, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Anivia, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Annie, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Ashe, SpellSlot.R, -1),

            new CollisionCountOBJ(Champion.AurelionSol, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Azir, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Bard, SpellSlot.Q, 1),

            new CollisionCountOBJ(Champion.Brand, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Braum, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Caitlyn, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Caitlyn, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Cassiopeia, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Cassiopeia, SpellSlot.W, int.MaxValue),
            new CollisionCountOBJ(Champion.Cassiopeia, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Corki, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Darius, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.Diana, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Draven, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Ekko, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Ekko, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Ezreal, SpellSlot.W, int.MaxValue),
            new CollisionCountOBJ(Champion.Ezreal, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Fizz, SpellSlot.R, -1),

            new CollisionCountOBJ(Champion.Galio, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Galio, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.Gangplank, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.Gragas, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Graves, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Graves, SpellSlot.W, int.MaxValue),
            new CollisionCountOBJ(Champion.Graves, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Hecarim, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Heimerdinger, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.Illaoi, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Irelia, SpellSlot.R, int.MaxValue),

            new CollisionCountOBJ(Champion.Janna, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.JarvanIV, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.JarvanIV, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.Jhin, SpellSlot.W, -1),
            new CollisionCountOBJ(Champion.Jhin, SpellSlot.E, int.MaxValue),
            new CollisionCountOBJ(Champion.Jhin, SpellSlot.R, -1),

            new CollisionCountOBJ(Champion.Jinx, SpellSlot.E, int.MaxValue),
            new CollisionCountOBJ(Champion.Jinx, SpellSlot.R, -1),

            new CollisionCountOBJ(Champion.Kalista, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Karthus, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Karthus, SpellSlot.W, int.MaxValue),

            new CollisionCountOBJ(Champion.Kassadin, SpellSlot.E, int.MaxValue),

            new CollisionCountOBJ(Champion.KogMaw, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),
            new CollisionCountOBJ(Champion.Chogath, SpellSlot.Q, int.MaxValue),

            new CollisionCountOBJ(Champion.Lux, SpellSlot.Q, 1),
            new CollisionCountOBJ(Champion.Lux, SpellSlot.W, int.MaxValue),
            new CollisionCountOBJ(Champion.Lux, SpellSlot.E, int.MaxValue),
            new CollisionCountOBJ(Champion.Lux, SpellSlot.R, int.MaxValue),

        }; 
    }
}
