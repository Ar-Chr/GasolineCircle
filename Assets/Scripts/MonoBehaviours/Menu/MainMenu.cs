using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
    }
}
