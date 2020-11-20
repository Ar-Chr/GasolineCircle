using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
        resumeButton.onClick.AddListener(GameManager.Instance.TogglePause);
    }
}
