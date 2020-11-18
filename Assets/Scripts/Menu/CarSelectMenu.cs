using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectMenu : MonoBehaviour
{
    public Button playButton;
    public Button backButton;

    public Car[] carsForChoice;

    [SerializeField] private CarSelectionArea player0SelectionArea;
    [SerializeField] private CarSelectionArea player1SelectionArea;

    private void Start()
    {
        player0SelectionArea.carSelectMenu = this;
        player1SelectionArea.carSelectMenu = this;
    }
}

[Serializable]
public class CarSelectionArea
{
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Image carImage;
    [Space]
    [SerializeField] private Text durabilityText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Text fuelRateText;

    public CarSelectMenu carSelectMenu;

    private int currentCarNumber;

    private void Start()
    {
        leftArrow.onClick.AddListener(() => PreviousCar());
        rightArrow.onClick.AddListener(() => NextCar());
    }

    private void PreviousCar()
    {
        if (currentCarNumber == 0)
            currentCarNumber = carSelectMenu.carsForChoice.Length - 1;

        ShowCurrentCar();
    }

    private void NextCar()
    {
        if (currentCarNumber == carSelectMenu.carsForChoice.Length - 1)
            currentCarNumber = 0;

        ShowCurrentCar();
    }

    private void ShowCurrentCar()
    {
        Car currentCar = carSelectMenu.carsForChoice[currentCarNumber];
        carImage.sprite = currentCar.sprite;
        durabilityText.text = currentCar.specs.durability.ToString();
        fuelText.text = currentCar.specs.fuel.ToString();
        fuelRateText.text = currentCar.specs.fuelRate.ToString();
    }

    private void OnEnable()
    {
        currentCarNumber = 0;
        ShowCurrentCar();
    }
}
