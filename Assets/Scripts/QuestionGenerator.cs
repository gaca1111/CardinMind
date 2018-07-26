using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour
{
    #region Enums

    private enum ColoursInAbcQuestions
    {
        jasnoniebieskich = 0,
        ciemnoniebieskich = 1,
        jasnozielonych = 2,
        ciemnozielonych = 3,
        fioletowych = 4,
        różowych = 5,
        czerwonych = 6,
        żółtych = 7,
        pomarańczowych = 8
    }

    private enum ShapesInAbcQuestions
    {
        prostokątów = 0,
        kół = 1,
        kwadratów = 2,
        trójkątów = 3,
        figur = 4
    }

    private enum ColoursInYesNoQuestions
    {
        jasnoniebieskie = 0,
        ciemnoniebieskie = 1,
        jasnozielone = 2,
        ciemnozielone = 3,
        fioletowe = 4,
        różowe = 5,
        czerwone = 6,
        żółte = 7,
        pomarańczowe = 8
    }

    private enum ShapesInYesNoQuestions
    {
        prostokąty = 0,
        koła = 1,
        kwadraty = 2,
        trójkąty = 3,
        figury = 4
    }

    private enum ColoursInValidation
    {
        Light_Blue = 0,
        Dark_Blue = 1,
        Light_Green = 2,
        Dark_Green = 3,
        Violet = 4,
        Pink = 5,
        Red = 6,
        Yellow = 7,
        Orange = 8
    }

    private enum ShapesInValidation
    {
        Rectangle = 0,
        Circle = 1,
        Square = 2,
        Triangle = 3,
        Null = 4
    }

    #endregion

    #region ClassFields

    private List<Shape_With_Place> figuresList;
    private List<int> askedQuestions;
    private Dictionary<Shape.Figures_Colours, int> coloursDictionary;
    private Dictionary<string, int> shapesDictionary;
    private int questionType;
    private int questionsAsked;
    private int userAnswer;
    private bool answeredQuestion = false;
    private bool isYesNoQuestion = false;
    public Button AnswerOptionA, AnswerOptionB, AnswerOptionC, AnswerOptionD;
    public Text QuestionField;
    public SceneChangerScript SceneChanger;

    #endregion

    #region Initilizers

    void Start()
    {
        CreateDictionaries();
        askedQuestions = new List<int>();
        figuresList = Static.ShapeWithPlaces;
        if (figuresList != null) PopulateDictionaries();
        questionsAsked = 0;

        QuestionField.text = CreateQuestion();
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
            {"Triangle", 0},
            {"Null", 0 }
        };
    }

    #endregion

    #region ButtonAndClickActions
    
    void Update()
    {
        if(Input.anyKey)
        if (answeredQuestion)
        {
            if (questionsAsked > 4) NewCard();
            else
            {
                QuestionField.text = CreateQuestion();
                answeredQuestion = false;
            }
        }
    }

    public void NewCard()
    {
        SceneChanger.EndGame();
    }

    public void AnswerOptionButton()
    {
        var buttonClicked = EventSystem.current.currentSelectedGameObject.name;
        SetUserAnswerFromButton(buttonClicked);
        ValidateAnswer();
        HideButtons();
    }

    private void HideButtons()
    {
        AnswerOptionA.gameObject.SetActive(false);
        AnswerOptionB.gameObject.SetActive(false);
        AnswerOptionC.gameObject.SetActive(false);
        AnswerOptionD.gameObject.SetActive(false);
    }

    #endregion
    
    #region PopulateInstances
    
    public void PopulateDictionaries()
    {
        foreach (var figure in Static.ShapeWithPlaces)
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

            shapesDictionary["Null"]++;
        }
    }

    private void PopulateButtons(int rightAnswer)
    {
        if (isYesNoQuestion)
        {
            AnswerOptionA.gameObject.SetActive(false);
            AnswerOptionB.gameObject.SetActive(true);
            AnswerOptionB.GetComponentInChildren<Text>().text = "TAK";
            AnswerOptionC.gameObject.SetActive(true);
            AnswerOptionC.GetComponentInChildren<Text>().text = "NIE";
            AnswerOptionD.gameObject.SetActive(false);
        }
        else
        {
            AnswerOptionA.gameObject.SetActive(true);
            AnswerOptionB.gameObject.SetActive(true);
            AnswerOptionC.gameObject.SetActive(true);
            AnswerOptionD.gameObject.SetActive(true);
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

        questionType = 49;
        isYesNoQuestion = questionType >= 50;
        askedQuestions.Add(questionType);
        PopulateButtons(GetRightAbcAnswer());
        return !isYesNoQuestion ? CreateAbcQuestion() : CreateYesNoQuestion();
    }

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
        return counter;

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
            if (shapeType == 4 &&
                figure.shape.Get_Colour().ToString() == ((ColoursInValidation) colourType).ToString()) counter++;
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
                if (shapeType == 4 &&
                    figure.shape.Get_Colour().ToString() == ((ColoursInValidation)colourType).ToString()) counter++;
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
}