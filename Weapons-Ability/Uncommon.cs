using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Data.Gameplay.Mods;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Data.Gameplay.Mods;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppSystem.IO;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using static Il2CppSystem.Globalization.HebrewNumber;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

namespace AncientMonkey.Weapons
{
    public class Chronobauble : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Uncommon;
        public override string WeaponName => "Chronobauble";
        public override string Icon => GetSpriteReference("texBaubleIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            var hasItem = false;

            //var SlowModel = proj.GetBehavior<SlowModel>();
            //new SlowModel("", 0.6f, 2, "60% Slow", 999, "", true, false, null, true ,false, false);
            foreach (var attackModel in towerModel.GetAttackModels().ToArray())
            {
                foreach (var projectile in attackModel.GetDescendants<ProjectileModel>().ToArray())
                {
                    foreach (var slow in projectile.GetDescendants<SlowModel>().ToArray())
                    {
                        if (slow.name.Contains("Chronobauble"))
                        {
                            hasItem = true;
                            slow.Lifespan += 2;
                            slow.lifespan += 2;
                            slow.lifespanFrames += 120;
                        }
                    }
                    if (hasItem == false)
                    {
                        var proj = Game.instance.model.GetTowerFromId("GlueGunner-100").GetAttackModel().weapons[0].projectile.Duplicate();
                        var newSlowModel = proj.GetBehavior<SlowModel>();
                        newSlowModel.Lifespan = 2;
                        newSlowModel.lifespan = 2;
                        newSlowModel.lifespanFrames = 120;
                        newSlowModel.SetName("Chronobauble");
                        projectile.AddBehavior(newSlowModel);
                    }
                }
            }
            tower.UpdateRootModel(towerModel);
        }
    }
    public class AtGMissileMk1 : WeaponTemplate
    {
        public override int SandboxIndex => 1;
        public override Rarity WeaponRarity => Rarity.Uncommon;
        public override string WeaponName => "AtG Missile Mk.1";
        public override string Icon => GetSpriteReference("texMissileLauncherIcon").GUID;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            var n = towerModel.GetDescendant<AttackModel>();
            var hasAtg = false;
            foreach (var i in towerModel.GetDescendants<AttackModel>().ToArray())
            {
                if (i.name == "DoubleTap")
                {
                    n = i; //should be DoubleTap
                }
                foreach (var V in i.weapons.ToArray())
                {
                    if (V.name == "AtGMissileMk1")
                    {
                        hasAtg = true;
                        V.projectile.GetDamageModel().damage += 3;
                    }
                }
            }
            if (!hasAtg)
            {
                foreach (var V in Game.instance.model.GetTowerFromId("HeliPilot-420").GetDescendants<AttackModel>().ToArray())
                {
                    if (V.name == "AttackModel_Missiles_MissileArrayFirst")
                    {
                        V.SetName("AtGMissileMk1");
                        V.weapons[0].SetName("AtGMissileMk1");
                        V.weapons[0].rate *= 10; V.weapons[0].Rate *= 10; V.weapons[0].rateFrames *= 10;
                        V.weapons[0].projectile.GetDamageModel().damage = 3;
                        V.weapons[0].projectile.AddBehavior(new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_",99,false,false,270f,false,90,false,5,true));
                        V.weapons[0].emission = new SingleEmissionModel("SingleEmissionModel_AtGMissileMk1", new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<EmissionBehaviorModel>(0));
                        V.weapons[0].emission.AddBehavior(new EmissionRotationOffsetModel("EmissionRotationOffsetModel_AtGMissileMk1", 45f));
                        n.AddWeapon(V.weapons[0]);
                    }
                }
            }
            //foreach (var i in towerModel.GetDescendants<AttackModel>().ToArray())
            //{
            //foreach (var v in towerModel.GetDescendants<AlternateProjectileModel>().ToArray())
            //{
            //if (v.name == "AlternateProjectileModel_AtGMissileMk1")
            //{
            //v.projectile.GetDamageModel().damage += 3;
            //}
            //}
            //}
            //n.AddBehavior(new AlternateProjectileModel("AlternateProjectileModel_AtGMissileMk1", null, null, 10);
            tower.UpdateRootModel(towerModel);
        }
    }
    public class Uncommon
    {
        public static List<string> UncommonWpn = new List<string>();
        public static List<string> UncommonImg = new List<string>();
    }
}
