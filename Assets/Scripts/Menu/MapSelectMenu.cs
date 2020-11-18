using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapSelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Vector3 firstButtonPosition;
    [SerializeField] private Vector3 buttonOffset;
    [SerializeField] private float maxSumHeight;
    [Space]
    public Button backButton;
    public Button nextButton;
    [Space]
    [SerializeField] private string[] levelNames;

    private Button[] buttons;
    private float buttonHeight;
    private float offsetHeight;

    private void Start()
    {
        buttonHeight = buttonPrefab.GetComponent<RectTransform>().rect.height;
        offsetHeight = Mathf.Abs(buttonOffset.y);

        CreateAllButtons();
        AddOnClickToAllButtons();
    }

    private void CreateAllButtons()
    {
        buttons = new Button[levelNames.Length];
        Vector3 lastButtonPosition = firstButtonPosition - new Vector3(0, buttonOffset.y, 0);
        float sumHeight = buttonHeight;
        for (int i = 0; i < levelNames.Length; i++)
        {
            float x, y;
            if (sumHeight > maxSumHeight)
            {
                sumHeight = buttonHeight + offsetHeight;
                x = lastButtonPosition.x + buttonOffset.x;
                y = firstButtonPosition.y;
            }
            else
            {
                sumHeight += offsetHeight;
                x = lastButtonPosition.x;
                y = lastButtonPosition.y + buttonOffset.y;
            }
            Vector3 buttonPosition = new Vector3(x, y, 0);
            lastButtonPosition = buttonPosition;
            GameObject buttonObj = 
                Instantiate(buttonPrefab, buttonPosition, Quaternion.identity, transform);
            buttonObj.GetComponentInChildren<Text>().text = levelNames[i];
            buttons[i] = buttonObj.GetComponent<Button>();
        }
    }

    private void AddOnClickToAllButtons()
    {
        foreach (Button button in buttons)
        {
            string levelName = button.gameObject.GetComponentInChildren<Text>().text;
            button.onClick.AddListener(() =>
                GameManager.Instance.OnMapSelected.Invoke(levelName));
        }
    }
}
