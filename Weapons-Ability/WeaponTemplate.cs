using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation.Towers;

namespace AncientMonkey
{
    
    public abstract class WeaponTemplate : ModContent 
    {
        public override void Register(){}
        public abstract int SandboxIndex { get; }
        public abstract Rarity WeaponRarity { get; }
        public abstract string WeaponName { get; }
        public abstract string Icon { get; }
        public abstract void EditTower(Tower tower);
        public enum Rarity
        {
            Common, //White
            Uncommon, //Green
            Legendary, //Red
            Exotic, //Yellow

            Lunar, //Blue

            VoidCommon, //Purple
            VoidUncommon, //Purple
            VoidLegendary, //Purple
            VoidExotic, //Purple
        }
        public virtual bool IsCamo { get; }
        public virtual bool IsLead { get; }
        public virtual bool IsModded { get; }
        public virtual string ModName { get; }


        public List<AttackModel> attackModelList = new List<AttackModel>();
    }
}
