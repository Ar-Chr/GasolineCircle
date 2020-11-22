using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField] private PlayerUIElements player0UiElements;
    [SerializeField] private PlayerUIElements player1UiElements;
    private Dictionary<Player, PlayerUIElements> playerInfos =
        new Dictionary<Player, PlayerUIElements>();

    private static readonly string lapsPrefix = "Кругов пройдено: ";
    private static readonly string bestTimePrefix = "Лучшее время: ";
    private static readonly string previousTimePrefix = "Предыдущее время: ";

    private void Start()
    {
        GameManager.Instance.OnPlayerPassedFinish.AddListener(HandlePlayerPassedFinish);
        UIManager.Instance.OnDurabilityChanged.AddListener(HandleDurabilityChanged);
        UIManager.Instance.OnFuelChanged.AddListener(HandleFuelChanged);
    }

    public void Initialize()
    {
        playerInfos = new Dictionary<Player, PlayerUIElements>();
        playerInfos.Add(GameManager.Instance.players[0], player0UiElements);
        playerInfos.Add(GameManager.Instance.players[1], player1UiElements);

        player0UiElements.playerName.text = GameManager.Instance.players[0].name;
        player1UiElements.playerName.text = GameManager.Instance.players[1].name;

        foreach (var uiElements in playerInfos.Values)
        {
            uiElements.laps.text = $"{lapsPrefix}0";
            uiElements.bestTime.text = $"{bestTimePrefix}-";
            uiElements.previousTime.text = $"{previousTimePrefix}-";
            uiElements.durabilityBar.value = 1;
            uiElements.fuelBar.value = 1;
        }
    }

    private void HandlePlayerPassedFinish(Player player)
    {
        StartCoroutine(CallUpdateNextFrame(player));
    }

    public void HandleDurabilityChanged(Player player, float newRatio)
    {
        playerInfos[player].durabilityBar.value = newRatio;
    }

    public void HandleFuelChanged(Player player, float newRatio)
    {
        playerInfos[player].fuelBar.value = newRatio;
    }

    private IEnumerator CallUpdateNextFrame(Player player)
    {
        yield return new WaitForSeconds(0);
        UpdateLapInfo(player);
    }

    private void UpdateLapInfo(Player player)
    {
        var uiElements = playerInfos[player];
        var lapInfo = GameManager.Instance.lapInfoManager;

        uiElements.laps.text = $"{lapsPrefix}{lapInfo.Laps(player)}";
        uiElements.bestTime.text = $"{bestTimePrefix}{lapInfo.BestTime(player): 0.00}";
        uiElements.previousTime.text = $"{previousTimePrefix}{lapInfo.PreviousTime(player): 0.00}";
    }

    [Serializable]
    private class PlayerUIElements
    {
        public Text playerName;

        public Text laps;
        public Text bestTime;
        public Text previousTime;

        public Slider durabilityBar;
        public Slider fuelBar;
    }
}
