using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField] PlayerUIElements[] uiElements;
    private Dictionary<Player, PlayerUIElements> playerInfos =
        new Dictionary<Player, PlayerUIElements>();

    private static readonly string lapsPrefix = "Кругов пройдено: ";
    private static readonly string bestTimePrefix = "Лучшее время: ";
    private static readonly string previousTimePrefix = "Предыдущее время: ";

    private void Start()
    {
        GameManager.Instance.OnPlayerPassedFinish.AddListener(HandlePlayerPassedFinish);
    }

    public void Initialize()
    {
        playerInfos = new Dictionary<Player, PlayerUIElements>();
        foreach (var e in uiElements)
            playerInfos.Add(GameManager.Instance.players[e.player], e);

        foreach (var uiElements in playerInfos.Values)
        {
            uiElements.laps.text = $"{lapsPrefix}0";
            uiElements.bestTime.text = $"{bestTimePrefix}-";
            uiElements.previousTime.text = $"{previousTimePrefix}-";
        }
    }

    private void HandlePlayerPassedFinish(Player player)
    {
        StartCoroutine(CallUpdateNextFrame(player));
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
        public int player;

        public Text laps;
        public Text bestTime;
        public Text previousTime;
    }
}
