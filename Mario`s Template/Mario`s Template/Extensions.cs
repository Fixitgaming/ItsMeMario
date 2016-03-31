﻿using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using static Mario_s_Template.Menus;
// ReSharper disable CoVariantArrayConversion

namespace Mario_s_Template
{
    public static class Extensions
    {
        #region Vector
        /// <summary>
        /// Checks if the position is solid
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static bool IsSolid(this Vector3 pos)
        {
            return pos.ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Building) && pos.ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall);
        }

        public static Vector3 GetBestCircularFarmPosition(this Spell.Skillshot spell, int count = 3)
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .Where(
                        m => m.IsValidTarget(spell.Range))
                    .ToArray();

            if (minions.Length == 0 && minions != null) return Vector3.Zero;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, spell.Range, spell.Width,
                spell.CastDelay, spell.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            return farmLocation?.CastPosition ?? Vector3.Zero;
        }

        public static Vector3 GetBestLinearFarmPosition(this Spell.Skillshot spell, int minMinionsToHit = 3)
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(spell.Range)).ToArray();

            var bestPos = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, spell.Width,
                (int) spell.Range, Player.Instance.Position.To2D());

            return minions.Length != 0 ? bestPos.CastPosition : Vector3.Zero;

        }

        public static Vector3 GetBestCircularCastPosition(this Spell.Skillshot spell, int count = 3)
        {
            var heros =
                EntityManager.Heroes.Enemies.Where(
                        m => m.IsValidTarget(spell.Range))
                    .ToArray();

            if (heros.Length == 0 && heros != null) return Vector3.Zero;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(heros, spell.Range, spell.Width,
                spell.CastDelay, spell.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            return farmLocation?.CastPosition ?? Vector3.Zero;
        }

        public static BestCastPosition GetBestLinearCastPosition(IEnumerable<AIHeroClient> entities, float width, int range, Vector2? sourcePosition = null)
        {
            var targets = entities.ToArray();
            switch (targets.Length)
            {
                case 0:
                    return new BestCastPosition();
                case 1:
                    return new BestCastPosition { CastPosition = targets[0].ServerPosition, HitNumber = 1 };
            }

            var posiblePositions = new List<Vector2>(targets.Select(o => o.ServerPosition.To2D()));
            foreach (var target in targets)
            {
                posiblePositions.AddRange(from t in targets where t.NetworkId != target.NetworkId select (t.ServerPosition.To2D() + target.ServerPosition.To2D()) / 2);
            }

            var startPos = sourcePosition ?? Player.Instance.ServerPosition.To2D();
            var minionCount = 0;
            var result = Vector2.Zero;

            foreach (var pos in posiblePositions.Where(o => o.IsInRange(startPos, range)))
            {
                var endPos = startPos + range * (pos - startPos).Normalized();
                var count = targets.Count(o => o.ServerPosition.To2D().Distance(startPos, endPos, true, true) <= width * width);

                if (count >= minionCount)
                {
                    result = endPos;
                    minionCount = count;
                }
            }

            return new BestCastPosition { CastPosition = result.To3DWorld(), HitNumber = minionCount };
        }

        public struct BestCastPosition
        {
            public int HitNumber;
            public Vector3 CastPosition;
        }

        #endregion Vector

        #region Spells
        #region CanCast

        public static bool CanCast(this Obj_AI_Base target, Spell.SpellBase spell, Menu m)
        {
            if (spell == null) return false;
            if (m.UniqueMenuId != ComboMenuID)
            {
                if (Player.Instance.ManaPercent < m.GetSliderValue("manaSlider")) return false;
            }
            return target.IsValidTarget(spell.Range) && spell.IsReady() &&
                   m.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use");
        }

        public static bool CanCast(this Obj_AI_Base target, Spell.Active spell, Menu m)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCast(asBase, m);
        }

        public static bool CanCast(this Obj_AI_Base target, Spell.Skillshot spell, Menu m, int hitchancePercent = 75)
        {
            var asBase = spell as Spell.SpellBase;
            var pred = spell.GetPrediction(target);
            return target.CanCast(asBase, m) && pred.HitChancePercent >= 75;
        }

        public static bool CanCast(this Obj_AI_Base target, Spell.Chargeable spell, Menu m)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCast(asBase, m);
        }

        public static bool CanCast(this Obj_AI_Base target, Spell.Ranged spell, Menu m)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCast(asBase, m);
        }

        public static bool CanCast(this Obj_AI_Base target, Spell.Targeted spell, Menu m)
        {
            var asBase = spell as Spell.SpellBase;
            return target.CanCast(asBase, m);
        }

        #endregion CanCast

        #region TryToCast

        public static bool TryToCast(this Spell.SpellBase spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCast(spell, m) && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Active spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCast(spell, m) && spell.Cast();
        }

        public static bool TryToCast(this Spell.Skillshot spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCast(spell, m) && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Targeted spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCast(spell, m) && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Chargeable spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCast(spell, m) && spell.Cast(target);
        }

        public static bool TryToCast(this Spell.Ranged spell, Obj_AI_Base target, Menu m)
        {
            if (target == null) return false;
            return target.CanCast(spell, m) && spell.Cast(target);
        }

        #endregion TryToCast

        #region Drawings

        public static void DrawSpell(this Spell.SpellBase spell, Color color)
        {
            if (DrawingsMenu.GetCheckBoxValue(spell.Slot.ToString().ToLower() + "Use") &&
                DrawingsMenu.GetCheckBoxValue("readyDraw") ? spell.IsReady() : spell.IsLearned)
            {
                EloBuddy.SDK.Rendering.Circle.Draw(color, spell.Range, 1f, Player.Instance);
            }
        }

        #endregion Drawings

        #endregion Spells

        #region Menus

        #region Creating

        public static void CreateCheckBox(this Menu m, string displayName, string uniqueId, bool defaultValue = true)
        {
            try
            {
                m.Add(uniqueId, new CheckBox(displayName, defaultValue));
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Error creating the checkbox with the uniqueID = ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(uniqueId);
                Console.ResetColor();
            }
        }

        public static void CreateSlider(this Menu m, string displayName, string uniqueId, int defaultValue = 0, int minValue = 0, int maxValue = 100)
        {
            try
            {
                m.Add(uniqueId, new Slider(displayName, defaultValue, minValue, maxValue));
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Error creating the slider with the uniqueID = ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(uniqueId);
                Console.ResetColor();
            }
        }

        #endregion Creating

        #region Getting

        public static bool GetCheckBoxValue(this Menu m, string uniqueId)
        {
            try
            {
                return m.Get<CheckBox>(uniqueId).CurrentValue;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Error getting the checkbox with the uniqueID = ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(uniqueId);
                Console.ResetColor();
            }
            return false;
        }

        public static int GetSliderValue(this Menu m, string uniqueId)
        {
            try
            {
                return m.Get<Slider>(uniqueId).CurrentValue;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Error getting the slider with the uniqueID = ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(uniqueId);
                Console.ResetColor();
            }
            return -1;
        }

        #endregion Getting

        #endregion Menus

        #region GetTargetHelper

        public static Obj_AI_Minion GetLastHitMinion(this Spell.SpellBase spell)
        {
            return
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .FirstOrDefault(
                        m =>
                            m.IsValidTarget(spell.Range) && Prediction.Health.GetPrediction(m, spell.CastDelay) <= m.GetDamage(spell.Slot) &&
                            m.IsEnemy);
        }

        public static AIHeroClient GetKillableHero(this Spell.SpellBase spell)
        {
            return
                EntityManager.Heroes.Enemies.FirstOrDefault(
                    e =>
                        e.IsValidTarget(spell.Range) && Prediction.Health.GetPrediction(e, spell.CastDelay) <= e.GetDamage(spell.Slot) &&
                        !e.HasUndyingBuff());
        }

        public static Obj_AI_Base GetJungleMinion(this Spell.SpellBase spell)
        {
            return
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(spell.Range));
        }

        #endregion GetTargetHelper
    }
}