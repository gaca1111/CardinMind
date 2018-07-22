using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour {

	public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneToEasy(string sceneName)
    {
        var difficultyModifiers = SettingDificulty();
        var cardGenerator = gameObject.AddComponent<Card_Generator>();
        cardGenerator.Generate_Card(difficultyModifiers);
        //var card = cardGenerator.Generate_Card(difficultyModifiers);
        
        SceneManager.LoadScene(sceneName);
    }

    Difficulty_Modifiers SettingDificulty()
    {
        var difficultyModifiers = gameObject.AddComponent<Difficulty_Modifiers>();
        var col = new List<Shape.Figures_Colours>
        {
            Shape.Figures_Colours.Light_Blue,
            Shape.Figures_Colours.Dark_Blue,
            Shape.Figures_Colours.Light_Green
        };

        Difficulty_Modifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
        Difficulty_Modifiers.Number_of_figures = 4;
        Difficulty_Modifiers.Number_of_mistakes = 3;
        Difficulty_Modifiers.Colours_only_mechanic = true;
        Difficulty_Modifiers.Set_Figures_Colours(col);
        Difficulty_Modifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Random;
                
        return difficultyModifiers;
    }
}
