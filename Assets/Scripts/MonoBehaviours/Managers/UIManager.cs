using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject globalBackground;
    [Space]
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private CarSelectMenu carSelectMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [Space]
    [SerializeField] private Camera dummyCamera;

    private void Start()
    {
        mainMenu.playButton.onClick.AddListener(() =>
        {
            SwitchMenus(mainMenu, carSelectMenu);
        });

        carSelectMenu.backButton.onClick.AddListener(() =>
        {
            SwitchMenus(carSelectMenu, mainMenu);
        });

        carSelectMenu.playButton.onClick.AddListener(() =>
        {
            SwitchMenus(carSelectMenu, null);
            GameManager.Instance.StartGame();
        });

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
    }

    private void SwitchMenus(MonoBehaviour from, MonoBehaviour to)
    {
        if (from != null)
            from.gameObject.SetActive(false);

        if (to != null)
            to.gameObject.SetActive(true);
    }

    private void HandleGameStateChange(GameManager.GameState previousState, GameManager.GameState currentState)
    {
        if (previousState == GameManager.GameState.PREGAME)
        {
            globalBackground.SetActive(false);
            dummyCamera.gameObject.SetActive(false);
        }

        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }
}
