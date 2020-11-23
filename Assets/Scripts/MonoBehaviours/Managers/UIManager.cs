using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject globalBackground;
    [Space]
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private MapSelectMenu mapSelectMenu;
    [SerializeField] private CarSelectMenu carSelectMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [Space]
    [SerializeField] private Overlay overlay;
    [SerializeField] private VictoryScreen victoryScreen;
    [Space]
    [SerializeField] private Camera dummyCamera;
    [Space]
    [SerializeField] private GameObject comingSoon;

    [HideInInspector] public Events.EventDurabilityChanged OnDurabilityChanged;
    [HideInInspector] public Events.EventFuelChanged OnFuelChanged;

    private void Start()
    {
        SwitchMenusWithButton(mainMenu.playButton, mainMenu, mapSelectMenu);
        SwitchMenusWithButton(mapSelectMenu.backButton, mapSelectMenu, mainMenu);
        SwitchMenusWithButton(mapSelectMenu.nextButton, mapSelectMenu, carSelectMenu);
        SwitchMenusWithButton(carSelectMenu.backButton, carSelectMenu, mapSelectMenu);

        carSelectMenu.playButton.onClick.AddListener(() =>
        {
            carSelectMenu.gameObject.SetActive(false);
            GameManager.Instance.StartGame();
        });

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
        GameManager.Instance.OnPlayerWon.AddListener(HandlePlayerWon);
    }

    private void SwitchMenusWithButton(Button button, MonoBehaviour from, MonoBehaviour to)
    {
        button.onClick.AddListener(() =>
        {
            from.gameObject.SetActive(false);
            to.gameObject.SetActive(true);
        });
    }

    private void HandleGameStateChange(GameManager.GameState previousState, GameManager.GameState currentState)
    {
        if (previousState == GameManager.GameState.PREGAME)
        {
            globalBackground.SetActive(false);
            dummyCamera.gameObject.SetActive(false);
            overlay.gameObject.SetActive(true);
            overlay.Initialize();
        }

        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

    private void HandlePlayerWon(Player player)
    {
        victoryScreen.gameObject.SetActive(true);
        victoryScreen.ShowWinnerStats(player);
    }

    public void SpawnComingSoon(Vector3 position, Transform parent)
    {
        var cs =Instantiate(comingSoon, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-45f, 45f)), parent);
        Destroy(cs, 2f);
    }
}
