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
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
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
using System.Threading;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using Monkeys;
using Il2CppAssets.Scripts.Models.Effects;

namespace AncientMonkey.Weapons
{
    public class VoidUncommon
    {
        public static List<string> VoidUncommonWpn = new List<string>();
        public static List<string> VoidUncommonImg = new List<string>();
    }
}
namespace Monkeys
{
    public class LychMinion : ModTower
    {
        public override string Portrait => "Officer";
        public override string Name => "Lych";
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.DartMonkey + "-002";

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;

        public override string DisplayName => "Lych";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 40f, 3, false, false));
            weapons.rate = 0.06f;
            projectile.display = Game.instance.model.GetBloon(BloonType.Lych5).display;
            projectile.scale *= 0.3f;
            projectile.GetDamageModel().damage = 750;
            projectile.pierce = 75;
            projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            projectile.GetBehavior<TravelStraitModel>().lifespan *= 3;
            towerModel.range = 50;
            attackModel.range = 50;
            towerModel.radius = 0;
            towerModel.displayScale *= 0.6f;
            towerModel.isGlobalRange = false;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
            towerModel.display = Game.instance.model.GetBloon(BloonType.Lych5).display;
            towerModel.ignoreTowerForSelection = true;
        }

    }
}