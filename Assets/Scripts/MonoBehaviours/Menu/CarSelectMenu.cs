using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectMenu : MonoBehaviour
{
    public Button playButton;
    public Button backButton;

    public Car_SO[] carsForChoice;

    [SerializeField] private CarSelectionArea player0;
    [SerializeField] private CarSelectionArea player1;

    private void Awake()
    {
        player0.carSelectMenu = this;
        player1.carSelectMenu = this;
        playButton.onClick.AddListener(() => 
            GameManager.Instance.CarsSelected(player0.CurrentCar, player1.CurrentCar));
        playButton.onClick.AddListener(() =>
            GameManager.Instance.NamesSelected(player0.playerNameInput.text, player1.playerNameInput.text));

        player0.Start();
        player1.Start();
    }

    private void OnEnable()
    {
        player0.ResetCurrentCarNumber();
        player0.ShowCurrentCar();
        player1.ResetCurrentCarNumber();
        player1.ShowCurrentCar();
    }
}

[Serializable]
public class CarSelectionArea
{
    public InputField playerNameInput;
    [Space]
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Image carImage;
    [SerializeField] private Text carName;
    [SerializeField] private Text carDescription;
    [Space]
    [SerializeField] private Text durabilityText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Text fuelRateText;
    [SerializeField] private Text topSpeedText;
    [Space]
    [SerializeField] private Text abilityName;
    [SerializeField] private Image abilityIcon;

    public CarSelectMenu carSelectMenu;

    private int currentCarNumber;
    public Car_SO CurrentCar => carSelectMenu.carsForChoice[currentCarNumber];

    public void Start()
    {
        leftArrow.onClick.AddListener(PreviousCar);
        rightArrow.onClick.AddListener(NextCar);
    }

    private void PreviousCar()
    {
        if (currentCarNumber == 0)
            currentCarNumber = carSelectMenu.carsForChoice.Length - 1;
        else
            currentCarNumber--;

        ShowCurrentCar();
    }

    private void NextCar()
    {
        if (currentCarNumber == carSelectMenu.carsForChoice.Length - 1)
            currentCarNumber = 0;
        else
            currentCarNumber++;

        ShowCurrentCar();
    }

    public void ShowCurrentCar()
    {        
        carImage.sprite = CurrentCar.sprite;
        carName.text = CurrentCar.name;
        carDescription.text = CurrentCar.decription;

        durabilityText.text = CurrentCar.specs.durability.ToString();
        fuelText.text = CurrentCar.specs.fuel.ToString();
        fuelRateText.text = CurrentCar.specs.fuelRate.ToString();
        topSpeedText.text = CurrentCar.specs.topSpeed.ToString();

        abilityName.text = CurrentCar.Ability.AbilityInfo.name;
        abilityIcon.sprite = CurrentCar.Ability.AbilityInfo.abilitySprite;
    }

    public void ResetCurrentCarNumber() => currentCarNumber = 0;
}
