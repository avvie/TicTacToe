using System;
using AI;
using TMPro;
using UnityEngine;

namespace Game.GameHelper
{
    // Quick easy way to pass data, not a fan of statics
    public static class GameConfig
    {
        public static LastWinnerEnum LastWinner { get; private set; }
        
        static GameConfig() => LastWinner = LastWinnerEnum.None;
        
        private static Func<GameObject, TextMeshProUGUI, IAIAgent> GetEasyAI() => (go, label) => SetLabelAndReturn<EasyIAi>(go.AddComponent<EasyIAi>(), label) as IAIAgent;
        private static Func<GameObject, TextMeshProUGUI, IAIAgent> GetNormalAI() => (go, label) => SetLabelAndReturn<NormalAi>(go.AddComponent<NormalAi>(), label) as IAIAgent;
        private static Func<GameObject, TextMeshProUGUI, IAIAgent> GetExpertAI() => (go, label) => SetLabelAndReturn<ExpertAi>(go.AddComponent<ExpertAi>(), label) as IAIAgent;

        private static Component SetLabelAndReturn<T>(IAIAgent component, TextMeshProUGUI label) where T : Agent
        {
            component.SetChatLabel(label);
            return component as T;
        }
        public static GameDifficulty CurrentDifficulty { get; private set; }

        public static void SetLastWinner(PlayerType type)
        {
            switch (type)
            {
                case PlayerType.None:
                    LastWinner = LastWinnerEnum.Draw;
                    break;
                case PlayerType.Human:
                    LastWinner = LastWinnerEnum.Human;
                    break;
                case PlayerType.AI:
                    LastWinner = LastWinnerEnum.AI;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        public static void SetStageDifficulty(GameDifficulty diff) => CurrentDifficulty = diff;

        public static string GetLastWinnerText()
        {
            string message = "";
            switch (LastWinner)
            {
                case LastWinnerEnum.Draw:
                    message = "Game ended in a draw!";
                    break;
                case LastWinnerEnum.Human:
                    message = "Congrats you won!";
                    break;
                case LastWinnerEnum.AI:
                    message = "Oops better luck next time";
                    break;
            }

            return message;
        }
        public static Func<GameObject, TextMeshProUGUI, IAIAgent> GetCurrentDiffAi()
        {
            switch (CurrentDifficulty)
            {
                case GameDifficulty.Easy:
                    return GetEasyAI();
                case GameDifficulty.Normal:
                    return GetNormalAI();
                case GameDifficulty.Hard:
                    return GetExpertAI();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new InvalidOperationException();
        }
        
    }

    public enum LastWinnerEnum
    {
        None = 0,
        Draw = 1,
        Human = 2,
        AI = 3,
    }

    public enum GameDifficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
    }
}