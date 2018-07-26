using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Custom_Mode_Script : MonoBehaviour
{
    public Button PlayButton, ColorChoosingButton, DarkBlueButton, LightBlueButton, DarkGreenButton, LightGreenButton, VioletButton, PinkButton, RedButton, YellowButton, OrangeButton;
    public Image LightBlueImage, DarkBlueImage, LightGreenImage, DarkGreenImage, VioletImage, PinkImage, RedImage, YellowImage, OrangeImage;
    public Slider AllowedMistakesSlider, NumberOfFiguresSlider;
    public InputField TimeRestrictionInputField;
    public Dropdown CardSizeDropdown, CardChoosingDropdown, GameModeDropdown, ColorFilingDropdown;
    public Text AllowedMistakesText, NumberOfFiguresText;
    private bool isColorChoosing;
    private List<Shape.Figures_Colours> coloursList;
    public SceneChangerScript SceneChanger;

	// Use this for initialization
	void Start ()
	{
	    isColorChoosing = false;
        coloursList = new List<Shape.Figures_Colours>();
        SetInitialValue();
	    AddListeners();
	}

    void AddListeners()
    {
        var playButton = PlayButton.GetComponent<Button>();
        var colorChoosingButton = ColorChoosingButton.GetComponent<Button>();
        AllowedMistakesSlider.onValueChanged.AddListener(AllowedMistakesSliderOnValueChange);
        NumberOfFiguresSlider.onValueChanged.AddListener(NumberOfFiguresSliderOnValueChange);
        CardSizeDropdown.onValueChanged.AddListener(CardSizeDropdownOnValueChange);
        playButton.onClick.AddListener(PlayOnClick);
        colorChoosingButton.onClick.AddListener(ShowColourChoosing);
        DarkBlueButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Dark_Blue); });
        LightBlueButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Light_Blue); });
        DarkGreenButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Dark_Green); });
        LightGreenButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Light_Green); });
        VioletButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Violet); });
        PinkButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Pink); });
        RedButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Red); });
        YellowButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Yellow); });
        OrangeButton.onClick.AddListener(delegate { ColourButtonClicked(Shape.Figures_Colours.Orange); });
    }

    private Rect buttonRect;
    void ShowColourChoosing()
    {
        isColorChoosing = !isColorChoosing;
        DarkBlueButton.gameObject.SetActive(isColorChoosing);
        LightBlueButton.gameObject.SetActive(isColorChoosing);
        DarkGreenButton.gameObject.SetActive(isColorChoosing);
        LightGreenButton.gameObject.SetActive(isColorChoosing);
        VioletButton.gameObject.SetActive(isColorChoosing);
        PinkButton.gameObject.SetActive(isColorChoosing);
        RedButton.gameObject.SetActive(isColorChoosing);
        YellowButton.gameObject.SetActive(isColorChoosing);
        OrangeButton.gameObject.SetActive(isColorChoosing);
        if(isColorChoosing) ButtonDrawBorder();
        else ClearColoursBorders();
    }

    void ColourButtonClicked(Shape.Figures_Colours colour)
    {
        if (coloursList.Contains(colour)) coloursList.Remove(colour);
        else coloursList.Add(colour); 
        ButtonDrawBorder();
    }

    void ClearColoursBorders()
    {
        LightBlueImage.gameObject.SetActive(false);
        DarkBlueImage.gameObject.SetActive(false);
        LightGreenImage.gameObject.SetActive(false);
        DarkGreenImage.gameObject.SetActive(false);
        VioletImage.gameObject.SetActive(false);
        PinkImage.gameObject.SetActive(false);
        RedImage.gameObject.SetActive(false);
        YellowImage.gameObject.SetActive(false);
        OrangeImage.gameObject.SetActive(false);
    }

    void ButtonDrawBorder()
    {
        ClearColoursBorders();
        foreach (var colour in coloursList)
        {
            switch (colour)
            {
                case Shape.Figures_Colours.Light_Blue:
                    LightBlueImage.gameObject.SetActive(true);
                break;
                case Shape.Figures_Colours.Dark_Blue:
                    DarkBlueImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Light_Green:
                    LightGreenImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Dark_Green:
                    DarkGreenImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Violet:
                    VioletImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Pink:
                    PinkImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Red:
                    RedImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Yellow:
                    YellowImage.gameObject.SetActive(true);
                    break;
                case Shape.Figures_Colours.Orange:
                    OrangeImage.gameObject.SetActive(true);
                    break;
            }
        }
    }

    void PlayOnClick()
    {
        Static.DifficultyModifiers = DifficultyToPlayerPref();
        SceneChanger.StartGame();
    }

    void AllowedMistakesSliderOnValueChange(float arg)
    {
        AllowedMistakesText.text = AllowedMistakesSlider.value.ToString();
    }

    void NumberOfFiguresSliderOnValueChange(float arg)
    {
        NumberOfFiguresText.text = NumberOfFiguresSlider.value.ToString();
    }

    void CardSizeDropdownOnValueChange(int arg)
    {
        if (CardSizeDropdown.value == 0) NumberOfFiguresSlider.maxValue = 12;
        else NumberOfFiguresSlider.maxValue = 70;
    }

    void SetInitialValue()
    {
        NumberOfFiguresSlider.value = PlayerPrefs.GetInt("NumberOfFigures");
        CardSizeDropdown.value = PlayerPrefs.GetString("CardType") != Difficulty_Modifiers.CardType.Cart_Type70.ToString() ? 0 : 1;
        AllowedMistakesSlider.value = PlayerPrefs.GetInt("NumberOfMistakes");
        ColorFilingDropdown.value = PlayerPrefs.GetInt("ColoursOnlyMechanic");
        GameModeDropdown.value = PlayerPrefs.GetInt("GameMode");
        CardChoosingDropdown.value = PlayerPrefs.GetInt("CardChoosing");

        TimeRestrictionInputField.text = PlayerPrefs.GetInt("TimeRestriction").ToString();
        AllowedMistakesText.text = AllowedMistakesSlider.value.ToString();
        NumberOfFiguresText.text = NumberOfFiguresSlider.value.ToString();

        if (PlayerPrefs.GetInt("Light_Blue") == 1) coloursList.Add(Shape.Figures_Colours.Light_Blue);
        if (PlayerPrefs.GetInt("Dark_Blue") == 1) coloursList.Add(Shape.Figures_Colours.Dark_Blue);
        if (PlayerPrefs.GetInt("Light_Green") == 1) coloursList.Add(Shape.Figures_Colours.Light_Green);
        if (PlayerPrefs.GetInt("Dark_Green") == 1) coloursList.Add(Shape.Figures_Colours.Dark_Green);
        if (PlayerPrefs.GetInt("Violet") == 1) coloursList.Add(Shape.Figures_Colours.Violet);
        if (PlayerPrefs.GetInt("Pink") == 1) coloursList.Add(Shape.Figures_Colours.Pink);
        if (PlayerPrefs.GetInt("Red") == 1) coloursList.Add(Shape.Figures_Colours.Red);
        if (PlayerPrefs.GetInt("Yellow") == 1) coloursList.Add(Shape.Figures_Colours.Yellow);
        if (PlayerPrefs.GetInt("Orange") == 1) coloursList.Add(Shape.Figures_Colours.Orange);
    }

    Difficulty_Modifiers DifficultyToPlayerPref()
    {
        var maxNumberOfFigures = NumberOfFiguresSlider.value;
        var difficultyModifiers = new Difficulty_Modifiers
        {
            cardType = CardSizeDropdown.value == 0
                ? Difficulty_Modifiers.CardType.Cart_Type12
                : Difficulty_Modifiers.CardType.Cart_Type70
        };

        if (difficultyModifiers.cardType == Difficulty_Modifiers.CardType.Cart_Type12 &&
            NumberOfFiguresSlider.value  > 11) maxNumberOfFigures = 11;
        if (difficultyModifiers.cardType == Difficulty_Modifiers.CardType.Cart_Type70 &&
            NumberOfFiguresSlider.value > 69) maxNumberOfFigures = 69;
        difficultyModifiers.Number_of_figures = int.Parse(maxNumberOfFigures.ToString());
        difficultyModifiers.Number_of_mistakes = int.Parse(AllowedMistakesSlider.value.ToString());
        difficultyModifiers.Colours_only_mechanic = ColorFilingDropdown.value == 0;
        difficultyModifiers.TimeRestriction = int.Parse(TimeRestrictionInputField.text);
        difficultyModifiers.Set_Figures_Colours(coloursList);
        difficultyModifiers.Card_pick_mechanic = CardChoosingDropdown.value == 0;
        ColourListToPlayerPrefs();

        switch (GameModeDropdown.value)
        {
            case 0:
                difficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Random;
                PlayerPrefs.SetInt("GameMode", 0);
                break;
            case 1:
                difficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Assembling;
                PlayerPrefs.SetInt("GameMode", 1);
                break;
            default:
                difficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Questions;
                PlayerPrefs.SetInt("GameMode", 2);
                break;
        }
        PlayerPrefs.SetString("CardType", difficultyModifiers.cardType.ToString());
        PlayerPrefs.SetInt("NumberOfFigures", difficultyModifiers.Number_of_figures);
        PlayerPrefs.SetInt("NumberOfMistakes", difficultyModifiers.Number_of_mistakes);
        PlayerPrefs.SetInt("ColoursOnlyMechanic", difficultyModifiers.Colours_only_mechanic ? 0 : 1);
        PlayerPrefs.SetInt("TimeRestriction", int.Parse(TimeRestrictionInputField.text));
        PlayerPrefs.SetInt("CardChoosing", difficultyModifiers.Card_pick_mechanic ? 0 : 1);
        return difficultyModifiers;
    }

    void ColourListToPlayerPrefs()
    {
        PlayerPrefs.SetInt("Light_Blue", coloursList.Contains(Shape.Figures_Colours.Light_Blue) ? 1 : 0);
        PlayerPrefs.SetInt("Dark_Blue", coloursList.Contains(Shape.Figures_Colours.Dark_Blue) ? 1 : 0);
        PlayerPrefs.SetInt("Light_Green", coloursList.Contains(Shape.Figures_Colours.Light_Green) ? 1 : 0);
        PlayerPrefs.SetInt("Dark_Green", coloursList.Contains(Shape.Figures_Colours.Dark_Green) ? 1 : 0);
        PlayerPrefs.SetInt("Violet", coloursList.Contains(Shape.Figures_Colours.Violet) ? 1 : 0);
        PlayerPrefs.SetInt("Pink", coloursList.Contains(Shape.Figures_Colours.Pink) ? 1 : 0);
        PlayerPrefs.SetInt("Red", coloursList.Contains(Shape.Figures_Colours.Red) ? 1 : 0);
        PlayerPrefs.SetInt("Yellow", coloursList.Contains(Shape.Figures_Colours.Yellow) ? 1 : 0);
        PlayerPrefs.SetInt("Orange", coloursList.Contains(Shape.Figures_Colours.Orange) ? 1 : 0);
    }
}
