using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapSelectMenu : MonoBehaviour
{
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Image mapImage;
    [SerializeField] private Text mapName;
    [SerializeField] private Text mapDescription;
    [Space]
    public Button backButton;
    public Button nextButton;
    [Space]
    [SerializeField] Map_SO[] mapsForChoice;

    private int currentMapNumber;
    private Map_SO CurrentMap => mapsForChoice[currentMapNumber];

    public void Start()
    {
        leftArrow.onClick.AddListener(PreviousMap);
        rightArrow.onClick.AddListener(NextMap);
        nextButton.onClick.AddListener(() => 
            GameManager.Instance.OnMapSelected.Invoke(CurrentMap.sceneName));
    }

    private void OnEnable()
    {
        currentMapNumber = 0;
        ShowCurrentMap();
    }

    private void PreviousMap()
    {
        if (currentMapNumber == 0)
            currentMapNumber = mapsForChoice.Length - 1;
        else
            currentMapNumber--;

        ShowCurrentMap();
    }

    private void NextMap()
    {
        if (currentMapNumber == mapsForChoice.Length - 1)
            currentMapNumber = 0;
        else
            currentMapNumber++;

        ShowCurrentMap();
    }

    public void ShowCurrentMap()
    {
        mapImage.sprite = CurrentMap.background;
        mapName.text = CurrentMap.name;
        mapDescription.text = CurrentMap.description;       
    }
}
