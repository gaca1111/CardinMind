﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Custom_Mode_Script : MonoBehaviour
{
    public Button PlayButton, ColorChoosingButton;
    public Slider AllowedMistakesSlider;
    public InputField NumberOfFiguresInputField, TimeRestrictionInputField;
    public Dropdown CardSizeDropdown, CardChoosingDropdown, GameModeDropdown, ColorFilingDropdown;
    public Text AllowedMistakesText;

	// Use this for initialization
	void Start ()
	{
	    var playButton = PlayButton.GetComponent<Button>();
	    var colorChoosingButton = ColorChoosingButton.GetComponent<Button>();
        SetInitialValue();
        AllowedMistakesSlider.onValueChanged.AddListener(AllowedMistakesSliderOnValueChange);
        playButton.onClick.AddListener(PlayOnClick);
        colorChoosingButton.onClick.AddListener(ShowPopUp);
	}

    //ColorChoosingButton.transform.position.y
    private Rect buttonRect;
    void ShowPopUp()
    {
        buttonRect= new Rect(0, 0, 200, 250);
        PopupWindow.Show(buttonRect, new PopUpMenu());
    }

    void PlayOnClick()
    {
        var difficultyModifiers = SettingDificulty();
        var cardGenerator = gameObject.AddComponent<Card_Generator>();
        var col = new List<Shape.Figures_Colours>();

        if(PlayerPrefs.GetInt("Light_Blue") == 1) col.Add(Shape.Figures_Colours.Light_Blue);
        if(PlayerPrefs.GetInt("Dark_Blue") == 1) col.Add(Shape.Figures_Colours.Dark_Blue);
        if(PlayerPrefs.GetInt("Light_Green") == 1) col.Add(Shape.Figures_Colours.Light_Green);
        if(PlayerPrefs.GetInt("Dark_Green") == 1) col.Add(Shape.Figures_Colours.Dark_Green);
        if(PlayerPrefs.GetInt("Violet") == 1) col.Add(Shape.Figures_Colours.Violet);
        if(PlayerPrefs.GetInt("Pink") == 1) col.Add(Shape.Figures_Colours.Pink);
        if(PlayerPrefs.GetInt("Red") == 1) col.Add(Shape.Figures_Colours.Red);
        if(PlayerPrefs.GetInt("Yellow") == 1) col.Add(Shape.Figures_Colours.Yellow);
        if(PlayerPrefs.GetInt("Orange") == 1) col.Add(Shape.Figures_Colours.Orange);

        difficultyModifiers.Set_Figures_Colours(col);

        cardGenerator.Generate_Card(difficultyModifiers);
    }
    
    void AllowedMistakesSliderOnValueChange(float arg)
    {
        AllowedMistakesText.text = AllowedMistakesSlider.value.ToString();
    }

    void SetInitialValue()
    {
        NumberOfFiguresInputField.text = PlayerPrefs.GetInt("NumberOfFigures").ToString();
        CardSizeDropdown.value = PlayerPrefs.GetString("CardType") != Difficulty_Modifiers.CardType.Cart_Type70.ToString() ? 0 : 1;
        AllowedMistakesSlider.value = PlayerPrefs.GetInt("NumberOfMistakes");
        ColorFilingDropdown.value = PlayerPrefs.GetInt("ColoursOnlyMechanic");
        GameModeDropdown.value = PlayerPrefs.GetInt("GameMode");
        AllowedMistakesText.text = AllowedMistakesSlider.value.ToString();
    }

    Difficulty_Modifiers SettingDificulty()
    {
        var difficultyModifiers = gameObject.AddComponent<Difficulty_Modifiers>() as Difficulty_Modifiers;

        difficultyModifiers.cardType = CardSizeDropdown.value == 0
            ? Difficulty_Modifiers.CardType.Cart_Type12
            : Difficulty_Modifiers.CardType.Cart_Type70;
        difficultyModifiers.Number_of_figures = NumberOfFiguresInputField.text != "" ? int.Parse(NumberOfFiguresInputField.text) : 12;
        difficultyModifiers.Number_of_mistakes = int.Parse(AllowedMistakesSlider.value.ToString()); ;
        difficultyModifiers.Number_of_mistakes = 0;
        difficultyModifiers.Colours_only_mechanic = ColorFilingDropdown;

        switch (GameModeDropdown.value)
        {
            case 0:
                difficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Random;
                PlayerPrefs.SetInt("GameMode", 0);
                break;
            case 1:
                difficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Both;
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
        PlayerPrefs.SetInt("ColoursOnlyMechanic", difficultyModifiers.Colours_only_mechanic.ToString() == "TRUE" ? 1 : 0);
        return difficultyModifiers;
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
