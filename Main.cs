using MelonLoader;
using BTD_Mod_Helper;
using AncientMonkey;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using UnityEngine;
using System.Linq;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppSystem.IO;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api;
using Il2CppTMPro;
using Il2CppNinjaKiwi.Common;
using AncientMonkey.Weapons;
using BTD_Mod_Helper.Api.ModOptions;
using AncientMonkey.Challenge;

[assembly: MelonInfo(typeof(AncientMonkey.AncientMonkey), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AncientMonkey;

public class AncientMonkey : BloonsTD6Mod
{
    internal static readonly ModSettingBool SandboxMode = new(false)
    {
        requiresRestart = true,
        icon = VanillaSprites.SandboxBtn,
        description = "Enable Sandbox Mode"
    };

    
    public static AncientMonkey mod;
    public float newWeaponCost = 250;
    public float baseNewWeaponCost = 250;
    public float uncommonChance = 85;
    public float legendaryChance = 99;
    public float exoticChance = 100;
    public float voidChance = 100;
    public float lunarChance = 100;
    public float uncommonStrongChance = 0;
    public float voidStrongChance = 100;
    public float legendaryStrongChance = 80;
    public float lunarStrongChance = 100;
    public float strongerWeaponCost = 1100;
    public float baseStrongerWeaponCost = 1100; //used for large chests
    public float newAbilityCost = 1750;
    public float baseNewAbilityCost = 1750;
    public bool upgradeOpen = false;
    public bool selectingWeaponOpen = false;
    public bool mib = false;
    public bool panelOpen = false;
    public float level = 0;
    public float rangeBoostSandbox = 1;
    public float attackSpeedBoostSandbox = 1;
    public float moneyBoostSandbox = 1;
    public int damageBoostSandbox = 0;
    public int pierceBoostSandbox = 0;
    public int newWeaponSlot = 3;
    public int strongWeaponSlot = 3;
    public int abilitySlot = 1;
    public int extraWeaponSlotLevel = 0;
    public int extraWeaponSlotLevelMax = 1;
    public int extraWeaponSlotCost = 80000;
    public int strongExtraWeaponSlotLevel = 0;
    public int strongExtraWeaponSlotLevelMax = 1;
    public int strongExtraWeaponSlotCost = 100000;
    public int ExtraAbilitySlotLevel = 0;
    public int ExtraAbilitySlotLevelMax = 1;
    public int ExtraAbilitySlotCost = 160000;
    public int ExtraLuckLevel = 0;
    public int ExtraLuckMax = 20;
    public int XP = 0;
    public int XPMax = 0;
    public float UpgradeCost = 125000;
    public float Upgrade2Cost = 1000000;
    public float ExtraLuckCost = 250;
    public WeaponTemplate.Rarity minNewWeaponRarity = WeaponTemplate.Rarity.Common;
    public WeaponTemplate.Rarity maxNewWeaponRarity = WeaponTemplate.Rarity.Legendary;
    public WeaponTemplate.Rarity minStrongWeaponRarity = WeaponTemplate.Rarity.Common;
    public WeaponTemplate.Rarity maxStrongWeaponRarity = WeaponTemplate.Rarity.Legendary;

    public ChallengeTemplate? activeChallenge = new None(); 
    public ChallengeTemplate selectedChallenge
    {
        get { return activeChallenge; }
        set { activeChallenge = value; }
    }
    public override void OnApplicationStart()
    {
       
        mod = this;

        foreach (var weapon in ModContent.GetContent<WeaponTemplate>().OrderByDescending(c => c.mod == mod))
        {
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Common)
            {
                Common.CommonWpn.Add(weapon.WeaponName);
                Common.CommonImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Uncommon)
            {
                Uncommon.UncommonWpn.Add(weapon.WeaponName);
                Uncommon.UncommonImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Legendary)
            {
                Legendary.LegendaryWpn.Add(weapon.WeaponName);
                Legendary.LegendaryImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Exotic)
            {
                Exotic.ExoticWpn.Add(weapon.WeaponName);
                Exotic.ExoticImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.VoidCommon)
            {
                Common.CommonWpn.Add(weapon.WeaponName);
                Common.CommonImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.VoidUncommon)
            {
                Uncommon.UncommonWpn.Add(weapon.WeaponName);
                Uncommon.UncommonImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.VoidLegendary)
            {
                Legendary.LegendaryWpn.Add(weapon.WeaponName);
                Legendary.LegendaryImg.Add(weapon.Icon);
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Lunar)
            {
                Lunar.LunarWpn.Add(weapon.WeaponName);
                Lunar.LunarImg.Add(weapon.Icon);
            }
        }
        foreach (var ability in ModContent.GetContent<AbilityTemplate>().OrderByDescending(c => c.mod == mod))
        {
            AbilityClass.AbilityName.Add(ability.AbilityName);
            AbilityClass.AbilityImg.Add(ability.Icon);
        }
    }
    
    public void Reset()
    {
        newWeaponCost = 325 * mod.selectedChallenge.NewWeaponCostMult;
        uncommonChance = 90;
        voidChance = 100;
        uncommonChance = 85;
        legendaryChance = 99;
        exoticChance = 100;
        voidChance = 100;
        lunarChance = 100;
        uncommonStrongChance = 0;
        voidStrongChance = 100;
        legendaryStrongChance = 80;
        lunarStrongChance = 100;
        baseNewWeaponCost = 250 * mod.selectedChallenge.NewWeaponCostMult;
        strongerWeaponCost = 500 * mod.selectedChallenge.StrongerWeaponCostMult;
        baseStrongerWeaponCost = 500 * mod.selectedChallenge.StrongerWeaponCostMult;
        newAbilityCost = 7500 * mod.selectedChallenge.AbilityWeaponCostMult;
        baseNewAbilityCost = 1000 * mod.selectedChallenge.AbilityWeaponCostMult;
        upgradeOpen = false;
        selectingWeaponOpen = false;
        mib = false;
        panelOpen = false;
        level = 0;
        newWeaponSlot = 3;
        strongWeaponSlot = 3;
        abilitySlot = 1;
        extraWeaponSlotLevel = 0;
        extraWeaponSlotCost = 50000;
        strongExtraWeaponSlotLevel = 0;
        strongExtraWeaponSlotCost = 65000;
        ExtraAbilitySlotLevel = 0;
        ExtraAbilitySlotCost = 100000;
        ExtraLuckCost = 700 * mod.selectedChallenge.LuckCostMult;
        ExtraLuckLevel = 0;
        minNewWeaponRarity = mod.selectedChallenge.MinNURarity;
        maxNewWeaponRarity = mod.selectedChallenge.MaxNURarity;
        minStrongWeaponRarity = mod.selectedChallenge.MinNURarity;
        maxStrongWeaponRarity = mod.selectedChallenge.MaxNURarity;
        UpgradeCost = 125000 * mod.selectedChallenge.UpgradeCostMult;
        Upgrade2Cost = 1000000 * mod.selectedChallenge.UpgradeCostMult;
        XP = 0;
        XPMax = 0;
    }
    public override void OnGameModelLoaded(GameModel model)
    {
        Reset();
    }
    public override void OnTowerSold(Tower tower, float amount)
    {
        if (tower.towerModel.name.Contains("AncientMonkey-Commando"))
        {
            Reset();
        }
    }
    public override void OnRestart()
    {
        MenuUi.instance.CloseMenu();
    }
    public override void OnTowerCreated(Tower tower, Entity target, Model modelToUse)
    {
        if (tower.towerModel.name.Contains("AncientMonkey-Commando"))
        {
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            minNewWeaponRarity = WeaponTemplate.Rarity.Common;
            maxNewWeaponRarity = WeaponTemplate.Rarity.Common;
            newWeaponSlot = 5;
            MenuUi.NewWeaponPanel(rect, tower, false);
            Reset();
        }
    }
    public override void OnNewGameModel(GameModel result)
    {
        foreach (var tower in result.towerSet.ToList())
        {
            if (tower.name.Contains("AncientMonkey-Commando"))
            {
                tower.GetShopTowerDetails().towerCount = 1;
            }
        }
    }
    public override void OnTowerSelected(Tower tower)
    {
        if (tower.towerModel.name.Contains("AncientMonkey-Commando"))
        {
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            MenuUi.CreateUpgradeMenu(rect, tower);
            upgradeOpen = true;
        }
    }
    public override void OnTowerDeselected(Tower tower)
    {
        if (tower.towerModel.name.Contains("AncientMonkey-Commando"))
        {
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            upgradeOpen = false;
            if (MenuUi.instance)
            {
                MenuUi.instance.CloseMenu();
            }

        }
    }
    [RegisterTypeInIl2Cpp(false)]
    public class MenuUi : MonoBehaviour
    {
        public static MenuUi instance;

        public ModHelperInputField input;
        public void CloseMenu()
        {
            if(gameObject)
            {
                Destroy(gameObject);
            }
        }
        public void NewWeapon(Tower tower)
        {
            InGame game = InGame.instance;
            if (SandboxMode)
            {
                RectTransform rect = game.uiRect;
                MenuUi.NewWeaponPanel(rect, tower, false);
                MenuUi.instance.CloseMenu();
                return;
            }
            if (game.GetCash() >= mod.newWeaponCost)
            {
                game.AddCash(-mod.newWeaponCost);
                RectTransform rect = game.uiRect;
                mod.newWeaponCost += mod.baseNewWeaponCost;
                tower.worth += mod.newWeaponCost - mod.baseNewWeaponCost;
                mod.baseNewWeaponCost *= 1.06f;
                MenuUi.NewWeaponPanel(rect, tower,false);
                MenuUi.instance.CloseMenu();
            }
        }
        public void WeaponSelected(string Weapon, Tower tower,bool levelup)
        {
            mod.panelOpen = false;
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            if(!SandboxMode)
            {
                Destroy(gameObject);
            }
       
            foreach (var weapon in ModContent.GetContent<WeaponTemplate>().OrderByDescending(c => c.mod == mod))
            {
                if(weapon.WeaponName == Weapon)
                {
                    weapon.EditTower(tower);
                }
            }
            mod.XP += 1;
            if (mod.XP >= mod.XPMax && mod.level >= 2)
            {
                mod.XPMax += 2;
                NewWeaponPanel(rect, tower, true);

                return;
            }
            //mod.rareChance -= 5f + mod.ExtraLuckLevel * 0.45f;
            //mod.epicChance -= 1.45f + mod.ExtraLuckLevel * 0.1f;
            //if (mod.epicChance <= 88)
            //{
                //mod.legendaryChance -= 0.85f + mod.ExtraLuckLevel * 0.06f;
            //}
            //if (mod.legendaryChance <= 91)
            //{
                //mod.exoticChance -= 0.50f + mod.ExtraLuckLevel * 0.04f;
            //}
            //if (mod.level >= 1)
            //{
                //if (mod.exoticChance <= 94)
                //{
                    //mod.godlyChance -= 0.3f + mod.ExtraLuckLevel * 0.025f;
                //}
            //}
            //if (mod.level >= 2)
            //{
                //if (mod.godlyChance <= 95)
                //{
                    //mod.omegaChance -= 0.15f + mod.ExtraLuckLevel * 0.01f;
                //}
            //}
            mod.XP += 1;
          
            if (mod.upgradeOpen == true && !SandboxMode)
            {
                CreateUpgradeMenu(rect, tower);
            }
            
            mod.selectingWeaponOpen = false;
            if (mod.mib)
            {
                var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
                towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
                foreach (var weaponModel in towerModel.GetDescendants<WeaponModel>().ToArray())
                {
                    if (weaponModel.projectile.HasBehavior<DamageModel>())
                    {
                        weaponModel.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                    if (weaponModel.projectile.HasBehavior<CreateProjectileOnContactModel>())
                    {
                        weaponModel.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                    if (weaponModel.projectile.HasBehavior<CreateProjectileOnExhaustFractionModel>())
                    {
                        weaponModel.projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                }
                tower.UpdateRootModel(towerModel);
            }
          
        }
        public static ModHelperPanel CreateWeapon(WeaponTemplate weapon, Tower tower )
        {
            var sprite = VanillaSprites.GreyInsertPanel; //common

            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Uncommon)
            {
                sprite = VanillaSprites.InsertPanelWhite;
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Lunar) //|| weapon.WeaponRarity == WeaponTemplate.Rarity.LunarEquipment) //luner
            {
                sprite = VanillaSprites.BlueInsertPanel;
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.VoidCommon || weapon.WeaponRarity == WeaponTemplate.Rarity.VoidUncommon || weapon.WeaponRarity == WeaponTemplate.Rarity.VoidLegendary || weapon.WeaponRarity == WeaponTemplate.Rarity.VoidExotic) //void
            {
                sprite = VanillaSprites.MainBgPanelParagon;
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Legendary) 
            {
                sprite = VanillaSprites.InsertPanelWhite; // 
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Exotic)
            {
                sprite = VanillaSprites.MainBGPanelYellow;
            }
            if (weapon.WeaponRarity == WeaponTemplate.Rarity.Uncommon)
            {
                sprite = VanillaSprites.InsertPanelWhite;
            }
            var panel = ModHelperPanel.Create(new Info("WeaponContent" + weapon.WeaponName, 0, 0, 2250, 150), sprite);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText wpnName = panel.AddText(new Info("wpnName", -600, 0, 1000, 150), weapon.WeaponName, 80, TextAlignmentOptions.MidlineLeft);
            ModHelperText rarity = panel.AddText(new Info("rarity", 275, 0, 600, 150), weapon.WeaponRarity.ToString(), 80, TextAlignmentOptions.MidlineLeft);
            ModHelperImage image = panel.AddImage(new Info("image", -100, 0, 140, 140), weapon.Icon);
            ModHelperButton selectWpnBtn = panel.AddButton(new Info("selectWpnBtn", 900, 0, 400, 120), VanillaSprites.GreenBtnLong, new System.Action(() => { upgradeUi.WeaponSelected(weapon.WeaponName, tower, false);}) ) ;
            //if(weapon.IsCamo)
            //{
                //ModHelperImage camoImg = panel.AddImage(new Info("camoImg", 460, 0, 120, 120), VanillaSprites.CamoBloonIcon);
            //}
            //if (weapon.IsLead)
            //{
                //ModHelperImage leadImg = panel.AddImage(new Info("leadImg", 580, 0, 120, 120), VanillaSprites.LeadBloonIcon);
            //}
            ModHelperText selectWpn = selectWpnBtn.AddText(new Info("selectWpn", 0, 0, 700, 160), "Select", 60);
            return panel;
        }
        public static void SandBoxWeaponPanel(RectTransform rect, Tower tower)
        {
          
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, 2500, 1850, new UnityEngine.Vector2()), VanillaSprites.MainBGPanelBlue);
            panel.transform.DestroyAllChildren();            
            ModHelperScrollPanel scrollPanel = panel.AddScrollPanel(new Info("scrollPanel", 0, 0, 2500, 1850), RectTransform.Axis.Vertical, VanillaSprites.MainBGPanelBlue, 15, 50);
            ModHelperButton exit = panel.AddButton(new Info("exit", 1200, 900, 135, 135), VanillaSprites.RedBtn, new System.Action(() => {
                tower.SetSelectionBlocked(false); panel.DeleteObject(); if (mod.upgradeOpen == true){CreateUpgradeMenu(rect, tower);  }
            }));
            ModHelperText x = exit.AddText(new Info("x", 0, 0, 700, 160), "X", 80);

            for (int i = 1; i < 100; i++)
            {
                foreach (var weapon in ModContent.GetContent<WeaponTemplate>())
                {
                    if (weapon.SandboxIndex == i)
                    {
                        scrollPanel.AddScrollContent(CreateWeapon(weapon, tower));
                    }
                }
            }
        }
       
        public static void NewWeaponPanel(RectTransform rect, Tower tower, bool Levelup)
        {
            mod.panelOpen = true;
            if(instance)
            {
                instance.CloseMenu();
            }
          
            if(SandboxMode)
            {
                SandBoxWeaponPanel(rect, tower);
                return;
            }
            float weaponPanelWidth = 833.33f;
            float weaponPanelX = 412.5f;
            float weaponPanelY = 900;
            float wpnContentX = 25 - (mod.newWeaponSlot -1) * 425;
            float panelWidth = mod.newWeaponSlot * weaponPanelWidth;
            var imag = VanillaSprites.BrownInsertPanel;
            if (mod.level == 1)
            {
                imag = VanillaSprites.BlueInsertPanel;
            }
            if (mod.level == 2)
            {
                imag = VanillaSprites.MainBgPanelParagon;
            }
            ModHelperPanel panel =  rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, panelWidth, 1850, new UnityEngine.Vector2()), imag);

            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText selectWpn = panel.AddText(new Info("selectWpn", 0, 800, 2500, 180), "Select New Weapon", 100);
            if (!instance)
            {
                selectWpn.Text.text = "Choose A starter Weapon";
                selectWpn.Text.color = new Color(0, 1, 0);
            }
            if (Levelup)
            {
                selectWpn.Text.text = "Choose A level up Weapon";
                selectWpn.Text.color = new Color(0.46f, 0, 0.78f);
            }
            Il2CppSystem.Random rnd = new Il2CppSystem.Random();
            for (int i = 0; i < mod.newWeaponSlot; i++)
            {
                var WpnRarityNum = rnd.Next(1, 100);
                var WpnRarity = "Common";
                var RarityNumber = 1;
                var MinNum = 1;
                var MaxNum = 1;
                var CanVoid = false;
                // should only be [Common to Legendary] or [VoidCommon to VoidLegendary]
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Common)
                {
                    MinNum = 1;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Uncommon)
                {
                    MinNum = 2;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Legendary)
                {
                    MinNum = 3;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Exotic)
                {
                    MinNum = 4;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Lunar)
                {
                    MinNum = 5;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidCommon)
                {
                    MinNum = 6;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidUncommon)
                {
                    MinNum = 7;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidLegendary)
                {
                    MinNum = 8;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidExotic)
                {
                    MinNum = 9;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Common)
                {
                    MaxNum = 1;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Uncommon)
                {
                    MaxNum = 2;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Legendary)
                {
                    MaxNum = 3;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Exotic)
                {
                    MaxNum = 4;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Lunar)
                {
                    MaxNum = 5;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidCommon)
                {
                    MaxNum = 6;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidUncommon)
                {
                    MaxNum = 7;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidLegendary)
                {
                    MaxNum = 8;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidExotic)
                {
                    MaxNum = 9;
                }
                //if (WpnRarityNum >= mod.rareChance)
                //{
                //RarityNumber = 2;
                //}

                if (WpnRarityNum >= mod.uncommonChance)
                {
                    RarityNumber = 2;
                }
                if (WpnRarityNum >= mod.legendaryChance)
                {
                    RarityNumber = 3;
                }
                if (WpnRarityNum >= mod.exoticChance)
                {
                    RarityNumber = 4;
                }
                if (WpnRarityNum >= mod.voidChance)
                {
                    RarityNumber = 5;
                }
                if (RarityNumber < MinNum)
                {
                    RarityNumber = MinNum;
                }
                if (RarityNumber > MaxNum)
                {
                    RarityNumber = MaxNum;
                }
                if (RarityNumber == 2)
                {
                     WpnRarity = "Uncommon";
                }
                if (RarityNumber == 3)
                {
                    WpnRarity = "Legendary";
                }
                if (RarityNumber == 4)
                {
                    WpnRarity = "Exotic";
                }
                if (RarityNumber == 5)
                {
                    WpnRarity = "Lunar";
                }

                var sprite = VanillaSprites.GreyInsertPanel;
                var numWpn = rnd.Next(0, Common.CommonWpn.Count);
                var weapon = Common.CommonWpn[numWpn];
                var img = Common.CommonImg[numWpn];

                if (WpnRarity == "Uncommon")
                {
                    numWpn = rnd.Next(0, Uncommon.UncommonWpn.Count);
                    sprite = VanillaSprites.MainBgPanelWhiteSmall;
                    weapon = Uncommon.UncommonWpn[numWpn];
                    img = Uncommon.UncommonImg[numWpn];
                }
                if (WpnRarity == "Legendary")
                {
                    numWpn = rnd.Next(0, Legendary.LegendaryWpn.Count);
                    sprite = VanillaSprites.MainBGPanelYellow;
                    weapon = Legendary.LegendaryWpn[numWpn];
                    img = Legendary.LegendaryImg[numWpn];
                }
                if (WpnRarity == "Exotic")
                {
                    numWpn = rnd.Next(0, Exotic.ExoticWpn.Count);
                    sprite = VanillaSprites.MainBGPanelYellow;
                    weapon = Exotic.ExoticWpn[numWpn];
                    img = Exotic.ExoticImg[numWpn];
                }
                if (WpnRarity == "Lunar")
                {
                    numWpn = rnd.Next(0, Lunar.LunarWpn.Count);
                    sprite = VanillaSprites.BlueInsertPanel;
                    weapon = Lunar.LunarWpn[numWpn];
                    img = Lunar.LunarImg[numWpn];
                }
                ModHelperPanel wpnPanel = panel.AddPanel(new Info("wpnPanel", weaponPanelX, weaponPanelY, 650, 1450, new UnityEngine.Vector2()), sprite);
                ModHelperText rarityText = panel.AddText(new Info("rarityText", wpnContentX, 600, 800, 180), WpnRarity, 100);
                ModHelperText weaponText = panel.AddText(new Info("weaponText", wpnContentX, 500, 800, 180), weapon, 75);
                
                ModHelperImage image = panel.AddImage(new Info("image", wpnContentX, 0, 400, 400), img);
                ModHelperButton selectWpnBtn = panel.AddButton(new Info("selectWpnBtn", wpnContentX, -600, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.WeaponSelected(weapon, tower,Levelup)));
                ModHelperText selectWpnTxt = selectWpnBtn.AddText(new Info("selectWpnTxt", 0, 0, 700, 160), "Select", 70);
                foreach (var weaponContent in ModContent.GetContent<WeaponTemplate>().OrderByDescending(c => c.mod == mod))
                {
                    if (weaponContent.WeaponName == weapon)
                    {
                        if (weaponContent.IsModded)
                        {
                            weaponText = panel.AddText(new Info("weaponText", wpnContentX, 500, 800, 180), weapon + " (" + weaponContent.ModName + " Mod)", 75);
                        }   
                        if (weaponContent.IsCamo && !weaponContent.IsLead)
                        {
                            ModHelperImage camoImg = panel.AddImage(new Info("camoImg", wpnContentX + 275, 650, 100, 100), VanillaSprites.CamoBloonIcon);
                        }
                        if (weaponContent.IsLead && !weaponContent.IsCamo)
                        {
                            ModHelperImage leadImg = panel.AddImage(new Info("leadImg", wpnContentX + 275, 650, 100, 100), VanillaSprites.LeadBloonIcon);
                        }
                        if (weaponContent.IsLead && weaponContent.IsCamo)
                        {
                            ModHelperImage camoImg = panel.AddImage(new Info("camoImg", wpnContentX + 275, 650, 100, 100), VanillaSprites.CamoBloonIcon);
                            ModHelperImage leadImg = panel.AddImage(new Info("leadImg", wpnContentX + 275, 560, 100, 100), VanillaSprites.LeadBloonIcon);
                        }
                    }
                }
                weaponPanelX += weaponPanelWidth;
                wpnContentX += weaponPanelWidth;
               
            }
        }
        public static void NewLargeWeaponPanel(RectTransform rect, Tower tower, bool Levelup)
        {
            mod.panelOpen = true;
            if (instance)
            {
                instance.CloseMenu();
            }

            if (SandboxMode)
            {
                SandBoxWeaponPanel(rect, tower);
                return;
            }
            float weaponPanelWidth = 833.33f;
            float weaponPanelX = 412.5f;
            float weaponPanelY = 900;
            float wpnContentX = 25 - (mod.newWeaponSlot - 1) * 425;
            float panelWidth = mod.newWeaponSlot * weaponPanelWidth;
            var imag = VanillaSprites.BrownInsertPanel;
            if (mod.level == 1)
            {
                imag = VanillaSprites.BlueInsertPanel;
            }
            if (mod.level == 2)
            {
                imag = VanillaSprites.MainBgPanelParagon;
            }
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, panelWidth, 1850, new UnityEngine.Vector2()), imag);

            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText selectWpn = panel.AddText(new Info("selectWpn", 0, 800, 2500, 180), "Select New Weapon", 100);
            if (!instance)
            {
                selectWpn.Text.text = "Choose A starter Weapon";
                selectWpn.Text.color = new Color(0, 1, 0);
            }
            if (Levelup)
            {
                selectWpn.Text.text = "Choose A level up Weapon";
                selectWpn.Text.color = new Color(0.46f, 0, 0.78f);
            }
            Il2CppSystem.Random rnd = new Il2CppSystem.Random();
            for (int i = 0; i < mod.newWeaponSlot; i++)
            {
                var WpnRarityNum = rnd.Next(1, 100);
                var WpnRarity = "Common";
                var RarityNumber = 1;
                var MinNum = 1;
                var MaxNum = 1;
                var CanVoid = false;
                // should only be [Common to Legendary] or [VoidCommon to VoidLegendary]
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Common)
                {
                    MinNum = 1;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Uncommon)
                {
                    MinNum = 2;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Legendary)
                {
                    MinNum = 3;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Exotic)
                {
                    MinNum = 4;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Lunar)
                {
                    MinNum = 5;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidCommon)
                {
                    MinNum = 6;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidUncommon)
                {
                    MinNum = 7;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidLegendary)
                {
                    MinNum = 8;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.VoidExotic)
                {
                    MinNum = 9;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Common)
                {
                    MaxNum = 1;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Uncommon)
                {
                    MaxNum = 2;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Legendary)
                {
                    MaxNum = 3;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Exotic)
                {
                    MaxNum = 4;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Lunar)
                {
                    MaxNum = 5;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidCommon)
                {
                    MaxNum = 6;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidUncommon)
                {
                    MaxNum = 7;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidLegendary)
                {
                    MaxNum = 8;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.VoidExotic)
                {
                    MaxNum = 9;
                }
                //if (WpnRarityNum >= mod.rareChance)
                //{
                //RarityNumber = 2;
                //}

                if (WpnRarityNum >= mod.uncommonChance)
                {
                    RarityNumber = 2;
                }
                if (WpnRarityNum >= mod.legendaryChance)
                {
                    RarityNumber = 3;
                }
                if (WpnRarityNum >= mod.exoticChance)
                {
                    RarityNumber = 4;
                }
                if (WpnRarityNum >= mod.voidChance)
                {
                    RarityNumber = 5;
                }
                if (RarityNumber < MinNum)
                {
                    RarityNumber = MinNum;
                }
                if (RarityNumber > MaxNum)
                {
                    RarityNumber = MaxNum;
                }
                if (RarityNumber == 2)
                {
                    WpnRarity = "Uncommon";
                }
                if (RarityNumber == 3)
                {
                    WpnRarity = "Legendary";
                }
                if (RarityNumber == 4)
                {
                    WpnRarity = "Exotic";
                }
                if (RarityNumber == 5)
                {
                    WpnRarity = "Lunar";
                }

                var sprite = VanillaSprites.GreyInsertPanel;
                var numWpn = rnd.Next(0, Common.CommonWpn.Count);
                var weapon = Common.CommonWpn[numWpn];
                var img = Common.CommonImg[numWpn];

                if (WpnRarity == "Uncommon")
                {
                    numWpn = rnd.Next(0, Uncommon.UncommonWpn.Count);
                    sprite = VanillaSprites.MainBgPanelWhiteSmall;
                    weapon = Uncommon.UncommonWpn[numWpn];
                    img = Uncommon.UncommonImg[numWpn];
                }
                if (WpnRarity == "Legendary")
                {
                    numWpn = rnd.Next(0, Legendary.LegendaryWpn.Count);
                    sprite = VanillaSprites.MainBGPanelYellow;
                    weapon = Legendary.LegendaryWpn[numWpn];
                    img = Legendary.LegendaryImg[numWpn];
                }
                if (WpnRarity == "Exotic")
                {
                    numWpn = rnd.Next(0, Exotic.ExoticWpn.Count);
                    sprite = VanillaSprites.MainBGPanelYellow;
                    weapon = Exotic.ExoticWpn[numWpn];
                    img = Exotic.ExoticImg[numWpn];
                }
                if (WpnRarity == "Lunar")
                {
                    numWpn = rnd.Next(0, Lunar.LunarWpn.Count);
                    sprite = VanillaSprites.BlueInsertPanel;
                    weapon = Lunar.LunarWpn[numWpn];
                    img = Lunar.LunarImg[numWpn];
                }
                ModHelperPanel wpnPanel = panel.AddPanel(new Info("wpnPanel", weaponPanelX, weaponPanelY, 650, 1450, new UnityEngine.Vector2()), sprite);
                ModHelperText rarityText = panel.AddText(new Info("rarityText", wpnContentX, 600, 800, 180), WpnRarity, 100);
                ModHelperText weaponText = panel.AddText(new Info("weaponText", wpnContentX, 500, 800, 180), weapon, 75);

                ModHelperImage image = panel.AddImage(new Info("image", wpnContentX, 0, 400, 400), img);
                ModHelperButton selectWpnBtn = panel.AddButton(new Info("selectWpnBtn", wpnContentX, -600, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.WeaponSelected(weapon, tower, Levelup)));
                ModHelperText selectWpnTxt = selectWpnBtn.AddText(new Info("selectWpnTxt", 0, 0, 700, 160), "Select", 70);
                foreach (var weaponContent in ModContent.GetContent<WeaponTemplate>().OrderByDescending(c => c.mod == mod))
                {
                    if (weaponContent.WeaponName == weapon)
                    {
                        if (weaponContent.IsModded)
                        {
                            weaponText = panel.AddText(new Info("weaponText", wpnContentX, 500, 800, 180), weapon + " (" + weaponContent.ModName + " Mod)", 75);
                        }
                        if (weaponContent.IsCamo && !weaponContent.IsLead)
                        {
                            ModHelperImage camoImg = panel.AddImage(new Info("camoImg", wpnContentX + 275, 650, 100, 100), VanillaSprites.CamoBloonIcon);
                        }
                        if (weaponContent.IsLead && !weaponContent.IsCamo)
                        {
                            ModHelperImage leadImg = panel.AddImage(new Info("leadImg", wpnContentX + 275, 650, 100, 100), VanillaSprites.LeadBloonIcon);
                        }
                        if (weaponContent.IsLead && weaponContent.IsCamo)
                        {
                            ModHelperImage camoImg = panel.AddImage(new Info("camoImg", wpnContentX + 275, 650, 100, 100), VanillaSprites.CamoBloonIcon);
                            ModHelperImage leadImg = panel.AddImage(new Info("leadImg", wpnContentX + 275, 560, 100, 100), VanillaSprites.LeadBloonIcon);
                        }
                    }
                }
                weaponPanelX += weaponPanelWidth;
                wpnContentX += weaponPanelWidth;

            }
        }
        public static void SandBoxStrongWeaponPanel(RectTransform rect, Tower tower)
        {
          
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, 1250, 1500, new UnityEngine.Vector2()), VanillaSprites.MainBGPanelBlue);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            panel.transform.DestroyAllChildren();
            if (mod.damageBoostSandbox < 0)
            {
                mod.damageBoostSandbox = 0;
            }
            if (mod.pierceBoostSandbox < 0)
            {
                mod.pierceBoostSandbox = 0;
            }
            if (mod.rangeBoostSandbox <= 0.9)
            {
                mod.rangeBoostSandbox = 1f;
            }
            if (mod.attackSpeedBoostSandbox <= 0)
            {
                mod.attackSpeedBoostSandbox = 0.05f;
            }
            if (mod.attackSpeedBoostSandbox >= 1.05f)
            {
                mod.attackSpeedBoostSandbox = 1f;
            }
            if (mod.moneyBoostSandbox <= 0.9)
            {
                mod.moneyBoostSandbox = 1f;
            }

            mod.rangeBoostSandbox = Mathf.Round(mod.rangeBoostSandbox * 10) / 10;
            mod.attackSpeedBoostSandbox = Mathf.Round(mod.attackSpeedBoostSandbox * 100) / 100;
            mod.moneyBoostSandbox = Mathf.Round(mod.moneyBoostSandbox * 10) / 10;

            panel.AddText(new Info("text", 300, 350, 1200, 150), "Damage Boost: " + mod.damageBoostSandbox, 75, TextAlignmentOptions.TopLeft);
            panel.AddButton(new Info("button1", -400, 430, 100, 100), VanillaSprites.AddMoreBtn, new System.Action(() => { mod.damageBoostSandbox += 1; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));
            panel.AddButton(new Info("button2", -400, 350, 100, 100), VanillaSprites.AddRemoveBtn, new System.Action(() => { mod.damageBoostSandbox -= 1; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));

            panel.AddText(new Info("text2", 300, 150, 1200, 150), "Pierce Boost: " + mod.pierceBoostSandbox, 75, TextAlignmentOptions.TopLeft);
            panel.AddButton(new Info("button3", -400, 230, 100, 100), VanillaSprites.AddMoreBtn, new System.Action(() => { mod.pierceBoostSandbox += 1; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));
            panel.AddButton(new Info("button4", -400, 150, 100, 100), VanillaSprites.AddRemoveBtn, new System.Action(() => { mod.pierceBoostSandbox -= 1; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));

            panel.AddText(new Info("text2", 300, -50, 1200, 150), "Range Boost: " + mod.rangeBoostSandbox, 75, TextAlignmentOptions.TopLeft);
            panel.AddButton(new Info("button3", -400, 30, 100, 100), VanillaSprites.AddMoreBtn, new System.Action(() => { mod.rangeBoostSandbox += 0.1f; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));
            panel.AddButton(new Info("button4", -400, -50, 100, 100), VanillaSprites.AddRemoveBtn, new System.Action(() => { mod.rangeBoostSandbox -= 0.1f; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));

            panel.AddText(new Info("text2", 300, -250, 1200, 150), "Attack Speed Boost: " + mod.attackSpeedBoostSandbox, 75, TextAlignmentOptions.TopLeft);
            panel.AddButton(new Info("button3", -400, -170, 100, 100), VanillaSprites.AddMoreBtn, new System.Action(() => { mod.attackSpeedBoostSandbox -= 0.05f; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));
            panel.AddButton(new Info("button4", -400, -250, 100, 100), VanillaSprites.AddRemoveBtn, new System.Action(() => { mod.attackSpeedBoostSandbox += 0.05f; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));

            panel.AddText(new Info("text2", 300, -450, 1200, 150), "Money Boost: " + mod.moneyBoostSandbox, 75, TextAlignmentOptions.TopLeft);
            panel.AddButton(new Info("button3", -400, -370, 100, 100), VanillaSprites.AddMoreBtn, new System.Action(() => { mod.moneyBoostSandbox += 0.1f; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));
            panel.AddButton(new Info("button4", -400, -450, 100, 100), VanillaSprites.AddRemoveBtn, new System.Action(() => { mod.moneyBoostSandbox -= 0.1f; panel.DeleteObject(); ; SandBoxStrongWeaponPanel(rect, tower); }));

            ModHelperButton selectWpnBtn = panel.AddButton(new Info("selectWpnBtn", 0, -600, 400, 120), VanillaSprites.GreenBtnLong, new System.Action(() => { upgradeUi.StrongWpnSelected(mod.damageBoostSandbox, mod.pierceBoostSandbox, mod.attackSpeedBoostSandbox, mod.rangeBoostSandbox, mod.moneyBoostSandbox, tower); panel.DeleteObject(); }));
            ModHelperText selectWpn = selectWpnBtn.AddText(new Info("selectWpn", 0, 0, 700, 160), "Edit", 70);
        }
        public static void StrongWeaponPanel(RectTransform rect, Tower tower)
        {
            mod.panelOpen = true;
            if (SandboxMode)
            {
                SandBoxStrongWeaponPanel(rect, tower);
                return;
            }
            float strongWeaponPanelWidth = 833.33f;
            float strongWeaponPanelX = 412.5f;
            float strongWeaponPanelY = 900;
            float strongWpnContentX = 25 - (mod.strongWeaponSlot - 1) * 425;
            float panelWidth = mod.strongWeaponSlot * strongWeaponPanelWidth;
            var img = VanillaSprites.BrownInsertPanel;
            if (mod.level == 1)
            {
                img = VanillaSprites.BlueInsertPanel;
            }
            if (mod.level == 2)
            {
                img = VanillaSprites.MainBgPanelParagon;
            }
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, panelWidth, 1850, new UnityEngine.Vector2()), img);
            ModHelperText selectStrongWpn = panel.AddText(new Info("selectStrongWpn", 0, 800, 2500, 180), "Select Stronger Weapon Card", 100);
            Il2CppSystem.Random rnd = new Il2CppSystem.Random();

            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            for (int i = 0; i < mod.strongWeaponSlot; i++)
            {
                var StrongWpnRarityNum = rnd.Next(1, 100);
                var StrongWpnRarity = "Common";
                var RarityNumber = 1;
                var MinNum = 1;
                var MaxNum = 1;
                var damageBoost = rnd.Next(0, 2);
                var pierceBoost = rnd.Next(0, 2);
                float rangeBoost = rnd.Next(0, 7);
                float attackSpeedBoost = rnd.Next(95, 101);
                float moneyBoost = rnd.Next(0, 7);
                var sprite = VanillaSprites.GreyInsertPanel;


                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Common)
                {
                    MinNum = 1;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Legendary)
                {
                    MinNum = 4;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Exotic)
                {
                    MinNum = 5;
                }
                if (mod.minNewWeaponRarity == WeaponTemplate.Rarity.Common)
                {
                    MaxNum = 1;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Legendary)
                {
                    MaxNum = 4;
                }
                if (mod.maxNewWeaponRarity == WeaponTemplate.Rarity.Exotic)
                {
                    MaxNum = 5;
                }
                if (StrongWpnRarityNum >= mod.legendaryStrongChance)
                {
                    RarityNumber = 4;
                }
                if (RarityNumber < MinNum)
                {
                    RarityNumber = MinNum;
                }
                if (RarityNumber > MaxNum)
                {
                    RarityNumber = MaxNum;
                }
                if (RarityNumber == 2)
                {
                    StrongWpnRarity = "Rare";
                }
                if (RarityNumber == 3)
                {
                    StrongWpnRarity = "Epic";
                }
                if (RarityNumber == 4)
                {
                    StrongWpnRarity = "Legendary";
                }
                if (RarityNumber == 5)
                {
                    StrongWpnRarity = "Exotic";
                }
                if (RarityNumber == 6)
                {
                    StrongWpnRarity = "Godly";
                }
                if (RarityNumber == 7)
                {
                    StrongWpnRarity = "Omega";
                }
                if (StrongWpnRarity == "Rare")
                {
                    damageBoost = rnd.Next(0, 3);
                    pierceBoost = rnd.Next(0, 3);
                    rangeBoost = rnd.Next(0, 10);
                    attackSpeedBoost = rnd.Next(91, 101);
                    moneyBoost = rnd.Next(0, 10);
                    sprite = VanillaSprites.BlueInsertPanel;
                }
                if (StrongWpnRarity == "Epic")
                {
                    damageBoost = rnd.Next(1, 5);
                    pierceBoost = rnd.Next(1, 5);
                    rangeBoost = rnd.Next(0, 13);
                    attackSpeedBoost = rnd.Next(88, 101);
                    moneyBoost = rnd.Next(0, 13);
                    sprite = VanillaSprites.MainBgPanelParagon;
                }
                if (StrongWpnRarity == "Legendary")
                {
                    damageBoost = rnd.Next(2, 7);
                    pierceBoost = rnd.Next(2, 7);
                    rangeBoost = rnd.Next(0, 15);
                    attackSpeedBoost = rnd.Next(85, 101);
                    moneyBoost = rnd.Next(0, 15);
                    sprite = VanillaSprites.MainBGPanelYellow;
                }
                if (StrongWpnRarity == "Exotic")
                {
                    damageBoost = rnd.Next(3, 9);
                    pierceBoost = rnd.Next(3, 9);
                    rangeBoost = rnd.Next(0, 18);
                    attackSpeedBoost = rnd.Next(83, 101);
                    moneyBoost = rnd.Next(0, 18);
                    sprite = VanillaSprites.MainBgPanelWhiteSmall;
                }
                if (StrongWpnRarity == "Godly")
                {
                    damageBoost = rnd.Next(5, 15);
                    pierceBoost = rnd.Next(5, 15);
                    rangeBoost = rnd.Next(0, 23);
                    attackSpeedBoost = rnd.Next(75, 101);
                    moneyBoost = rnd.Next(0, 23);
                    sprite = VanillaSprites.MainBGPanelSilver;
                }
                if (StrongWpnRarity == "Omega")
                {
                    damageBoost = rnd.Next(8, 22);
                    pierceBoost = rnd.Next(8, 22);
                    rangeBoost = rnd.Next(0, 28);
                    attackSpeedBoost = rnd.Next(71, 101);
                    moneyBoost = rnd.Next(0, 28);
                    sprite = VanillaSprites.MainBgPanelHematite;
                }
                attackSpeedBoost = attackSpeedBoost / 100;
                rangeBoost = 1 + rangeBoost / 100;
                moneyBoost = 1 + moneyBoost / 100;
                ModHelperPanel strongWpnPanel = panel.AddPanel(new Info("strongWpnPanel", strongWeaponPanelX, strongWeaponPanelY, 650, 1450, new UnityEngine.Vector2()), sprite);
                
                ModHelperText rarityText = panel.AddText(new Info("rarityText", strongWpnContentX, 600, 800, 180), StrongWpnRarity, 100);
                ModHelperText cardText = panel.AddText(new Info("cardText", strongWpnContentX, 500, 800, 180), "Stronger Weapon Card", 50);
                ModHelperText dmgBoostText = panel.AddText(new Info("dmgBoostText", strongWpnContentX, 200, 800, 180), "Damage Boost :" + damageBoost, 50);
                ModHelperText prcBoostText = panel.AddText(new Info("prcBoostText", strongWpnContentX, 100, 800, 180), "Pierce Boost :" + pierceBoost, 50);
                ModHelperText rngBoostText = panel.AddText(new Info("rngBoostText", strongWpnContentX, 0, 800, 180), "Range Boost : X" + rangeBoost, 50);
                ModHelperText atkspdBoostText = panel.AddText(new Info("atkspdBoostText", strongWpnContentX, -100, 800, 180), "Attack Speed Boost : X" + attackSpeedBoost, 50);
                ModHelperText mnyBoostText = panel.AddText(new Info("mnyBoostText", strongWpnContentX, -200, 800, 180), "Money Boost : X" + moneyBoost, 50);
                ModHelperButton selectWpnBtn = panel.AddButton(new Info("selectWpnBtn", strongWpnContentX, -600, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.StrongWpnSelected(damageBoost, pierceBoost, attackSpeedBoost, rangeBoost, moneyBoost, tower)));
                ModHelperText selectWpn = selectWpnBtn.AddText(new Info("selectWpn", 0, 0, 700, 160), "Select", 70);
                strongWeaponPanelX += strongWeaponPanelWidth;
                strongWpnContentX += strongWeaponPanelWidth;
            }
        }
        public void StrongWeapon(Tower tower)
        {
            InGame game = InGame.instance;
            if (SandboxMode)
            {
                RectTransform rect = game.uiRect;
                MenuUi.NewLargeWeaponPanel(rect, tower, false);
                MenuUi.instance.CloseMenu();

                return;
            }

            if (game.GetCash() >= mod.strongerWeaponCost)
            {
                game.AddCash(-mod.strongerWeaponCost);
                RectTransform rect = game.uiRect;
                MenuUi.NewLargeWeaponPanel(rect, tower, false);
                mod.strongerWeaponCost += mod.baseStrongerWeaponCost;
                tower.worth += mod.strongerWeaponCost - mod.baseStrongerWeaponCost;
                mod.baseStrongerWeaponCost *= 1.12f;
                
         
                MenuUi.instance.CloseMenu();

            }
        }
        public void StrongWpnSelected(int Dmg, int Pierce, float AtkSpd, float Range, float Money, Tower tower)
        {
            mod.panelOpen = false;
            InGame game = InGame.instance;
            Destroy(gameObject);
            RectTransform rect = game.uiRect;
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            if (mod.upgradeOpen == true)
            {
                CreateUpgradeMenu(rect, tower);
            }
            towerModel.range *= Range;
            if (towerModel.HasBehavior<TowerCreateTowerModel>())
            {
                foreach (var towercreate in towerModel.GetBehaviors<TowerCreateTowerModel>().ToArray())
                {
                    if (towercreate.towerModel.HasBehavior<AirUnitModel>())
                    {
                        foreach (var attackModel in towercreate.towerModel.GetBehavior<AirUnitModel>().GetBehaviors<AttackAirUnitModel>().ToArray())
                        {
                            if (towerModel.GetBehavior<TowerCreateTowerModel>().towerModel.HasBehavior<AirUnitModel>())
                            {
                                if (attackModel.weapons[0].projectile.HasBehavior<DamageModel>())
                                {
                                    attackModel.weapons[0].rate *= AtkSpd;
                                    attackModel.weapons[0].projectile.pierce += Pierce;
                                    attackModel.weapons[0].projectile.GetDamageModel().damage += Dmg;
                                }
                                if (attackModel.weapons[0].projectile.HasBehavior<TravelStraitModel>())
                                {
                                    attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan += Range / 90;
                                }
                                if (attackModel.weapons[0].projectile.HasBehavior<CashModel>())
                                {
                                    attackModel.weapons[0].projectile.GetBehavior<CashModel>().minimum *= Money;
                                    attackModel.weapons[0].projectile.GetBehavior<CashModel>().maximum *= Money;
                                }

                            }
                          
                        }
                        foreach (var attackModel in towercreate.towerModel.GetBehavior<AirUnitModel>().GetBehaviors<AttackModel>().ToArray())
                        {
                            if (attackModel.weapons[0].projectile.HasBehavior<DamageModel>())
                            {
                                attackModel.weapons[0].rate *= AtkSpd;
                                attackModel.weapons[0].projectile.pierce += Pierce;
                                attackModel.weapons[0].projectile.GetDamageModel().damage += Dmg;
                            }
                            if (attackModel.weapons[0].projectile.HasBehavior<TravelStraitModel>())
                            {
                                attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan += Range / 90;
                            }
                            if (attackModel.weapons[0].projectile.HasBehavior<CashModel>())
                            {
                                attackModel.weapons[0].projectile.GetBehavior<CashModel>().minimum *= Money;
                                attackModel.weapons[0].projectile.GetBehavior<CashModel>().maximum *= Money;
                            }
                        }
                    }
                }
            }
            foreach (var attackModel in towerModel.GetDescendants<AttackModel>().ToArray())
            {
                if (!attackModel.weapons[0].projectile.HasBehavior<SlowModel>())
                {
                    attackModel.weapons[0].rate *= AtkSpd;
                    if (attackModel.weapons[0].projectile.HasBehavior<DamageModel>())
                    {
                        attackModel.weapons[0].projectile.pierce += Pierce;
                        attackModel.weapons[0].projectile.GetDamageModel().damage += Dmg;
                    }
                    if (attackModel.weapons[0].projectile.HasBehavior<TravelStraitModel>())
                    {
                        attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan += Range / 60;
                    }
                    if (attackModel.weapons[0].projectile.HasBehavior<CashModel>())
                    {
                        attackModel.weapons[0].projectile.GetBehavior<CashModel>().minimum *= Money;
                        attackModel.weapons[0].projectile.GetBehavior<CashModel>().maximum *= Money;
                    }
                    if (attackModel.weapons[0].projectile.HasBehavior<CreateProjectileOnContactModel>())
                    {
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += Pierce;
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage += Dmg;

                    }
                    if (attackModel.weapons[0].projectile.HasBehavior<CreateProjectileOnExhaustFractionModel>())
                    {
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.pierce += Pierce;
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().damage += Dmg;
                    }
                } 
            }
            foreach (var attackModel in towerModel.GetDescendants<AttackModel>().ToArray())
            {
                attackModel.range *= Range;
            }
            tower.UpdateRootModel(towerModel);
           

        }
        public void NewAbility(Tower tower)
        {
            InGame game = InGame.instance;
            if(SandboxMode)
            {
                RectTransform rect = game.uiRect;
                MenuUi.NewAbilityPanel(rect, tower);
                MenuUi.instance.CloseMenu();
                return;
            }
            if (game.GetCash() >= mod.newAbilityCost)
            {
                game.AddCash(-mod.newAbilityCost);
                RectTransform rect = game.uiRect;
                mod.newAbilityCost += mod.baseNewAbilityCost;
                tower.worth += mod.newAbilityCost - mod.baseNewAbilityCost;
                MenuUi.NewAbilityPanel(rect, tower);
                MenuUi.instance.CloseMenu();
            }
        }
        public static ModHelperPanel CreateAbility(AbilityTemplate ability, Tower tower)
        {

            var sprite = VanillaSprites.GreyInsertPanel;
            var panel = ModHelperPanel.Create(new Info("WeaponContent" + ability.AbilityName, 0, 0, 2250, 150), sprite);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText abilityName = panel.AddText(new Info("abilityName", -600, 0, 1000, 150), ability.AbilityName, 80, TextAlignmentOptions.MidlineLeft);
            ModHelperImage image = panel.AddImage(new Info("image", -100, 0, 140, 140), ability.Icon);
            ModHelperButton selectWpnBtn = panel.AddButton(new Info("selectWpnBtn", 900, 0, 400, 120), VanillaSprites.GreenBtnLong, new System.Action(() => { upgradeUi.AbilitySelected(ability.AbilityName, tower, "none"); }));
            ModHelperText selectWpn = selectWpnBtn.AddText(new Info("selectWpn", 0, 0, 700, 160), "Select", 60);
            return panel;
        }
        public static void SandBoxAbilityPanel(RectTransform rect, Tower tower)
        {
          
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, 2500, 1850, new UnityEngine.Vector2()), VanillaSprites.MainBGPanelBlue);
            panel.transform.DestroyAllChildren();

            ModHelperScrollPanel scrollPanel = panel.AddScrollPanel(new Info("scrollPanel", 0, 0, 2500, 1850), RectTransform.Axis.Vertical, VanillaSprites.MainBGPanelBlue, 15, 50);
            ModHelperButton exit = panel.AddButton(new Info("exit", 1200, 900, 135, 135), VanillaSprites.RedBtn, new System.Action(() => {
                tower.SetSelectionBlocked(false); panel.DeleteObject(); 
            }));
            ModHelperText x = exit.AddText(new Info("x", 0, 0, 700, 160), "X", 80);
            foreach (var ability in ModContent.GetContent<AbilityTemplate>())
            { 
                scrollPanel.AddScrollContent(CreateAbility(ability, tower));
            }
        }
        public static void NewAbilityPanel(RectTransform rect, Tower tower)
        {
            mod.panelOpen = true;
            if (SandboxMode)
            {
                SandBoxAbilityPanel(rect, tower);
                return;
            }

            float abilityPanelWidth = 833.33f;
            float abilityPanelX = 412.5f;
            float abilityPanelY = 900;
            float abilityContentX = 25 + (mod.abilitySlot - 1) * -425;
            float panelWidth = mod.abilitySlot * abilityPanelWidth;
            var sprite = VanillaSprites.BrownInsertPanel;
            if (mod.level == 1)
            {
                sprite = VanillaSprites.BlueInsertPanel;
            }
            if (mod.level == 2)
            {
                sprite = VanillaSprites.MainBgPanelParagon;
            }
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, panelWidth, 1850, new UnityEngine.Vector2()), sprite);
            ModHelperText selectAb = panel.AddText(new Info("selectAb", 0, 800, 2500, 180), "Select New Ability", 100);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            Il2CppSystem.Random rnd = new Il2CppSystem.Random();
            for (int i = 0; i < mod.abilitySlot; i++)
            {
                var num = rnd.Next(0, AbilityClass.AbilityName.Count);
                var abSelected = AbilityClass.AbilityName[num];
                var imgSelected = AbilityClass.AbilityImg[num];
                ModHelperPanel abilityPanel = panel.AddPanel(new Info("abilityPanel", abilityPanelX, abilityPanelY, 650, 1450, new UnityEngine.Vector2()), VanillaSprites.GreyInsertPanel);
                ModHelperText abilityText = panel.AddText(new Info("abilityText", abilityContentX, 600, 800, 180), "Ability", 100);
                ModHelperText abilityText2 = panel.AddText(new Info("abilityText2", abilityContentX, 500, 800, 180), abSelected, 75);
                ModHelperButton selectAbilityBtn = panel.AddButton(new Info("selectAbilityBtn", abilityContentX, -600, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.AbilitySelected(abSelected, tower, "Common")));
                ModHelperText selectAbility1 = selectAbilityBtn.AddText(new Info("selectAbility1", 0, 0, 700, 160), "Select", 70);
                ModHelperImage image = panel.AddImage(new Info("image", abilityContentX, 0, 400, 400), imgSelected);
                abilityPanelX += abilityPanelWidth;
                abilityContentX += abilityPanelWidth;
            }
        
        }
        public void AbilitySelected(string Ability, Tower tower, string rarity)
        {
            mod.panelOpen = false;
            InGame game = InGame.instance;
            if(!SandboxMode)
            {
                Destroy(gameObject);
            }
            RectTransform rect = game.uiRect;
            if (mod.upgradeOpen == true && !SandboxMode)
            {
                CreateUpgradeMenu(rect, tower);
            }
            
            if (Ability == "MIB")
            {
                mod.mib = true;
                var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
                towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
                foreach (var weaponModel in towerModel.GetDescendants<WeaponModel>().ToArray())
                {
                    if (weaponModel.projectile.HasBehavior<DamageModel>())
                    {
                        weaponModel.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                    if (weaponModel.projectile.HasBehavior<CreateProjectileOnContactModel>())
                    {
                        weaponModel.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                    if (weaponModel.projectile.HasBehavior<CreateProjectileOnExhaustFractionModel>())
                    {
                        weaponModel.projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                }
                tower.UpdateRootModel(towerModel);
            }
            foreach (var ability in ModContent.GetContent<AbilityTemplate>().OrderByDescending(c => c.mod == mod))
            {
                if (ability.AbilityName == Ability)
                {
                    ability.EditTower(tower);
                }
            }
          
        }
        public void Upgrade1Panel(Tower tower)
        {
            MenuUi.instance.CloseMenu();
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, 2500, 1850, new UnityEngine.Vector2()), VanillaSprites.BrownInsertPanel);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText upgradeText = panel.AddText(new Info("upgradeText", 0, 800, 2500, 180), "Upgrade To Advanced Ancient Monkey", 100);
            ModHelperText text1 = panel.AddText(new Info("text1", 0, 500, 2500, 180), "-Unlock a new rarity", 75);
            ModHelperText text2 = panel.AddText(new Info("text2", 0, 400, 2500, 180), "-Increase New Weapon Slot by 1", 75);
            ModHelperText text3 = panel.AddText(new Info("text3", 0, 300, 2500, 180), "-Increase Stronger Weapon Slot by 1", 75);
            ModHelperText text4 = panel.AddText(new Info("text4", 0, 200, 2500, 180), "-Increase New Ability Slot by 1", 75);
            ModHelperText text5 = panel.AddText(new Info("text5", 0, 100, 2500, 180), "-Greatly Increased Luck", 75);
            ModHelperText text6 = panel.AddText(new Info("text6", 0, 0, 2500, 180), "-Removed Common Rarity", 75);
            ModHelperText text7 = panel.AddText(new Info("text7", 0, -100, 2500, 180), "-Stronger Weapon and New Weapon Cost Increased", 75);
            ModHelperText text8 = panel.AddText(new Info("text8", 0, -200, 2500, 180), "-New Ability Cost Decreased", 75);
            ModHelperText text9 = panel.AddText(new Info("text9", 0, -300, 2500, 180), "-Keep Everything", 75);
            ModHelperButton upgrade1 = panel.AddButton(new Info("upgrade1", 350, -800, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.Upgrade1(tower)));
            ModHelperText upgrade1Buy = upgrade1.AddText(new Info("upgrade1Buy", 0, 0, 700, 160), "Upgrade ($" + mod.UpgradeCost / 1000 + "K)", 70);
            ModHelperButton cancel = panel.AddButton(new Info("cancel", -350, -800, 500, 160), VanillaSprites.RedBtnLong, new System.Action(() => upgradeUi.Cancel(tower)));
            ModHelperText cancelText = cancel.AddText(new Info("cancelText", 0, 0, 700, 160), "Cancel", 70);
        }
        public void Upgrade2Panel(Tower tower)
        {
            MenuUi.instance.CloseMenu();
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, 2500, 1850, new UnityEngine.Vector2()), VanillaSprites.BrownInsertPanel);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText upgradeText = panel.AddText(new Info("upgradeText", 0, 800, 2500, 180), "Upgrade To Super Ancient Monkey", 100);
            ModHelperText text1 = panel.AddText(new Info("text1", 0, 500, 2500, 180), "-Unlock a new rarity", 75);
            ModHelperText text2 = panel.AddText(new Info("text2", 0, 400, 2500, 180), "-Greatly Increased Luck", 75);
            ModHelperText text3 = panel.AddText(new Info("text3", 0, 300, 2500, 180), "-Removed Rare Rarity", 75);
            ModHelperText text4 = panel.AddText(new Info("text4", 0, 200, 2500, 180), "-Stronger Weapon and New Weapon Cost Increased", 75);
            ModHelperText text5 = panel.AddText(new Info("text5", 0, 100, 2500, 180), "-New Ability Cost Decreased", 75);
            ModHelperText text6 = panel.AddText(new Info("text5", 0, 0, 2500, 180), "-New XP System", 75);
            ModHelperText text7 = panel.AddText(new Info("text7", 0, -100, 2500, 180), "-Keep Everything", 75);
            ModHelperButton upgrade1 = panel.AddButton(new Info("upgrade1", 350, -800, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.Upgrade2(tower)));
            ModHelperText upgrade1Buy = upgrade1.AddText(new Info("upgrade1Buy", 0, 0, 700, 160), "Upgrade ($" + mod.Upgrade2Cost / 1000000 + "M)", 70);
            ModHelperButton cancel = panel.AddButton(new Info("cancel", -350, -800, 500, 160), VanillaSprites.RedBtnLong, new System.Action(() => upgradeUi.Cancel(tower)));
            ModHelperText cancelText = cancel.AddText(new Info("cancelText", 0, 0, 700, 160), "Cancel", 70);
        }
        public string[] Monkeys = {
        "DartMonkey",
        "BoomerangMonkey",
        "BombShooter",
        "TackShooter",
        "IceMonkey",
        "GlueGunner",
        "SniperMonkey",
        "MonkeySub",
        "MonkeyBuccaneer",
        "MonkeyAce",
        "HeliPilot",
        "MortarMonkey",
        "DartlingGunner",
        "WizardMonkey",
        "SuperMonkey",
        "NinjaMonkey",
        "Alchemist",
        "Druid",
        "BananaFarm",
        "MonkeyVillage",
        "SpikeFactory",
        "EngineerMonkey"
        };
        public string Monkey = "DartMonkey";
        public int upgradeTier = 0;
        public int upgradePath = 0;
        public void ChangeSkin(Tower tower)
        {
            Il2CppNinjaKiwi.Common.Random rnd = new Il2CppNinjaKiwi.Common.Random();
            var monkey = Monkeys[rnd.Next(1, Monkeys.Length)];
            var upgradeTier = rnd.Next(1, 6);
            var upgradePath = rnd.Next(1, 4);
            instance.Monkey = monkey;
            instance.upgradePath = upgradePath;
            instance.upgradeTier = upgradeTier;

            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            if (upgradePath == 1)
            {
                towerModel.display = Game.instance.model.GetTowerFromId(monkey + "-" + upgradeTier + "00").display;
            }
            if (upgradePath == 2)
            {
                towerModel.display = Game.instance.model.GetTowerFromId(monkey + "-0" + upgradeTier + "0").display;
            }
            if (upgradePath == 3)
            {
                towerModel.display = Game.instance.model.GetTowerFromId(monkey + "-00" + upgradeTier).display;
            }
            tower.UpdateRootModel(towerModel);
        }
        public void ExtraPanel(Tower tower)
        {
            mod.panelOpen = true;
          
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            var sprite = VanillaSprites.BrownInsertPanel;
            if (mod.level == 1)
            {
                sprite = VanillaSprites.BlueInsertPanel;
            }
            if (mod.level == 2)
            {
                sprite = VanillaSprites.MainBgPanelParagon;
            }
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 1500, 2500, 1850, new UnityEngine.Vector2()), sprite);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            ModHelperText upgradeText = panel.AddText(new Info("upgradeText", 0, 800, 2500, 180), "Extra Upgrades Panel", 100);

            var pipY = 400;
            var pipX = -650;
            for (int i = 0; i < mod.extraWeaponSlotLevel; i++)
            {
                ModHelperImage pipUpgraded = panel.AddImage(new Info("pipUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOnGreen);
                pipX += 87;
            }
            for (int i = 0; i < mod.extraWeaponSlotLevelMax - mod.extraWeaponSlotLevel; i++)
            {
                ModHelperImage pipNotUpgraded = panel.AddImage(new Info("pipNotUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOffRed);
                pipX += 87;
            }
            if(mod.extraWeaponSlotLevel == mod.extraWeaponSlotLevelMax)
            {
                ModHelperButton upgradeExtraSlotLevel = panel.AddButton(new Info("upgradeExtraSlotLevel", -980, 400, 525, 145), VanillaSprites.GreenBtnLong, null);
                ModHelperText upgradeExtraSlotLevelText = upgradeExtraSlotLevel.AddText(new Info("upgradeExtraSlotLevelText", 0, 0, 550, 150), "Max", 50);
            } 
            else
            {
                ModHelperButton upgradeExtraSlotLevel = panel.AddButton(new Info("upgradeExtraSlotLevel", -980, 400, 525, 145), VanillaSprites.GreenBtnLong, new System.Action(() => { if (game.GetCash() >= mod.extraWeaponSlotCost) { game.AddCash(mod.extraWeaponSlotCost *= -1); mod.extraWeaponSlotLevel += 1; panel.DeleteObject(); ExtraPanel(tower); mod.newWeaponSlot++; } }));
                ModHelperText upgradeExtraSlotLevelText = upgradeExtraSlotLevel.AddText(new Info("upgradeExtraSlotLevelText", 0, 0, 550, 150), "Extra New Weapon Slot : $" + mod.extraWeaponSlotCost, 45);
            }

            pipY = 250;
            pipX = -650;
            for (int i = 0; i < mod.strongExtraWeaponSlotLevel; i++)
            {
                ModHelperImage pipUpgraded = panel.AddImage(new Info("pipUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOnGreen);
                pipX += 87;
            }
            for (int i = 0; i < mod.extraWeaponSlotLevelMax - mod.strongExtraWeaponSlotLevel; i++)
            {
                ModHelperImage pipNotUpgraded = panel.AddImage(new Info("pipNotUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOffRed);
                pipX += 87;
            }
            if (mod.strongExtraWeaponSlotLevel == mod.extraWeaponSlotLevelMax)
            {
                ModHelperButton upgradeExtraSlotLevel = panel.AddButton(new Info("upgradeExtraSlotLevel", -980, 250, 525, 145), VanillaSprites.GreenBtnLong, null);
                ModHelperText upgradeExtraSlotLevelText = upgradeExtraSlotLevel.AddText(new Info("upgradeExtraSlotLevelText", 0, 0, 550, 150), "Max", 60);
            }
            else
            {
                ModHelperButton upgradeExtraSlotLevel = panel.AddButton(new Info("upgradeExtraSlotLevel", -980, 250, 525, 145), VanillaSprites.GreenBtnLong, new System.Action(() => { if (game.GetCash() >= mod.strongExtraWeaponSlotCost) { game.AddCash(mod.strongExtraWeaponSlotCost *= -1); mod.strongExtraWeaponSlotLevel += 1; panel.DeleteObject(); ExtraPanel(tower); mod.strongWeaponSlot++; } }));
                ModHelperText upgradeExtraSlotLevelText = upgradeExtraSlotLevel.AddText(new Info("upgradeExtraSlotLevelText", 0, 0, 550, 150), "Extra Stronger Weapon Slot : $" + mod.strongExtraWeaponSlotCost, 45);
            }

            pipY = 100;
            pipX = -650;
            for (int i = 0; i < mod.ExtraAbilitySlotLevel; i++)
            {
                ModHelperImage pipUpgraded = panel.AddImage(new Info("pipUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOnGreen);
                pipX += 87;
            }
            for (int i = 0; i < mod.ExtraAbilitySlotLevelMax - mod.ExtraAbilitySlotLevel; i++)
            {
                ModHelperImage pipNotUpgraded = panel.AddImage(new Info("pipNotUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOffRed);
                pipX += 87;
            }
            if (mod.ExtraAbilitySlotLevel == mod.ExtraAbilitySlotLevelMax)
            {
                ModHelperButton upgradeExtraSlotLevel = panel.AddButton(new Info("upgradeExtraSlotLevel", -980, 100, 525, 145), VanillaSprites.GreenBtnLong, null);
                ModHelperText upgradeExtraSlotLevelText = upgradeExtraSlotLevel.AddText(new Info("upgradeExtraSlotLevelText", 0, 0, 550, 150), "Max", 60);
            }
            else
            {
                ModHelperButton upgradeExtraSlotLevel = panel.AddButton(new Info("upgradeExtraSlotLevel", -980, 100, 525, 145), VanillaSprites.GreenBtnLong, new System.Action(() => { if (game.GetCash() >= mod.ExtraAbilitySlotCost) { game.AddCash(mod.ExtraAbilitySlotCost *= -1); mod.ExtraAbilitySlotLevel += 1; panel.DeleteObject(); ExtraPanel(tower); mod.abilitySlot++; } }));
                ModHelperText upgradeExtraSlotLevelText = upgradeExtraSlotLevel.AddText(new Info("upgradeExtraSlotLevelText", 0, 0, 550, 150), "Extra New Ability Slot : $" + mod.ExtraAbilitySlotCost, 50);
            }

            pipY = -50;
            pipX = -650;
            for (int i = 0; i < mod.ExtraLuckLevel; i++)
            {
                ModHelperImage pipUpgraded = panel.AddImage(new Info("pipUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOnGreen);
                pipX += 87;
            }
            for (int i = 0; i < mod.ExtraLuckMax - mod.ExtraLuckLevel; i++)
            {
                ModHelperImage pipNotUpgraded = panel.AddImage(new Info("pipNotUpgraded", pipX, pipY, 80, 160), VanillaSprites.MkOffRed);
                pipX += 87;
            }
            if (mod.ExtraLuckLevel == mod.ExtraLuckMax)
            {
                ModHelperButton upgradeExtraLevel = panel.AddButton(new Info("upgradeExtraLevel", -980, -50, 525, 145), VanillaSprites.GreenBtnLong, null);
                ModHelperText upgradeExtraLevelText = upgradeExtraLevel.AddText(new Info("upgradeExtraLevelText", 0, 0, 550, 150), "Max", 60);
            }
            else
            {
                ModHelperButton upgradeExtraLevel = panel.AddButton(new Info("upgradeExtraLevel", -980, -50, 525, 145), VanillaSprites.GreenBtnLong, new System.Action(() => { if (game.GetCash() >= mod.ExtraLuckCost) { game.AddCash(mod.ExtraLuckCost * -1); mod.ExtraLuckLevel += 1; mod.ExtraLuckCost *= 1.35f; ; panel.DeleteObject(); ExtraPanel(tower); } }));
                ModHelperText upgradeExtraLevelText = upgradeExtraLevel.AddText(new Info("upgradeExtraLevelText", 0, 0, 550, 150), "Extra Luck : $" + Mathf.Round(mod.ExtraLuckCost), 50);
            }

         
            ModHelperButton monkeySkinChange = panel.AddButton(new Info("monkeySkinChange", -980, -200, 525, 145), VanillaSprites.GreenBtnLong, new System.Action(() => { ChangeSkin(tower); panel.DeleteObject(); ExtraPanel(tower); } ));
            ModHelperText monkeySkinChangeText = monkeySkinChange.AddText(new Info("monkeySkinChangeText", 0, 0, 550, 150), "Change Skin", 60);
            if (upgradePath == 1)
            {
                ModHelperText monkeySkinChangeMonkeyText = panel.AddText(new Info("monkeySkinChangeMonkeyText", 50, -200, 1000, 150), Monkey + " " + upgradeTier + "00", 60);
            }
            if (upgradePath == 2)
            {
                ModHelperText monkeySkinChangeMonkeyText = panel.AddText(new Info("monkeySkinChangeMonkeyText", 50, -200, 1000, 150), Monkey + " 0" + upgradeTier + "0", 60);
            }
            if (upgradePath == 3)
            {
                ModHelperText monkeySkinChangeMonkeyText = panel.AddText(new Info("monkeySkinChangeMonkeyText", 50, -200, 1000, 150), Monkey + " 00" + upgradeTier, 60);
            }
          

            ModHelperButton cancel = panel.AddButton(new Info("cancel", 0, -800, 500, 160), VanillaSprites.RedBtnLong, new System.Action(() => upgradeUi.Cancel(tower)));
            ModHelperText cancelText = cancel.AddText(new Info("cancelText", 0, 0, 700, 160), "Cancel", 70);
        }
        public void Upgrade1(Tower tower)
        {
            InGame game = InGame.instance;
            if (game.GetCash() >= mod.UpgradeCost)
            {
                game.AddCash(-mod.UpgradeCost);
                
                RectTransform rect = game.uiRect;
            
                mod.newWeaponCost = 450 * mod.selectedChallenge.NewWeaponCostMult;
                mod.baseNewWeaponCost = 350 * mod.selectedChallenge.NewWeaponCostMult;
                mod.legendaryChance = 98;
                mod.exoticChance = 100;
                mod.legendaryStrongChance = 95;
                mod.strongerWeaponCost = 950 * mod.selectedChallenge.StrongerWeaponCostMult;
                mod.baseStrongerWeaponCost = 750 * mod.selectedChallenge.StrongerWeaponCostMult;
                mod.newAbilityCost = 6000 * mod.selectedChallenge.AbilityWeaponCostMult;

                mod.baseNewAbilityCost = 750 * mod.selectedChallenge.AbilityWeaponCostMult;
                mod.level += 1;
                mod.newWeaponSlot += 1;
                mod.strongWeaponSlot += 1;
                mod.abilitySlot += 1;
                mod.minNewWeaponRarity = mod.selectedChallenge.MinURarity;
                mod.maxNewWeaponRarity = mod.selectedChallenge.MaxURarity;
                mod.minStrongWeaponRarity = mod.selectedChallenge.MinURarity;
                mod.maxStrongWeaponRarity = mod.selectedChallenge.MaxURarity;
                mod.panelOpen = false;
                if (mod.upgradeOpen == true)
                {
                    CreateUpgradeMenu(rect, tower);
                }
                  
                Destroy(gameObject);
            }
        }
        public void Upgrade2(Tower tower)
        {
            InGame game = InGame.instance;
            if (game.GetCash() >= mod.Upgrade2Cost)
            {
                game.AddCash(-mod.Upgrade2Cost);

                RectTransform rect = game.uiRect;

                mod.newWeaponCost = 2350 * mod.selectedChallenge.NewWeaponCostMult;
                mod.baseNewWeaponCost = 1750 * mod.selectedChallenge.NewWeaponCostMult;
                mod.legendaryChance = 80;
                mod.exoticChance = 99;
                mod.legendaryStrongChance = 75;
                mod.strongerWeaponCost = 3500 * mod.selectedChallenge.StrongerWeaponCostMult;
                mod.baseStrongerWeaponCost = 2250 * mod.selectedChallenge.StrongerWeaponCostMult;
                mod.newAbilityCost = 5500 * mod.selectedChallenge.AbilityWeaponCostMult;

                mod.baseNewAbilityCost = 500 * mod.selectedChallenge.AbilityWeaponCostMult;
                mod.level += 1;
                mod.minNewWeaponRarity = mod.selectedChallenge.MinUURarity;
                mod.maxNewWeaponRarity = mod.selectedChallenge.MaxUURarity;
                mod.minStrongWeaponRarity = mod.selectedChallenge.MinUURarity;
                mod.maxStrongWeaponRarity = mod.selectedChallenge.MaxUURarity;
                mod.XP = 0;
                mod.XPMax = 2;

                mod.panelOpen = false;
                if (mod.upgradeOpen == true)
                {
                    CreateUpgradeMenu(rect, tower);
                }

                Destroy(gameObject);
            }
        }
        public void Cancel(Tower tower)
        {
            InGame game = InGame.instance;
            RectTransform rect = game.uiRect;
            Destroy(gameObject);
            mod.panelOpen = false;
            if (mod.upgradeOpen == true)
            {
                CreateUpgradeMenu(rect, tower);
            }
        }
        public static void CreateUpgradeMenu(RectTransform rect, Tower tower)
        {
            if(mod.panelOpen == true)
            {
                return;
            }
            var sprite = VanillaSprites.BrownInsertPanel;
            if (mod.level == 1)
            {
                sprite = VanillaSprites.BlueInsertPanel;
            }
            if (mod.level == 2)
            {
                sprite = VanillaSprites.MainBgPanelParagon;
            }
            ModHelperPanel panel = rect.gameObject.AddModHelperPanel(new Info("Panel_", 2200, 200, 3333, 450, new UnityEngine.Vector2()), sprite);
            MenuUi upgradeUi = panel.AddComponent<MenuUi>();
            instance = upgradeUi;
   
            if(SandboxMode)
            {
                ModHelperText newWpnCost = panel.AddText(new Info("newWpnCost", -416, 140, 1000, 180), "New Weapon: Free", 70);
            }
            else
            {
                ModHelperText newWpnCost = panel.AddText(new Info("newWpnCost", -416, 140, 1000, 180), "New Weapon: $" + Mathf.Round(mod.newWeaponCost), 70);
            }
            if (mod.selectingWeaponOpen == false)
            {
                ModHelperButton newWpnBtn = panel.AddButton(new Info("newWpnBtn", -416, 20, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.NewWeapon(tower)));
                ModHelperText newWpnBuy = newWpnBtn.AddText(new Info("newWpnBuy", 0, 0, 700, 160), "Buy", 70);
            }
            ModHelperText newWpnDesc = panel.AddText(new Info("newWpnDesc", -416, -100, 860, 180), "Give an extra weapon", 42);

            if (SandboxMode)
            {
                ModHelperText strongWpnCost = panel.AddText(new Info("strongWpnCost", -1216, 140, 1000, 180), "Stronger Weapon: Free", 70);
            }
            else
            {
                ModHelperText strongWpnCost = panel.AddText(new Info("strongWpnCost", -1216, 140, 1000, 180), "Stronger Weapon: $" + Mathf.Round(mod.strongerWeaponCost), 70);
            }
       
            if (mod.selectingWeaponOpen == false)
            {
                ModHelperButton strongWpnBtn = panel.AddButton(new Info("strongWpnBtn", -1216, 20, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.StrongWeapon(tower)));
                ModHelperText strongWpnBuy = strongWpnBtn.AddText(new Info("strongWpnBuy", 0, 0, 700, 160), "Buy", 70);
            }

            ModHelperText strongWpnDesc = panel.AddText(new Info("strongWpnDesc", -1216, -100, 860, 180), "Make your current weapon stronger", 42);
            if (SandboxMode)
            {
                ModHelperText abilityCost = panel.AddText(new Info("abilityCost", 384, 140, 1000, 180), "New Ability: Free", 70);
            } else
            {
                ModHelperText abilityCost = panel.AddText(new Info("abilityCost", 384, 140, 1000, 180), "New Ability: $" + Mathf.Round(mod.newAbilityCost), 70);
            }

            ModHelperText extraText = panel.AddText(new Info("extraText", 1217, 140, 1000, 180), "Extra Panel", 70);
            ModHelperButton extraBtn = panel.AddButton(new Info("extraBtn", 1217, 20, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => { upgradeUi.ExtraPanel(tower); MenuUi.instance.CloseMenu(); }));
            ModHelperText extraOpen = extraBtn.AddText(new Info("extraOpen", 0, 0, 700, 160), "Open", 70);

            if (mod.selectingWeaponOpen == false)
            {
                ModHelperButton abilityBtn = panel.AddButton(new Info("abilityBtn", 384, 20, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.NewAbility(tower)));
                ModHelperText abilityBuy = abilityBtn.AddText(new Info("abilityBuy", 0, 0, 700, 160), "Buy", 70);
            }
            ModHelperText abilityDesc = panel.AddText(new Info("abilityDesc", 384, -100, 860, 180), "Give an ability", 42);

            if(mod.level == 0)
            {
                ModHelperButton upgrade1 = panel.AddButton(new Info("upgrade1", 0, 300, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.Upgrade1Panel(tower)));
                ModHelperText upgrade1Buy = upgrade1.AddText(new Info("upgrade1Buy", 0, 0, 700, 160), "Upgrade", 70);
            }
            if (mod.level == 1)
            {
                ModHelperButton upgrade1 = panel.AddButton(new Info("upgrade2", 0, 300, 500, 160), VanillaSprites.GreenBtnLong, new System.Action(() => upgradeUi.Upgrade2Panel(tower)));
                ModHelperText upgrade1Buy = upgrade1.AddText(new Info("upgrade2Buy", 0, 0, 700, 160), "Upgrade", 70);
            }
            if (mod.level >= 2)
            {
                var percent = mod.XP * 100 / mod.XPMax;
                var size = 2970 * percent / 100;
                ModHelperPanel xppanel = panel.AddPanel(new Info("Panel", 0, 325, 3000, 180), VanillaSprites.BrownInsertPanel);
                ModHelperPanel xpbar = panel.AddPanel(new Info("Panel", (size - size / 2) - 2970 / 2 , 325, size, 150), VanillaSprites.MainBgPanelParagon);
                ModHelperText upgrade1Buy = panel.AddText(new Info("text", 0, 325, 3000, 180), mod.XPMax - mod.XP + " Until Free Weapon", 70);
            }
        }
    }
}