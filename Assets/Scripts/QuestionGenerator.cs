using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour {

    private enum Possibilities
    {
        ShapeRectangle,
        ShapeCircle,
        ShapeSquare,
        ShapeTriangle,
        ColourLightBlue,
        ColourDarkBlue,
        ColourLightGreen,
        ColourDarkGreen,
        ColourViolet,
        ColourPink,
        ColourRed,
        ColourYellow,
        ColourOrange,
        NumberOfFigures
    }

    private List<int> askedQuestions;
    private Dictionary<Shape.Figures_Colours, int> coloursDictionary;
    private Dictionary<string, int> shapesDictionary;
    private int numberOfFigures;
    private int questionType;
    private int questionsAsked;
    private bool answeredQuestion = false;
    public InputField UserAnswer;
    public Button NextButton;
    public Text QuestionField;


    private List<Shape_With_Place> FiguresList;
    
    void Start ()
    {
        PopulateDictionaries();
        askedQuestions = new List<int>();
        FiguresList = CardDrawer.FiguresList;
        if(FiguresList != null)
            foreach (var figure in FiguresList)
	        {
	            if (figure.shape is Rectangle) shapesDictionary["Rectangle"]++;
	            if (figure.shape is Circle) shapesDictionary["Circle"]++;
	            if (figure.shape is Square) shapesDictionary["Square"]++;
	            if (figure.shape is Triangle) shapesDictionary["Triangle"]++;

	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Light_Blue)
	                coloursDictionary[Shape.Figures_Colours.Light_Blue]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Dark_Blue)
	                coloursDictionary[Shape.Figures_Colours.Dark_Blue]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Light_Green)
	                coloursDictionary[Shape.Figures_Colours.Light_Green]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Dark_Green)
	                coloursDictionary[Shape.Figures_Colours.Dark_Green]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Violet)
	                coloursDictionary[Shape.Figures_Colours.Violet]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Pink)
	                coloursDictionary[Shape.Figures_Colours.Pink]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Red)
	                coloursDictionary[Shape.Figures_Colours.Red]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Yellow)
	                coloursDictionary[Shape.Figures_Colours.Yellow]++;
	            if (figure.shape.Get_Colour() == Shape.Figures_Colours.Orange)
	                coloursDictionary[Shape.Figures_Colours.Orange]++;

	            numberOfFigures++;
	        }

        QuestionField.text = CreateQuestion();
        NextButton.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        if (answeredQuestion)
        {
            QuestionField.text = CreateQuestion();
            answeredQuestion = false;
            if(questionsAsked < 13)NextButton.GetComponentInChildren<Text>().text = "Odpowiedz";
            else
            {
                NextButton.onClick.AddListener(NewCard);
                NextButton.GetComponentInChildren<Text>().text = "Nowa karta";
            }
        }
        else ValidatingAnswer();
    }

    public void NewCard()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    private void ValidatingAnswer()
    {
        answeredQuestion = true;
        if (ValidateQuestion(UserAnswer.text)) QuestionField.text = "Poprawna odpowiedź";
        else QuestionField.text = "Błędna odpowiedź";
        NextButton.GetComponentInChildren<Text>().text = "Następne pytanie";
    }

    public void PopulateDictionaries()
    {
        coloursDictionary = new Dictionary<Shape.Figures_Colours, int>
        {
            {Shape.Figures_Colours.Light_Blue, 0},
            {Shape.Figures_Colours.Dark_Blue, 0},
            {Shape.Figures_Colours.Light_Green, 0},
            {Shape.Figures_Colours.Dark_Green, 0},
            {Shape.Figures_Colours.Violet, 0},
            {Shape.Figures_Colours.Pink, 0},
            {Shape.Figures_Colours.Red, 0},
            {Shape.Figures_Colours.Yellow, 0},
            {Shape.Figures_Colours.Orange, 0}
        };
        shapesDictionary = new Dictionary<string, int>
        {
            {"Rectangle", 0},
            {"Circle", 0},
            {"Square", 0},
            {"Triangle", 0}
        };

    }

    private string CreateQuestion()
    {
        string question;
        var rand = new System.Random();
        questionsAsked++;
        if (askedQuestions.Count == 13) askedQuestions.Clear();
        while (true)
        {
            questionType = rand.Next(14);
            if (!askedQuestions.Contains(questionType)) break;
        }

        switch (questionType)
        {
            case 0:
                question = "Ile prostokątów znajduje się na karcie?";
                break;
            case 1:
                question = "Ile kół znajduje się na karcie?";
                break;
            case 2:
                question = "Ile kwadratów znajduje się na karcie?";
                break;
            case 3:
                question = "Ile trójkątów znajduje się na karcie?";
                break;
            case 4:
                question = "Ile jasno niebieskich figur znajduje się na karcie?";
                break;
            case 5:
                question = "Ile ciemno niebieskich figur znajduje się na karcie?";
                break;
            case 6:
                question = "Ile jasno zielonych figur znajduje się na karcie?";
                break;
            case 7:
                question = "Ile ciemno zielonych figur znajduje się na karcie?";
                break;
            case 8:
                question = "Ile fioletowych figur znajduje się na karcie?";
                break;
            case 9:
                question = "Ile różowych figur znajduje się na karcie?";
                break;
            case 10:
                question = "Ile czerwonych figur znajduje się na karcie?";
                break;
            case 11:
                question = "Ile żółtych figur znajduje się na karcie?";
                break;
            case 12:
                question = "Ile pomarańczowych figur znajduje się na karcie?";
                break;
            default:
                question = "Ile figur znajduje się na karcie";
                break;
        }

        askedQuestions.Add(questionType);
        return question;
    }

    public bool ValidateQuestion(string userResponse)
    {
        var userResponseInt = Int32.Parse(userResponse);
        switch (questionType)
        {
            case 0:
                if (shapesDictionary["Rectangle"] == userResponseInt) return true;
                break;
            case 1:
                if (shapesDictionary["Circle"] == userResponseInt) return true;
                break;
            case 2:
                if (shapesDictionary["Square"] == userResponseInt) return true;
                break;
            case 3:
                if (shapesDictionary["Triangle"] == userResponseInt) return true;
                break;
            case 4:
                if (coloursDictionary[Shape.Figures_Colours.Light_Blue] == userResponseInt) return true;
                break;
            case 5:
                if (coloursDictionary[Shape.Figures_Colours.Dark_Blue] == userResponseInt) return true;
                break;
            case 6:
                if (coloursDictionary[Shape.Figures_Colours.Light_Green] == userResponseInt) return true;
                break;
            case 7:
                if (coloursDictionary[Shape.Figures_Colours.Dark_Green] == userResponseInt) return true;
                break;
            case 8:
                if (coloursDictionary[Shape.Figures_Colours.Violet] == userResponseInt) return true;
                break;
            case 9:
                if (coloursDictionary[Shape.Figures_Colours.Pink] == userResponseInt) return true;
                break;
            case 10:
                if(coloursDictionary[Shape.Figures_Colours.Red] == userResponseInt) return true;
                break;
            case 11:
                if (coloursDictionary[Shape.Figures_Colours.Yellow] == userResponseInt) return true;
                break;
            case 12:
                if (coloursDictionary[Shape.Figures_Colours.Orange] == userResponseInt) return true;
                break;
            default:
                if (numberOfFigures == userResponseInt) return true;
                break;
        }
        return false;
    }
}
