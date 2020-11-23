using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;
    [Space]
    [SerializeField] private Image carImage;
    [SerializeField] private Text playerName;
    [SerializeField] private Text totalTime;

    private void Start()
    {
        restartButton.onClick.AddListener(() => 
            UIManager.Instance.SpawnComingSoon(restartButton.transform.position, transform));
    }

    public void ShowWinnerStats(Player winner)
    {
        carImage.sprite = winner.car.sprite;
        playerName.text = winner.name;
        totalTime.text = GameManager.Instance.lapInfoManager.TotalTime(winner).ToString();
    }
}
