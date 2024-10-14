using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Data.Gameplay.Mods;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using System.Diagnostics;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppSystem.IO;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Gameplay.Mods;
using AncientMonkey;

namespace AncientMonkey.Weapons
{
    public class APRounds : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Armor-Piercing Rounds";
        public override string Icon => GetSpriteReference("texAPRoundsIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            //var hasItem = new DamageModifierForTagModel("", "balls", 0, 0, true, true);
            foreach (var i in towerModel.GetDescendants<WeaponModel>().ToArray())
            {
                foreach (var n in i.GetDescendants<DamageModifierForTagModel>().ToArray())
                {
                    if (n.name == "Armor-Piercing Rounds")
                    {
                        n.damageMultiplier += 0.2f;
                    }
                    else
                    {
                        towerModel.AddBehavior(new DamageModifierForTagModel("Armor-Piercing Rounds", "Moabs", 1.2f, 0, true, true));
                    }
                }
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class Crowbar : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Crowbar?";
        public override string Icon => GetSpriteReference("texCrowbarIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            //var hasItem = new DamageModifierForTagModel("", "balls", 0, 0, true, true);
            foreach (var i in towerModel.GetDescendants<WeaponModel>().ToArray())
            {
                foreach (var n in i.GetDescendants<DamageModifierForTagModel>().ToArray())
                {
                    if (n.name == "Armor-Piercing Rounds")
                    {
                        n.damageMultiplier += 0.75f;
                    }
                    else
                    {
                        towerModel.AddBehavior(new DamageModifierForTagModel("Crowbar", "ProcCrowbar", 1.75f, 0, true, true));
                    }
                }
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class FocusCrystal : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Focus Crystal?";
        public override string Icon => GetSpriteReference("texCrowbarIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            //var hasItem = new DamageModifierForTagModel("", "balls", 0, 0, true, true);
            foreach (var i in towerModel.GetDescendants<WeaponModel>().ToArray())
            {
                foreach (var n in i.GetDescendants<DamageModifierForTagModel>().ToArray())
                {
                    if (n.name == "Focus Crystal")
                    {
                        n.damageMultiplier += 0.20f;
                    }
                    else
                    {
                        towerModel.AddBehavior(new DamageModifierForTagModel("Focus Crystal", "ProcFocusCrystal", 1.2f, 0, true, true));
                    }
                }
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class BackupMagazine : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Backup Magazine";
        public override string Icon => GetSpriteReference("Iconpng").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();

            var uab = new AbilityModel("dummy", "dummy", "dummy", 0, 0, GetSpriteReference("Iconpng"), 30, new Il2CppReferenceArray<Il2CppAssets.Scripts.Models.Model>(0), false, false, "", 3, 0, 0, false, false, false);
            foreach (var AbilityModel in towerModel.GetAbilities().ToArray())
            {
                if (AbilityModel.name.Contains("Secondary"))
                {
                    uab = AbilityModel;
                }
            }
            if (uab.enabled == true)
            {
                towerModel.AddBehavior(uab);
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class BisonSteak : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Bison Steak";
        public override string Icon => GetSpriteReference("texSteakIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            towerModel.GetBehavior<LifeRegenModel>().overRegenAmount += 5;
            tower.UpdateRootModel(towerModel);
        }
    }

    public class CautiousSlug : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Cautious Slug";
        public override string Icon => GetSpriteReference("texSnailIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            towerModel.GetBehavior<LifeRegenModel>().regenAmount += 1;
            tower.UpdateRootModel(towerModel);
        }
    }

    //[HarmonyPatch(typeof(Il2CppAssets.Scripts.Models.Gameplay.Mods.), nameof(MaxHealthModModel))]
    //internal static class MaxHealthModModel
    //{
    //[HarmonyPrefix]
    //private static bool Postfix(MaxHealthModModel __instance, ref int __result)
    //{
    // return false;
    //}
    //}

    public class SoldiersSyringe : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Soldier's Syringe";
        public override string Icon => GetSpriteReference("texSyringeIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            foreach (var weaponModel in towerModel.GetDescendants<WeaponModel>().ToArray())
            {
                weaponModel.Rate *= 0.85f;
                weaponModel.rate *= 0.85f;
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class Mocha : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Mocha";
        public override string Icon => GetSpriteReference("texCoffeeIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            //towerModel.GetDescendant<RateSupportBombExpertModel>().pierceMultiplier += 0.07f;
            foreach (var weaponModel in towerModel.GetAttackModels().ToArray())
            {
                weaponModel.range *= 1.07f;
                foreach (var wawa in towerModel.GetWeapons().ToArray())
                {
                    wawa.Rate *= 0.85f / 2;
                    wawa.rate *= 0.85f / 2;

                    foreach (var projectileModel in towerModel.GetDescendants<ProjectileModel>().ToArray())
                    {
                        projectileModel.pierce += 0.125f;
                    }
                }
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class PaulsGoatHoof : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Common;
        public override string WeaponName => "Paul's Goat Hoof";
        public override string Icon => GetSpriteReference("Iconpng").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();

            foreach (var weaponModel in towerModel.GetAttackModels().ToArray())
            {
                weaponModel.range *= 1.14f;
                foreach (var projectileModel in towerModel.GetDescendants<ProjectileModel>().ToArray())
                {
                    projectileModel.pierce += 0.25f;
                }
            }
            //if (towerModel.GetDescendant<RateSupportBombExpertModel>() != null)
            //{
            //towerModel.GetDescendant<RateSupportBombExpertModel>().rangeMultiplier += 0.14f;
            //towerModel.GetDescendant<RateSupportBombExpertModel>().pierceMultiplier += 0.14f;
            //}
            //else
            //{
            //var PierceBuff = new RateSupportBombExpertModel("", true, false, 0.14f, 0.14f, new Il2CppReferenceArray<TowerFilterModel>(1));
            //var fillterModels = PierceBuff.filters.ToList();
            //fillterModels.Clear();
            //fillterModels.Add(new FilterInBaseTowerIdModel("Sally_FilterInBaseTowerIdModel", new Il2CppStringArray(["AncientMonkey-Commando"])));
            //PierceBuff.filters = fillterModels.ToIl2CppReferenceArray();
            //PierceBuff.SetName("ItemPierceBuffs");
            //towerModel.AddBehavior(PierceBuff);
            //}
            tower.UpdateRootModel(towerModel);
        }
    }

    public class Common
    {
        public static List<string> CommonWpn = new List<string>();
        public static List<string> CommonImg = new List<string>();
    }
}
