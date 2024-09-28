﻿using System;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api;
using UnityEngine;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using TaskScheduler = BTD_Mod_Helper.Api.TaskScheduler;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.UI_New.Main.DifficultySelect;
using Il2CppAssets.Scripts.Unity.UI_New.Main.MapSelect;
using HarmonyLib;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

namespace AncientMonkey.Challenge
{
    public static class AncientPanelButton
    {
        private static ModHelperPanel panel;
        private static ModHelperButton image;

        private static void OpenEditorPanel()
        {
            MenuManager.instance.buttonClickSound.Play("ClickSounds");
            ModGameMenu.Open<AncientPanel>();
        }
        public static void CreatePanel(GameObject screen)
        {
            panel = screen.AddModHelperPanel(new Info("AncientPanelButton")
            {
                Anchor = new Vector2(1, 0),
                Pivot = new Vector2(1, 0)
            });
            var animator = panel.AddComponent<Animator>();
            animator.runtimeAnimatorController = Animations.GlobalButtonAnimation;
            animator.speed = .75f;

            image = panel.AddButton(new Info("AncientPanelButton", -4390, 500, 420, 420, new Vector2(1, 0), new Vector2(0.5f, 0)), VanillaSprites.PrimaryMonkeyIcon, new Action(OpenEditorPanel));
            image.AddText(new Info("Text", 0, -125, 425, 200), "Ancient Panel", 70f);
        }
        private static void HideButton()
        {
            panel.GetComponent<Animator>().Play("PopupSlideOut");
            TaskScheduler.ScheduleTask(() => panel.SetActive(false), ScheduleType.WaitForFrames, 13);
        }
        private static void Init()
        {
            var screen = CommonForegroundScreen.instance.transform;
            var ModSavePanel = screen.FindChild("AncientPanelButton");
            if (ModSavePanel == null)
            {
                CreatePanel(screen.gameObject);
            }
        }
        public static void Show()
        {
            Init();
            panel.SetActive(true);
            panel.GetComponent<Animator>().Play("PopupSlideIn");
        }

        public static void Hide()
        {
            var screen = CommonForegroundScreen.instance.transform;
            var ModSavePanel = screen.FindChild("AncientPanelButton");
            if (ModSavePanel != null)
            {
                HideButton();
            }
        }
    }
    [HarmonyPatch(typeof(MenuManager), nameof(MenuManager.OpenMenu))]
    internal static class MenuManager_OpenMenu
    {
        [HarmonyPostfix]
        private static void Postfix(MenuManager __instance, string menuName)
        {
            if (menuName == "MapSelectScreen")
            {
                AncientPanelButton.Show();
            }
        }
    }

    [HarmonyPatch(typeof(DifficultySelectScreen), nameof(DifficultySelectScreen.Open))]
    internal static class DifficultySelectScreen_Open
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            AncientPanelButton.Hide();
        }
    }

    [HarmonyPatch(typeof(DifficultySelectScreen), nameof(DifficultySelectScreen.OpenModeSelectUi))]
    internal static class DifficultySelectScreen_OpenModeSelectUi
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            AncientPanelButton.Hide();
        }
    }

    [HarmonyPatch(typeof(ContinueGamePanel), nameof(ContinueGamePanel.ContinueClicked))]
    internal static class ContinueGamePanel_ContinueClicked
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            AncientPanelButton.Hide();
        }
    }

    [HarmonyPatch(typeof(MapSelectScreen), nameof(MapSelectScreen.Open))]
    internal static class MapSelectScreen_Open
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            AncientPanelButton.Show();
        }
    }

    [HarmonyPatch(typeof(MapSelectScreen), nameof(MapSelectScreen.Close))]
    internal static class MapSelectScreen_Close
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            AncientPanelButton.Hide();
        }
    }
}
