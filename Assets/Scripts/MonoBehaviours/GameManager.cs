using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public Events.EventGameState OnGameStateChanged;

    public GameState CurrentGameState { get; private set; } = GameState.PREGAME;
    private string currentLevelName = string.Empty;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.PREGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    #region Level load

    private void LoadLevel(string levelName)
    {
        AsyncOperation levelLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if(levelLoad == null)
        {
            Debug.LogError("[GameManager] Unable to load level");
            return;
        }

        levelLoad.completed += OnLoadOperationComplete;
        currentLevelName = levelName;
    }

    private void OnLoadOperationComplete(AsyncOperation levelLoad)
    {
        ChangeGameStateTo(GameState.RUNNING);
    }

    #endregion

    #region Level unload
    // !!!!!!!
    #endregion

    private void ChangeGameStateTo(GameState state)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(previousGameState, CurrentGameState);
    }

    public void StartGame()
    {
        LoadLevel("TestLevel");
    }

    public void TogglePause()
    {
        ChangeGameStateTo(CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
}
