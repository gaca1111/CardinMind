using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour
{

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
    private int abcQuestionsAsked;
    private int yesNoQuestionsAsked;
    private bool answeredQuestion = false;
    private bool isYesNoQuestion = false;
    public InputField UserAnswer;
    public Button NextButton, AnswerOptionA, AnswerOptionB, AnswerOptionC, AnswerOptionD;
    public Text QuestionField;

    void Start()
    {
        CreateDictionaries();
        askedQuestions = new List<int>();
        if (CardDrawer.FiguresList != null) PopulateDictionaries();

        QuestionField.text = CreateQuestion();
        NextButton.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        if (answeredQuestion)
        {
            QuestionField.text = CreateQuestion();
            answeredQuestion = false;
            if (abcQuestionsAsked < 13) NextButton.GetComponentInChildren<Text>().text = "Odpowiedz";
            else
            {
                NextButton.GetComponentInChildren<Text>().text = "Nowa karta";
                NextButton.onClick.AddListener(NewCard);
            }
        }
        else ValidateAnswer();
    }

    public void NewCard()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void CreateDictionaries()
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

    public void PopulateDictionaries()
    {
        foreach (var figure in CardDrawer.FiguresList)
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
    }

    private void ButtonsPopulate(int rightAnswer)
    {
        if (isYesNoQuestion)
        {
            AnswerOptionA.GetComponentInChildren<Text>().text = "";
            AnswerOptionB.GetComponentInChildren<Text>().text = "TAK";
            AnswerOptionC.GetComponentInChildren<Text>().text = "NIE";
            AnswerOptionD.GetComponentInChildren<Text>().text = "";
        }
        else
        {
            var rand = new System.Random();
            var wrongAnswers = new List<int>
            {
                rand.Next(10),
                rand.Next(10),
                rand.Next(10),
                rand.Next(10)
            };
            var intHelper = rand.Next(4);
            var list = wrongAnswers.Distinct().ToList();

            AnswerOptionA.GetComponentInChildren<Text>().text =
                intHelper == 0 ? rightAnswer.ToString() : list[0].ToString();
            AnswerOptionB.GetComponentInChildren<Text>().text =
                intHelper == 1 ? rightAnswer.ToString() : list[1].ToString();
            AnswerOptionC.GetComponentInChildren<Text>().text =
                intHelper == 2 ? rightAnswer.ToString() : list[2].ToString();
            AnswerOptionD.GetComponentInChildren<Text>().text =
                intHelper == 3 ? rightAnswer.ToString() : list[3].ToString();
        }
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

        if (!isYesNoQuestion &&)
            question = CreateAbcQuestion();
        else
            question = CreateYesNoQuestion();

        askedQuestions.Add(questionType);
        return question;
    }


    private string CreateAbcQuestion()
    {
        var rand = new System.Random();
        while (true)
        {
            questionType = rand.Next(14);
            if (!askedQuestions.Contains(questionType)) break;
        }

        switch (questionType)
        {
            case 0:
                ButtonsPopulate(shapesDictionary["Rectangles"]);
                return "Ile prostokątów znajduje się na karcie?";
            case 1:
                ButtonsPopulate(shapesDictionary["Circle"]);
                return "Ile kół znajduje się na karcie?";
            case 2:
                ButtonsPopulate(shapesDictionary["Square"]);
                return "Ile kwadratów znajduje się na karcie?";
            case 3:
                ButtonsPopulate(shapesDictionary["Traingle"]);
                return "Ile trójkątów znajduje się na karcie?";
            case 4:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Light_Blue]);
                return "Ile jasno niebieskich figur znajduje się na karcie?";
            case 5:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Dark_Blue]);
                return "Ile ciemno niebieskich figur znajduje się na karcie?";
            case 6:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Light_Green]);
                return "Ile jasno zielonych figur znajduje się na karcie?";
            case 7:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Dark_Green]);
                return "Ile ciemno zielonych figur znajduje się na karcie?";
            case 8:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Violet]);
                return "Ile fioletowych figur znajduje się na karcie?";
            case 9:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Pink]);
                return "Ile różowych figur znajduje się na karcie?";
            case 10:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Red]);
                return "Ile czerwonych figur znajduje się na karcie?";
            case 11:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Yellow]);
                return "Ile żółtych figur znajduje się na karcie?";
            case 12:
                ButtonsPopulate(coloursDictionary[Shape.Figures_Colours.Orange]);
                return "Ile pomarańczowych figur znajduje się na karcie?";
            default:
                ButtonsPopulate(numberOfFigures);
                return "Ile figur znajduje się na karcie";
        }
    }

    private string CreateYesNoQuestion()
    {
        string question;

        return question;
    }

    private void ValidateAnswer()
    {
        answeredQuestion = true;
        if (ValidateAbcAnswer(UserAnswer.text)) QuestionField.text = "Poprawna odpowiedź";
        else QuestionField.text = "Błędna odpowiedź";
        NextButton.GetComponentInChildren<Text>().text = "Następne pytanie";
    }

    public bool ValidateAbcAnswer(string userResponse)
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
                if (coloursDictionary[Shape.Figures_Colours.Red] == userResponseInt) return true;
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
