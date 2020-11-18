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
    [Space]
    [SerializeField] private Camera dummyCamera;

    private void Start()
    {
        SwitchToMenuWithButton(mainMenu.playButton, mapSelectMenu);
        SwitchToMenuWithButton(mapSelectMenu.backButton, mainMenu);
        SwitchToMenuWithButton(mapSelectMenu.nextButton, carSelectMenu);
        SwitchToMenuWithButton(carSelectMenu.backButton, mapSelectMenu);

        carSelectMenu.playButton.onClick.AddListener(() =>
        {
            carSelectMenu.gameObject.SetActive(false);
            GameManager.Instance.StartGame();
        });

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
    }

    private void SwitchToMenuWithButton(Button button, MonoBehaviour to)
    {
        button.onClick.AddListener(() =>
        {
            button.gameObject.SetActive(false);
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
}
