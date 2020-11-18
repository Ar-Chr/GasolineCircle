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

    private Dictionary<string, string> levelNameConverter =
        new Dictionary<string, string>
        {
            { "Тестовый", "TestLevel" }
        };

    public Events.EventGameState OnGameStateChanged;
    public Events.EventPlayerPassedFinish OnPlayerPassedFinish;
    public Events.EventNextLevelSelected OnMapSelected;

    public GameState CurrentGameState { get; private set; } = GameState.PREGAME;
    private string nextLevelName = string.Empty;
    private string currentLevelName = string.Empty;

    public LapInfoManager lapInfoManager;

    public Player[] players;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        OnMapSelected.AddListener(HandleNextLevelSelected);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.PREGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    private void HandleNextLevelSelected(string nextLevelName)
    {
        this.nextLevelName = levelNameConverter[nextLevelName];
    }
    
    private void HandleCarsSelected(Car player0Car, Car player1Car)
    {

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
        players = FindObjectsOfType<Player>();
        lapInfoManager = new LapInfoManager(players);

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
        LoadLevel(nextLevelName);
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

public class LapInfoManager
{
    Dictionary<Player, PlayerLapInfo> lapInfos;

    public LapInfoManager(params Player[] players)
    {
        lapInfos = new Dictionary<Player, PlayerLapInfo>();
        foreach (Player player in players)
        {
            lapInfos.Add(player, new PlayerLapInfo() { bestTime = float.MaxValue });
        }
        GameManager.Instance.OnPlayerPassedFinish.AddListener(HandlePlayerPassedFinish);
    }

    public void HandlePlayerPassedFinish(Player player)
    {
        PlayerLapInfo oldInfo = lapInfos[player];
        float previousTime = oldInfo.CurrentLapTime;
        lapInfos[player] = new PlayerLapInfo(oldInfo.laps + 1,
                                             Mathf.Min(oldInfo.bestTime, previousTime),
                                             previousTime,
                                             Time.time);
    }

    public int Laps(Player player) => lapInfos[player].laps;
    public float BestTime(Player player) => lapInfos[player].bestTime;
    public float PreviousTime(Player player) => lapInfos[player].previousTime;

    private struct PlayerLapInfo
    {
        public int laps;
        public float bestTime;
        public float previousTime;
        private float lastFinishPassTime;

        public float CurrentLapTime => Time.time - lastFinishPassTime;

        public PlayerLapInfo(int laps, float bestTime, float previousTime, float lastFinishPassTime)
        {
            this.laps = laps;
            this.bestTime = bestTime;
            this.previousTime = previousTime;
            this.lastFinishPassTime = lastFinishPassTime;
        }
    }
}