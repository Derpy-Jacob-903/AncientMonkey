using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Scenarios;
using Il2CppAssets.Scripts.Models.Difficulty;
using Il2CppAssets.Scripts.Models.Gameplay.Mods;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Api.Bloons;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Unity.Scenes;

namespace AncientMonkey
{
    /*public class AncientRoundsMode : ModGameMode
    {
        public override string Name => "Ancient Rounds";
        public override string Difficulty => DifficultyType.Hard;
        public override string BaseGameMode => GameModeType.Clicks;
        public override string DisplayName => "Ancient Rounds";
        public override void ModifyBaseGameModeModel(ModModel gameModeModel)
        {
            gameModeModel.SetStartingCash(325);
            gameModeModel.SetStartingRound(1);
            gameModeModel.SetEndingRound(100);
            gameModeModel.GetMutator<MonkeyMoneyModModel>().multiplier = 2f;

            gameModeModel.SetAllCashMultiplier(0.65f);

            gameModeModel.UseRoundSet<AncientRounds>();
        }
    }
    class AncientRounds : ModRoundSet
    {
        public override string BaseRoundSet => RoundSetType.Empty;

        public override int DefinedRounds => 1000;

        public override string DisplayName => "Cheat Rounds";

        public override void ModifyEasyRoundModels(RoundModel roundModel, int round)
        {
            switch (round)
            {
                case 0:
                    roundModel.AddBloonGroup(BloonType.Red, 35, 0, 1500);
                    roundModel.AddBloonGroup(BloonType.Blue, 10, 350, 1150);
                    break;
                case 1:
                    roundModel.AddBloonGroup(BloonType.Red, 50, 0, 1600);
                    roundModel.AddBloonGroup(BloonType.Blue, 8, 0, 100);
                    roundModel.AddBloonGroup(BloonType.Blue, 8, 500, 600);
                    roundModel.AddBloonGroup(BloonType.Blue, 8, 1000, 1100);
                    roundModel.AddBloonGroup(BloonType.Blue, 8, 1500, 1600);
                    break;
                case 2:
                    roundModel.AddBloonGroup(BloonType.Green, 30, 0, 800);
                    break;
                case 3:
                    roundModel.AddBloonGroup(BloonType.Blue, 25, 0, 720);
                    roundModel.AddBloonGroup(BloonType.Green, 18, 474, 624);
                    roundModel.AddBloonGroup(BloonType.Blue, 10, 870, 1038);
                    break;
                case 4:
                    roundModel.AddBloonGroup(BloonType.Blue, 75, 0, 600);
                    break;
                case 5:
                    roundModel.AddBloonGroup(BloonType.Red, 40, 0, 500);
                    roundModel.AddBloonGroup(BloonType.Blue, 20, 500, 750);
                    roundModel.AddBloonGroup(BloonType.Green, 10, 750, 875);
                    roundModel.AddBloonGroup(BloonType.Yellow, 5, 875, 937.5f);
                    break;
                case 6:
                    roundModel.AddBloonGroup(BloonType.Red, 125, 0, 25);
                    break;
                case 7:
                    roundModel.AddBloonGroup(BloonType.Yellow, 40, 0, 1500);
                    roundModel.AddBloonGroup(BloonType.Green, 65, 0, 1500);
                    break;
                case 8:
                    roundModel.AddBloonGroup(BloonType.Green, 90, 0, 1000);
                    roundModel.AddBloonGroup(BloonType.Pink, 20, 0, 700);
                    roundModel.AddBloonGroup(BloonType.Yellow, 30, 1300, 1699);
                    break;
                case 9:
                    roundModel.AddBloonGroup(BloonType.PinkCamo, 15, 0, 2000);
                    roundModel.AddBloonGroup(BloonType.Red, 175, 0, 2000);
                    roundModel.AddBloonGroup(BloonType.BlueCamo, 50, 1900, 2100);
                    break;
                case 10:
                    roundModel.AddBloonGroup(BloonType.Red, 25, 0, 400);
                    roundModel.AddBloonGroup(BloonType.RedCamo, 25, 10, 410);
                    roundModel.AddBloonGroup(BloonType.RedRegrow, 25, 20, 420);
                    roundModel.AddBloonGroup(BloonType.RedRegrowCamo, 25, 30, 430);
                    roundModel.AddBloonGroup(BloonType.Blue, 25, 440, 850);
                    roundModel.AddBloonGroup(BloonType.BlueCamo, 25, 450, 860);
                    roundModel.AddBloonGroup(BloonType.BlueRegrow, 25, 460, 870);
                    roundModel.AddBloonGroup(BloonType.BlueRegrowCamo, 25, 470, 880);
                    roundModel.AddBloonGroup(BloonType.Green, 25, 890, 1300);
                    roundModel.AddBloonGroup(BloonType.GreenCamo, 25, 900, 1310);
                    roundModel.AddBloonGroup(BloonType.GreenRegrow, 25, 910, 1320);
                    roundModel.AddBloonGroup(BloonType.GreenRegrowCamo, 25, 920, 1330);
                    break;
                default:
                    break;
            }
        }
    } */
}
