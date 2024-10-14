using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Extensions;

namespace AncientMonkey.Weapons
{
    public abstract class AbilityTemplate : ModContent
    {
        public override void Register() { }
        public abstract string AbilityName { get; }
        public abstract string Icon { get; }
        public abstract void EditTower(Tower tower);
        public virtual bool IsCamo { get; }
        public virtual bool IsLead { get; }

        public void SwapEquipment(TowerModel towerModel, AbilityModel newAbilityModel)
        {
            foreach (var AbilityModel in towerModel.GetAbilities().ToArray())
            {
                if (AbilityModel.name.Contains("Equipment"))
                {
                    towerModel.RemoveBehavior(AbilityModel);
                }
            }
            towerModel.AddBehavior(newAbilityModel);
            return;
        }
        public void EditEquipmentName(AbilityModel abilityModel)
        {
            if (!abilityModel.name.Contains("Equipment"))
            {
                var i = abilityModel.name.IndexOf("_");
                abilityModel.name.Insert(i, "_Equipment");
            }
            return;
        }
    }
}
