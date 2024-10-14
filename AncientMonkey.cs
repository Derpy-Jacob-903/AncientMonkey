using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppTMPro;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.ServerEvents;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using System.Linq;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Simulation.GeraldoItems;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace blankdisplay
{
    public class BlankDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "NoneDisplay");
        }
    }
}
namespace AncientMonkey
{
    public class AncientMonkeyTower : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.EngineerMonkey + "-013";
        public override int Cost => 0;
        public override string DisplayName => "Commando Monkey";
        public override string Name => "Commando";
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0; 
        public override string Description => "The Ancient Monkey is very weak in the Beginning but you can make him stronger.";
        public override string Portrait => "AncientMonkeyIcon";
        public override string Icon => "AncientMonkeyIcon";
        public override bool IsValidCrosspath(int[] tiers) => ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            //var lzrdisplay = Game.instance.model.GetTowerFromId("SuperMonkey-100").GetAttackModel().weapons[0].projectile.display;
            var cachedStun = attackModel.weapons[0].projectile.GetBehavior<SlowOnPopModel>().Duplicate("SlowOnPopModel_Special_Suppressive_Fire");
            attackModel.weapons[0].projectile = Game.instance.model.GetTowerFromId("EngineerMonkey").GetAttackModel().weapons[0].projectile.Duplicate();
            attackModel.weapons[0].projectile.pierce = 1;
            //attackModel.weapons[0].rate = 0.5f;
            //attackModel.weapons[0].Rate = 0.5f; //nerf from 0.35
            var DTdisplay = attackModel.weapons[0].projectile;
            DTdisplay.ApplyDisplay<DoubleTapDisplay>();
            attackModel.SetName("DoubleTap");
            attackModel.weapons[0].SetName("DoubleTap");
            attackModel.weapons[0].projectile.SetName("DoubleTap");
            //towerModel.RemoveBehavior(attackModel);
            //copied from 
            var PRab1 = new AbilityModel("Secondary_PhaseRound", "Phase Round", "Fire a piercing bullet for 300% damage. Deals 40% more damage every time it passes through an enemy.", 0, 0, new Il2CppNinjaKiwi.Common.ResourceUtils.SpriteReference(VanillaSprites.IcicleImpaleUpgradeIcon), 3, null, false, false, "Secondary_PhaseRound", 0.0f, 0, -1, false, false);
            PRab1.AddBehavior(new ActivateAttackModel("ActivateAttackModel_Secondary_PhaseRound", 0.5f, true, new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<AttackModel>(1), true, false, false, false, true));
            PRab1.Cooldown = 3; PRab1.cooldown = 3; PRab1.cooldownFrames = 30;
            var proj = PRab1.GetDescendant<ActivateAttackModel>().attacks[0];
            proj = Game.instance.model.GetTowerFromId("SuperMonkey-220").GetAttackModels()[0].Duplicate();
            proj.weapons[0].projectile.ApplyDisplay<PhaseRoundDisplay>();
            proj.weapons[0].projectile.pierce = 40;
            proj.weapons[0].projectile.GetDamageModel().damage *= 3;
            proj.weapons[0].projectile.GetDescendant<TravelStraitModel>().Speed = proj.weapons[0].projectile.GetDescendant<TravelStraitModel>().speed = 0.6f;
            proj.weapons[0].projectile.GetDescendant<TravelStraitModel>().speedFrames = 0.6f;
            PRab1.GetDescendant<ActivateAttackModel>().isOneShot = true;
            PRab1.dontShowStacked = false;
            PRab1.GetDescendant<ActivateAttackModel>().lifespan = 0.5f; PRab1.GetDescendant<ActivateAttackModel>().Lifespan = 0.5f; PRab1.GetDescendant<ActivateAttackModel>().lifespanFrames = 30;
            //PRab1.GetDescendant<AttackModel>().targetProvider = attackModel.targetProvider;
            PRab1.SetName("Secondary_PhaseRound");
            PRab1.icon = GetSpriteReference("Phase_Round");
            towerModel.AddBehavior(PRab1);
            //
            var ab2 = Game.instance.model.GetTowerFromId("SuperMonkey-003").GetAbility().Duplicate();
            ab2.SetName("Utility_Blink");
            ab2.dontShowStacked = false;
            ab2.icon = GetSpriteReference("Blink");
            towerModel.AddBehavior(ab2);
            //
            //var SFab3 = new AbilityModel("Special_Suppressive_Fire", "Suppressive Fire", "[Stunning] Fire repeatedly for 100% damage per bullet. The number of shots increases with attack speed.", 0, 0, new Il2CppNinjaKiwi.Common.ResourceUtils.SpriteReference(VanillaSprites.IcicleImpaleUpgradeIcon), 3, null, false, false, "Secondary_PhaseRound", 0.0f, 0, -1, false, false);
            //SFab3.AddBehavior(new ActivateAttackModel("ActivateAttackModel_Suppressive_Fire", 1f, true, new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<AttackModel>(1), true, true, false, false, false));
            //SFab3.Cooldown = 9; SFab3.cooldown = 9; SFab3.cooldownFrames = 60;
            //var proj2 = SFab3.GetDescendant<ActivateAttackModel>().attacks[0]; //Array of AttackModels
            //proj2 = attackModel.Duplicate();
            //SFab3.GetBehavior<ActivateAttackModel>().lifespan = 1f; SFab3.GetDescendant<ActivateAttackModel>().Lifespan = 1f;
            //if (proj2 != null) proj2.weapons[0].rate = proj2.weapons[0].Rate = attackModel.weapons[0].Rate / 6;
            //SFab3.GetBehavior<ActivateAttackModel>().isOneShot = false;
            //SFab3.GetBehavior<ActivateAttackModel>().turnOffExisting = true;
            //proj2.weapons[0].projectile.AddBehavior(cachedStun);
            //SFab3.addedViaUpgrade = Id;
            //SFab3.SetName("Special_Suppressive_Fire");
            //SFab3.icon = GetSpriteReference("Suppressive_Fire");

            //towerModel.AddBehavior(SFab3);
            //tower.UpdateRootModel(towerModel);


            var Rejuv = new LifeRegenModel("LifeRegenModel_", 0, 0, 1.25f, new PrefabReference("eb70b6823aec0644c81f873e94cb26cc"));
            towerModel.AddBehavior(Rejuv);

            //for percent pierce buffs i.e. Paul's Goat Hoof
            //var PierceBuff = new RateSupportBombExpertModel("", true, false, 0.0f, 0.0f, new Il2CppReferenceArray<TowerFilterModel>(1));
            //var i = new PierceSupportModel("")
            //var fillterModels = PierceBuff.filters.ToList();
            //fillterModels.Clear();
            //fillterModels.Add(new FilterInBaseTowerIdModel("Sally_FilterInBaseTowerIdModel", new Il2CppStringArray(["AncientMonkey-Commando"])));
            //PierceBuff.filters = fillterModels.ToIl2CppReferenceArray();
            //PierceBuff.SetName("ItemPierceBuffs");
            //towerModel.AddBehavior(PierceBuff);
        }
    }
    public class CardMonkeyBaseDisplay : ModTowerDisplay<AncientMonkeyTower>
    {
        // Copy the Boomerang Monkey display
        public override string BaseDisplay => GetDisplay(TowerType.EngineerMonkey, 0, 0, 3);

        public override bool UseForTower(int[] tiers)
        {
            return tiers.Sum() == 0;
        }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            // Print info about the node in order to edit it easier
# if DEBUG
            //node.PrintInfo();
            //node.SaveMeshTexture();
#endif

            // Set our custom texture
            SetMeshTexture(node, "98994D4EB4F0760DF56863A95591D8EC");
            SetMeshOutlineColor(node, new Color(0.360784f, 0.262744f, 0.168625f));

            // Make it not hold the Boomerang
            //node.RemoveBone("SuperMonkeyRig:Dart");
        }
    }
    public class DoubleTapDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "LaserBlastSingle");
        }
    }
    public class PhaseRoundDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "PhaseRoundProj");
        }
    }
    public class SuppressiveFireDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "SuppressiveFireProj");
        }
    }
    public class MissileDisplay : ModDisplay //AtG Missile Mk. 1/2
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "ComancheBomb");
            //scale??
        }
    }

    [HarmonyPatch(typeof(Il2CppAssets.Scripts.Simulation.SimulationBehaviors.NecroData), nameof(NecroData.RbePool))]
    internal static class Necro_RbePool
    {
        [HarmonyPrefix]
        private static bool Postfix(NecroData __instance, ref int __result)
        {
            var tower = __instance.tower;
            if (tower.towerModel.name.Contains("Commando"))
            {
                __result = 9999;
            }
            return false;
        }
    }

    //[HarmonyPatch(typeof(Il2CppAssets.Scripts.Simulation.Bloons), nameof(Bloon))]
    //internal static class ApplyDamageToBloon
    //{
        //[HarmonyPrefix]
        //private static bool Postfix(ApplyDamageToBloon __instance, ref int __result)
        //{
            //var applyDamageToBloon = __instance;
            //if (applyDamageToBloon.) 
            //{
                //__result = 9999;
            //}
            //return false;
        //}
    //}
    //

    //public abstract class Bleed : AddBehaviorToBloonModel()
}
