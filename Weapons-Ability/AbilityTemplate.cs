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
using UnityEngine;

namespace AncientMonkey.Weapons
{
    public abstract class AbilityTemplate : ModContent
    {
        public override void Register() { }
        public abstract string AbilityName { get; }
        public abstract string Icon { get; }
        public abstract void EditTower(Tower tower);
        public float stackIndex = 0;
        public bool enabled = true;
        public virtual string Description { get; }
        public virtual Sprite CustomIcon { get; }
        public virtual bool IsCamo { get; }
        public virtual bool IsLead { get; }
    }
}
