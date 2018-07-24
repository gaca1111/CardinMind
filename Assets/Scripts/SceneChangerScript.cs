using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    private Animator _animator;
    private string _sceneToChange;
    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartEasy()
    {
        Static.DifficultyModifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
        Static.DifficultyModifiers.Number_of_figures = 3;
        Static.DifficultyModifiers.Set_Figures_Colours(new List<Shape.Figures_Colours>()
        {
            Shape.Figures_Colours.Orange,
            Shape.Figures_Colours.Dark_Green,
            Shape.Figures_Colours.Light_Blue,
            Shape.Figures_Colours.Red,
            Shape.Figures_Colours.Yellow
        });
        Static.DifficultyModifiers.Card_pick_mechanic = true;
        Static.DifficultyModifiers.Number_of_mistakes = 5;
        Static.DifficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Assembling;
        Static.DifficultyModifiers.TimeRestriction = 10;
        Static.DifficultyModifiers.Colours_only_mechanic = false;
        StartGame();
    }

    public void StartHard()
    {
        Static.DifficultyModifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type70;
        Static.DifficultyModifiers.Number_of_figures = 5;
        Static.DifficultyModifiers.Set_Figures_Colours(new List<Shape.Figures_Colours>()
        {
            Shape.Figures_Colours.Orange,
            Shape.Figures_Colours.Dark_Green,
            Shape.Figures_Colours.Light_Blue,
            Shape.Figures_Colours.Red,
            Shape.Figures_Colours.Yellow,
            Shape.Figures_Colours.Dark_Blue,
            Shape.Figures_Colours.Light_Green,
            Shape.Figures_Colours.Pink,
            Shape.Figures_Colours.Violet
        });
        Static.DifficultyModifiers.Card_pick_mechanic = false;
        Static.DifficultyModifiers.Number_of_mistakes = 5;
        Static.DifficultyModifiers.Game_mode = Difficulty_Modifiers.Game_Mode.Random;
        Static.DifficultyModifiers.TimeRestriction = 5;
        Static.DifficultyModifiers.Colours_only_mechanic = false;
        StartGame();
    }

    public void StartCustom()
    {
        ChangeScene("Custom_Mode");
    }

    public void StartGame()
    {
        ChangeScene(Static.DifficultyModifiers.Card_pick_mechanic ? "GameChooseScene" : "GameRememberScene");
    }

    public void GoToRemember()
    {
        ChangeScene("GameRememberScene");
    }

     public void CardRememberTimeOut()
    {
        switch (Static.DifficultyModifiers.Game_mode)
        {
            case Difficulty_Modifiers.Game_Mode.Questions:
                ChangeScene("Question_Scene");
                break;
            case Difficulty_Modifiers.Game_Mode.Assembling:
                ChangeScene("GameDrawScene");
                break;
            case Difficulty_Modifiers.Game_Mode.Random:
            {
                var random = Random.Range(0, 2);
                ChangeScene(random == 0 ? "Question_Scene" : "GameDrawScene");
                break;
            }
            case Difficulty_Modifiers.Game_Mode.Alternately:
                break;
            default:
            {
                var random = Random.Range(0, 2);
                ChangeScene(random == 0 ? "Question_Scene" : "GameDrawScene");
                break;
            }
        }
    }

    public void EndGame()
    {
        ChangeScene("PlayAgainScene");
    }

    public void BackToMainMenu()
    {
        ChangeScene("Main_Menu");
    }

    void ChangeScene(string sceneName)
    {
        transform.SetAsLastSibling();
        _sceneToChange = sceneName;
        _animator.SetTrigger("FadeOut");
    }

    void OnFadeOutEnd()
    {
        SceneManager.LoadScene(_sceneToChange);
    }
}
