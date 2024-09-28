using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Simulation.Towers;
using UnityEngine;

namespace AncientMonkey.Quest
{
    public abstract class QuestTemplate : ModContent
    {
        public override void Register() { }
        public abstract string QuestID { get; }
        public abstract string QuestName { get; }
        public abstract double GoalCountRequired { get; }
        public abstract int Reward { get; }
        public abstract Goal QuestGoal { get; }
        public bool Cleared = false;
        public enum Goal
        {
            BloonsPopped,
            RoundCleared,
            WeaponsBought,
            AbilityBought,
            StrongerBought,
            UpgradesBought,
            CashSpent,
        }
    }
}
