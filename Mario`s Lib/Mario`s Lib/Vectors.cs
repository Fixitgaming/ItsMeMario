using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

// ReSharper disable CoVariantArrayConversion

namespace Mario_s_Lib
{
    public static class Vectors
    {
        public static bool IsSolid(this Vector3 pos)
        {
            return pos.ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Building) && pos.ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall);
        }

        public static Vector3 GetBestCircularFarmPosition(this Spell.Skillshot spell, int count = 3, int hitchance = 85)
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .Where(
                        m => m.IsValidTarget(spell.Range))
                    .ToArray();

            if (minions.Length == 0 && minions != null) return Vector3.Zero;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, spell.Range, spell.Width,
                spell.CastDelay, spell.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            if (farmLocation != null && farmLocation.HitChancePercent >= hitchance && farmLocation.CollisionObjects.Length >= count)
            {
                return farmLocation.CastPosition;
            }

            return Vector3.Zero;
        }

        public static Vector3 GetBestLinearFarmPosition(this Spell.Skillshot spell, int minMinionsToHit = 3)
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(spell.Range)).ToArray();

            var bestPos = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, spell.Width,
                (int) spell.Range, Player.Instance.Position.To2D());

            if (minions.Length > 0 && bestPos.HitNumber >= minMinionsToHit)
            {
                return bestPos.CastPosition;
            }

            return Vector3.Zero;
        }

        public static Vector3 GetBestCircularCastPosition(this Spell.Skillshot spell, int count = 3, int hitchance = 75)
        {
            var heros =
                EntityManager.Heroes.Enemies.Where(
                    m => m.IsValidTarget(spell.Range))
                    .ToArray();

            if (heros.Length == 0 && heros != null) return Vector3.Zero;

            var castPos = Prediction.Position.PredictCircularMissileAoe(heros, spell.Range, spell.Width,
                spell.CastDelay, spell.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            if (castPos != null && castPos.HitChancePercent >= hitchance)
            {
                return castPos.CastPosition;
            }

            return Vector3.Zero;
        }

        public static BestCastPosition GetBestLinearCastPosition(IEnumerable<AIHeroClient> entities, float width, int range,
            Vector2? sourcePosition = null)
        {
            var targets = entities.ToArray();
            switch (targets.Length)
            {
                case 0:
                    return new BestCastPosition();
                case 1:
                    return new BestCastPosition {CastPosition = targets[0].ServerPosition, HitNumber = 1};
            }

            var posiblePositions = new List<Vector2>(targets.Select(o => o.ServerPosition.To2D()));
            foreach (var target in targets)
            {
                posiblePositions.AddRange(from t in targets
                    where t.NetworkId != target.NetworkId
                    select (t.ServerPosition.To2D() + target.ServerPosition.To2D())/2);
            }

            var startPos = sourcePosition ?? Player.Instance.ServerPosition.To2D();
            var minionCount = 0;
            var result = Vector2.Zero;

            foreach (var pos in posiblePositions.Where(o => o.IsInRange(startPos, range)))
            {
                var endPos = startPos + range*(pos - startPos).Normalized();
                var count = targets.Count(o => o.ServerPosition.To2D().Distance(startPos, endPos, true, true) <= width*width);

                if (count >= minionCount)
                {
                    result = endPos;
                    minionCount = count;
                }
            }

            return new BestCastPosition {CastPosition = result.To3DWorld(), HitNumber = minionCount};
        }

        public struct BestCastPosition
        {
            public int HitNumber;
            public Vector3 CastPosition;
        }
    }
}