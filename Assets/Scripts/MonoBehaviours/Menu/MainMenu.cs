using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button leaderboardButton;
    public Button quitButton;
    public Button loreButton;

    private void Start()
    {
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
    }
}
