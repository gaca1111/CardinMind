using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour
{
    #region Enums

    private enum ColoursInAbcQuestions
    {
        niebieskich,
        ciemnoniebieskich,
        zielonych,
        ciemnozielonych,
        fioletowych,
        różowych,
        czerwony,
        żółtych,
        pomarańczowych
    }

    private enum ShapesInAbcQuestions
    {
        prostokątów,
        kół,
        kwadratów,
        trójkątów, 
        figur
    }

    private enum ColoursInYesNoQuestions
    {
        niebieskie,
        ciemnoniebieskie,
        zielone,
        ciemnozielone,
        fioletowe,
        różowe,
        czerwone,
        żółte,
        pomarańczowe
    }

    private enum ShapesInYesNoQuestions
    {
        prostokąty,
        koła,
        kwadraty,
        trójkąty,
        figury
    }

    private enum ColoursInValidation
    {
        LightBlue,
        DarkBlue,
        LightGreen,
        DarkGreen,
        Violet,
        Pink,
        Red,
        Yellow,
        Orange
    }

    private enum ShapesInValidation
    {
        Rectangle,
        Circle,
        Square,
        Triangle
    }

    #endregion

    #region ClassFields

    private List<Shape_With_Place> figuresList;
    private List<int> askedQuestions;
    private Dictionary<Shape.Figures_Colours, int> coloursDictionary;
    private Dictionary<string, int> shapesDictionary;
    private int numberOfFigures;
    private int questionType;
    private int questionsAsked;
    private int abcQuestionsAsked;
    private int yesNoQuestionsAsked;
    private int userAnswer;
    private bool answeredQuestion = false;
    private bool isYesNoQuestion = false;
    public Button NextButton, AnswerOptionA, AnswerOptionB, AnswerOptionC, AnswerOptionD;
    public Text QuestionField;

    #endregion

    void Start()
    {
        CreateDictionaries();
        askedQuestions = new List<int>();
        figuresList = CardDrawer.FiguresList;
        if (figuresList != null) PopulateDictionaries();

        QuestionField.text = CreateQuestion();
        NextButton.onClick.AddListener(AnswerButton);
        AnswerOptionA.onClick.AddListener(AnswerOptionButton);
        AnswerOptionB.onClick.AddListener(AnswerOptionButton);
        AnswerOptionC.onClick.AddListener(AnswerOptionButton);
        AnswerOptionD.onClick.AddListener(AnswerOptionButton);
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

    #region ButtonActions

    private void AnswerButton()
    {
        if (answeredQuestion)
        {
            QuestionField.text = CreateQuestion();
            answeredQuestion = false;
            if (abcQuestionsAsked < 100) NextButton.GetComponentInChildren<Text>().text = "Odpowiedz";
            else
            {
                NextButton.GetComponentInChildren<Text>().text = "Nowa karta";
                NextButton.onClick.AddListener(NewCardButton);
            }
        }
        else ValidateAnswer();
    }

    public void NewCardButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void AnswerOptionButton()
    {
        SetUserAnswerFromButton(EventSystem.current.currentSelectedGameObject.name);
    }
    
    #endregion
    
    #region PopulateInstances
    
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

    private void PopulateButtons(int rightAnswer)
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
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12),
                rand.Next(12)
            };
            var intHelper = rand.Next(4);
            wrongAnswers.RemoveAll(x => x == rightAnswer);
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

    private void SetUserAnswerFromButton(string buttonName)
    {
        if (isYesNoQuestion)
            userAnswer = GameObject.Find(buttonName).GetComponentInChildren<Text>().text == "TAK" ? 1 : 0;
        else userAnswer = int.Parse(GameObject.Find(buttonName).GetComponentInChildren<Text>().text);
    }

    #endregion

    #region CreateQuestion

    private string CreateQuestion()
    {
        var rand = new System.Random();
        questionsAsked++;
        if (askedQuestions.Count == 98) askedQuestions.Clear();
        while (true)
        {
            questionType = rand.Next(99);
            if (!askedQuestions.Contains(questionType)) break;
        }

        isYesNoQuestion = questionType >= 50;
        askedQuestions.Add(questionType);
        if(!isYesNoQuestion)PopulateButtons(GetRightAbcAnswer());
        return !isYesNoQuestion ? CreateAbcQuestion() : CreateYesNoQuestion();
    }
    
    private string CreateAbcQuestion()
    {
        var shapeType = questionType / 10;
        var colourType = questionType % 10;
        if (colourType == 9)
            return "Ile " + ((ShapesInAbcQuestions) shapeType).ToString() + " znajduje się na karcie?";
        return "Ile " + ((ColoursInAbcQuestions) colourType).ToString() + " " +
               ((ShapesInAbcQuestions) shapeType).ToString() + " znajduje się na karcie?";
    }

    private string CreateYesNoQuestion()
    {
        var shapeType = (questionType / 10) - 5;
        var colourType = questionType % 10;
        if (colourType == 9)
            return "Czy na karcie znajdują się " + ((ShapesInYesNoQuestions)shapeType).ToString() + "?";
        return "Czy na karcie znajdują się " + ((ColoursInYesNoQuestions)colourType).ToString() + " " +
               ((ShapesInYesNoQuestions)shapeType).ToString() + "?";
    }

    #endregion

    #region VaildateAnswer

    private void ValidateAnswer()
    {
        answeredQuestion = true;
        var answerValidation = !isYesNoQuestion ? ValidateAbcAnswer(userAnswer) : ValidateYesNoAnswer(userAnswer);
        QuestionField.text = answerValidation ? "Poprawna odpowiedź" : "Błędna odpowiedź";
        NextButton.GetComponentInChildren<Text>().text = "Następne pytanie";
    }

    public bool ValidateAbcAnswer(int userResponse)
    {
        var shapeType = questionType / 10;
        var colourType = questionType % 10;

        if (colourType == 9)
        {
            return shapesDictionary[((ShapesInValidation) shapeType).ToString()] == userResponse;
        }

        var counter = 0;
        if (figuresList == null) return userResponse == counter;
        foreach (var figure in figuresList)
        {
            if (figure.shape.ToString() == ((ShapesInValidation) shapeType).ToString() &&
                figure.shape.Get_Colour().ToString() == ((ColoursInValidation) colourType).ToString())
                counter++;
        }


        return userResponse == counter;
    }

    public bool ValidateYesNoAnswer(int userResponse)
    {
        var shapeType = (questionType / 10) - 5;
        var colourType = questionType % 10;
        bool rightAnswer;

        if (colourType == 9)
        {
            rightAnswer =  shapesDictionary[((ShapesInValidation)shapeType).ToString()] > 0;
        }
        else
        {
            
            var counter = 0;
            if (figuresList == null) return userResponse == counter;
            foreach (var figure in figuresList)
            {
                if (figure.shape.ToString() == ((ShapesInValidation) shapeType).ToString() &&
                    figure.shape.Get_Colour().ToString() == ((ColoursInValidation) colourType).ToString())
                    counter++;
            }
            rightAnswer = counter > 0;
        }

        if (userResponse == 0) return rightAnswer == false;
        return rightAnswer;
    }

    #endregion

    private int GetRightAbcAnswer()
    {
            var shapeType = questionType / 10;
            var colourType = questionType % 10;

            if (colourType == 9)
            {
                return shapesDictionary[((ShapesInValidation)shapeType).ToString()];
            }

            var counter = 0;
            if (figuresList == null) return counter;
            foreach (var figure in figuresList)
            {
                if (figure.shape.ToString() == ((ShapesInValidation)shapeType).ToString() &&
                    figure.shape.Get_Colour().ToString() == ((ColoursInValidation)colourType).ToString())
                    counter++;
            }
            return  counter;
       
    }
}