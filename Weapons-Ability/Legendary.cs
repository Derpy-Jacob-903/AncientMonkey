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
using AncientMonkey;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;

namespace AncientMonkey.Weapons
{
    public class Legendary
    {
        public class HardlightAfterburner : WeaponTemplate
        {
            public override int SandboxIndex => 1;
            public override Rarity WeaponRarity => Rarity.Legendary;
            public override string WeaponName => "Hardlight Afterburner";
            public override string Icon => GetSpriteReference("Iconpng").GUID;
            public override void EditTower(Tower tower)
            {
                var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
                var uab = new AbilityModel("", "", "", 0, 0, null, 30, null, false, false, "", 3, 0, 0, false, false, false);
                foreach (var AbilityModel in towerModel.GetAbilities().ToArray())
                {
                    if (AbilityModel.name.Contains("Utility"))
                    {
                        uab = AbilityModel;
                    }
                }
                if (uab.enabled == true) 
                {
                    towerModel.AddBehavior(uab);
                    towerModel.AddBehavior(uab);
                }
                foreach (var AbilityModel in towerModel.GetAbilities().ToArray())
                {
                    if (AbilityModel.name.Contains("Utility"))
                    {
                        AbilityModel.Cooldown *= 0.67f;
                        AbilityModel.cooldown *= 0.67f;
                    }
                }
                tower.UpdateRootModel(towerModel);
            }
        }
        public class AlienHead : WeaponTemplate
        {
            public override int SandboxIndex => 1;
            public override Rarity WeaponRarity => Rarity.Legendary;
            public override string WeaponName => "Alien Head";
            public override string Icon => GetSpriteReference("texAlienHeadIcon").GUID;
            public override void EditTower(Tower tower)
            {
                var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
                var ab = towerModel.GetAbilities()[1].Duplicate();
                foreach (var AbilityModel in towerModel.GetAbilities().ToArray())
                {
                    if (AbilityModel.name.Contains("Secondary") || AbilityModel.name.Contains("Utility") || AbilityModel.name.Contains("Special"))
                    {
                        AbilityModel.Cooldown *= 0.75f;
                        AbilityModel.cooldown *= 0.75f;
                    }
                }
                tower.UpdateRootModel(towerModel);
            }
        }
        public static List<string> LegendaryWpn = new List<string>();
        public static List<string> LegendaryImg = new List<string>();
    }
}
