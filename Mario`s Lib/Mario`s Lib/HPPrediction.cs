using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;

namespace Mario_s_Lib
{
    internal class HPPrediction
    {
        public static readonly Dictionary<int, PredictedDamage> ActiveAttacks = new Dictionary<int, PredictedDamage>();

        public static void Initialize()
        {
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Game.OnUpdate += Game_OnUpdate;
            Spellbook.OnStopCast += Spellbook_OnStopCast;
            GameObject.OnDelete += GameObject_OnDelete;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.Team != ObjectManager.Player.Team || !sender.IsValidTarget(3000) || !args.SData.IsAutoAttack() || !(args.Target is Obj_AI_Base))
                return;

            var target = (Obj_AI_Base) args.Target;
            ActiveAttacks.Remove(sender.NetworkId);

            var attackData = new PredictedDamage(sender, target, Game.TicksPerSecond - Game.Ping/2, sender.AttackCastDelay*1000,
                sender.AttackDelay*1000 - (sender is Obj_AI_Turret ? 70 : 0), sender.IsMelee ? int.MaxValue : (int) args.SData.MissileSpeed,
                sender.GetAutoAttackDamage(target, true));

            ActiveAttacks.Add(sender.NetworkId, attackData);
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            ActiveAttacks.ToList()
                .Where(pair => pair.Value.StartTick < Game.TicksPerSecond - 3000)
                .ToList()
                .ForEach(pair => ActiveAttacks.Remove(pair.Key));
        }

        private static void Spellbook_OnStopCast(Obj_AI_Base sender, SpellbookStopCastEventArgs args)
        {
            if (sender.IsValid && args.StopAnimation)
            {
                if (ActiveAttacks.ContainsKey(sender.NetworkId))
                {
                    ActiveAttacks.Remove(sender.NetworkId);
                }
            }
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile?.SpellCaster != null)
            {
                var casterNetworkId = missile.SpellCaster.NetworkId;
                // ReSharper disable once UnusedVariable
                foreach (var activeAttack in ActiveAttacks.Where(activeAttack => activeAttack.Key == casterNetworkId))
                {
                    ActiveAttacks[casterNetworkId].Processed = true;
                }
            }
        }

        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (ActiveAttacks.ContainsKey(sender.NetworkId) && sender.IsMelee) ActiveAttacks[sender.NetworkId].Processed = true;
        }

        public static float GetHealthPrediction(Obj_AI_Base unit, int time, int delay = 70)
        {
            var predictedDamage = 0f;

            foreach (var attack in ActiveAttacks.Values)
            {
                var attackDamage = 0f;
                if (!attack.Processed && attack.Source.IsValidTarget(float.MaxValue) &&
                    attack.Target.IsValidTarget(float.MaxValue) && attack.Target.NetworkId == unit.NetworkId)
                {
                    var landTime = attack.StartTick + attack.Delay +
                                   1000*Math.Max(0, unit.Distance(attack.Source) - attack.Source.BoundingRadius)/attack.ProjectileSpeed + delay;

                    if (landTime < Game.TicksPerSecond + time)
                    {
                        attackDamage = attack.Damage;
                    }
                }
                predictedDamage += attackDamage;
            }
            return unit.Health - predictedDamage;
        }

        public static float LaneClearHealthPrediction(Obj_AI_Base unit, int time, int delay = 70)
        {
            var predictedDamage = 0f;

            foreach (var attack in ActiveAttacks.Values)
            {
                var n = 0;
                if (Game.TicksPerSecond - 100 <= attack.StartTick + attack.AnimationTime &&
                    attack.Target.IsValidTarget(float.MaxValue) &&
                    attack.Source.IsValidTarget(float.MaxValue) && attack.Target.NetworkId == unit.NetworkId)
                {
                    var fromT = attack.StartTick;
                    var toT = Game.TicksPerSecond + time;

                    while (fromT < toT)
                    {
                        if (fromT >= Game.TicksPerSecond &&
                            (fromT + attack.Delay + Math.Max(0, unit.Distance(attack.Source) - attack.Source.BoundingRadius)/attack.ProjectileSpeed <
                             toT))
                        {
                            n++;
                        }
                        fromT += (int) attack.AnimationTime;
                    }
                }
                predictedDamage += n*attack.Damage;
            }
            return unit.Health - predictedDamage;
        }

        public class PredictedDamage
        {
            public readonly float AnimationTime;

            public PredictedDamage(Obj_AI_Base source, Obj_AI_Base target, int startTick, float delay, float animationTime, int projectileSpeed,
                float damage)
            {
                Source = source;
                Target = target;
                StartTick = startTick;
                Delay = delay;
                ProjectileSpeed = projectileSpeed;
                Damage = damage;
                AnimationTime = animationTime;
            }

            public float Damage { get; }
            public float Delay { get; }
            public int ProjectileSpeed { get; }
            public Obj_AI_Base Source { get; }
            public int StartTick { get; internal set; }
            public Obj_AI_Base Target { get; }
            public bool Processed { get; internal set; }
        }
    }
}