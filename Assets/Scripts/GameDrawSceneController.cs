using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDrawSceneController : MonoBehaviour
{

    public SceneChangerScript SceneChanger;
    public GameObject Blob;
    public GameObject ColorArea;

    public GameObject CircleGameObject;
    public GameObject RectangleGameObject;
    public GameObject SquareGameObject;
    public GameObject TriangleGameObject;
    public Transform Card;
    public Transform ShapesPanel;
    public Transform CardPanel;

    public GameObject CorrectImage;
    public GameObject WrongImage;

    private int _mistakes = 0;

    private GameObject _displayImage = null;
    private float _timeToDisplay = 1;

    // Use this for initialization
    void Start()
    {
        var iterator = 0;
        foreach (var colour in Static.DifficultyModifiers.Get_Figures_Colours())
        {
            Debug.Log("Jestem w pętli pobierającej kolory iteracja numer: " + iterator++);
            var colorBlob = Instantiate(Blob);
            colorBlob.transform.SetParent(ColorArea.transform);
            colorBlob.GetComponent<Image>().color = Helpers.ShapeColorToUnityColor(colour);
        }

        if (Static.DifficultyModifiers.Colours_only_mechanic)
        {
            DrawCard();
            Debug.Log("Powinienem rysować i zniszczyć panele kształtów");
            Destroy(ShapesPanel.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_displayImage != null)
        {
            _timeToDisplay -= Time.deltaTime;
            if (_timeToDisplay <= 0)
            {
                _displayImage.GetComponent<Image>().enabled = false;
                _timeToDisplay = 1;
            }
        }
    }

    public void OnCheckButton()
    {
        var result = ValidateCard();
        if (!result)
        {
            _displayImage = WrongImage;
            _displayImage.GetComponent<Image>().enabled = true;
            _mistakes++;
        }
        else
        {
            _displayImage = CorrectImage;
            _displayImage.GetComponent<Image>().enabled = true;
            SceneChanger.EndGame();
        }

        if (_mistakes > Static.DifficultyModifiers.Number_of_mistakes)
        {
            SceneChanger.EndGame();
        }
    }

    public bool ValidateCard()
    {
        var counter = 0;
        foreach (var shape in Static.ShapeWithPlaces)
        {
            if (!IsOnCard(shape))
            {
                return false;
            }

            counter++;
        }

        return counter == Card.childCount;
    }

    public bool IsOnCard(Shape_With_Place shape)
    {
        for (int i = 0; i < Card.childCount; i++)
        {
            bool rectangle = false;
            bool position = false;
            bool rotation = false;
            bool color = false;
            int shapePositionRotated = 0;
            var child = Card.GetChild(i);
            var shapeOnCard = child.gameObject.GetComponent<DraggableShape>();
            if (shapeOnCard == null) continue;

            if (!Static.DifficultyModifiers.Colours_only_mechanic)
                if (Static.DifficultyModifiers.cardType == Difficulty_Modifiers.CardType.Cart_Type70)
                {
                    int computedPosition = shapeOnCard.NumberOfPosition + 10 + ((shapeOnCard.NumberOfPosition / 7) * 2);
                    if (shape.shape is Rectangle)
                    {
                        rectangle = true;
                        var rotationOfShape = shape.shape.Get_Rotation();
                        switch (rotationOfShape)
                        {
                            case Shape.Rotation.Up:

                                shapePositionRotated = shape.id_place >=21 ? shape.id_place - 21 : -1;
                                break;
                            case Shape.Rotation.Left:
                                shapePositionRotated = shape.id_place % 7 >= 3 ? shape.id_place - 3 : -1;
                                break;
                            case Shape.Rotation.Down:
                                shapePositionRotated = shape.id_place <= 48 ? shape.id_place + 27 : -1;
                                break;
                            case Shape.Rotation.Right:
                                shapePositionRotated = shape.id_place % 7 >= 3 ? shape.id_place + 3 : -1;
                                break;
                        }

                        if (computedPosition == shapePositionRotated)
                            position = true;
                    }
                    if (shape.id_place == computedPosition)
                    {
                        position = true;
                    }
                }
                else
                {
                    if (shape.shape is Rectangle)
                    {
                        rectangle = true;
                        var rotationOfShape = shape.shape.Get_Rotation();
                        switch (rotationOfShape)
                        {
                            case Shape.Rotation.Up:
                                shapePositionRotated = shape.id_place - 3;
                                break;
                            case Shape.Rotation.Left:
                                shapePositionRotated = shape.id_place - 1;
                                break;
                            case Shape.Rotation.Down:
                                shapePositionRotated = shape.id_place + 3;
                                break;
                            case Shape.Rotation.Right:
                                shapePositionRotated = shape.id_place + 1;
                                break;
                        }

                        if (shapeOnCard.NumberOfPosition == shapePositionRotated)
                            position = true;
                    }

                    if (shape.id_place == shapeOnCard.NumberOfPosition)
                    {
                        position = true;
                    }
                }
            else position = true;
            if (shape.shape.Get_Is_Rotative()) rotation = IsGoodRotation(shape, position && rectangle, shapeOnCard);
            else rotation = true;
            color = IsGoodColor(shape, child);
            if (position && rotation && color)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsGoodRotation(Shape_With_Place shape, bool positionAndRectangle, DraggableShape shapeOnCard)
    {
        var figureRotation = 0;
        var rotLeft = new Quaternion(0, 0, (float) 0.7, (float) 0.7).eulerAngles.magnitude;
        var rotDown = new Quaternion(0, 0, 1, 0).eulerAngles.magnitude;
        var rotRight = new Quaternion(0, 0, (float) 0.7, (float) -0.7).eulerAngles.magnitude;
        if (shapeOnCard.transform.rotation.eulerAngles.magnitude.Equals(rotLeft)) figureRotation = 1;
        if (shapeOnCard.transform.rotation.eulerAngles.magnitude.Equals(rotDown)) figureRotation = 2;
        if (shapeOnCard.transform.rotation.eulerAngles.magnitude.Equals(rotRight)) figureRotation = 3;
        if (!shape.shape.Get_Is_Rotative())
        {
            return true;
        }
        if (figureRotation == 0 && shape.shape.Get_Rotation() == Shape.Rotation.Up)
        {
            return true;
        }
        if (figureRotation == 1 && shape.shape.Get_Rotation() == Shape.Rotation.Left)
        {
            return true;
        }
        if (figureRotation == 2 && shape.shape.Get_Rotation() == Shape.Rotation.Down)
        {
            return true;
        }
        if (figureRotation == 3 && shape.shape.Get_Rotation() == Shape.Rotation.Right)
        {
            return true;
        }
        if (positionAndRectangle)
        {
            if (figureRotation == 2 && shape.shape.Get_Rotation() == Shape.Rotation.Up)
            {
                return true;
            }
            if (figureRotation == 3 && shape.shape.Get_Rotation() == Shape.Rotation.Left)
            {
                return true;
            }
            if (figureRotation == 0 && shape.shape.Get_Rotation() == Shape.Rotation.Down)
            {
                return true;
            }
            if (figureRotation == 1 && shape.shape.Get_Rotation() == Shape.Rotation.Right)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsGoodColor(Shape_With_Place shape, Component shapeOncard)
    {
        var shapeOnCardColor = shapeOncard.gameObject.GetComponent<Image>().color;
        return shapeOnCardColor == Helpers.ShapeColorToUnityColor(shape.shape.Get_Colour());
    }

    void DrawCard()
    {
        var shapes = Static.ShapeWithPlaces;
        Debug.Log(Static.ShapeWithPlaces);
        Debug.Break();
        foreach (var shape in shapes)
        {
            Debug.Log("Shape: " + shape.shape + " rotation: " + shape.shape.Get_Rotation());
            CloneObjectToCard(shape.shape, shape.id_place);
        }
    }

    void CloneObjectToCard(Shape shape, int position)
    {
        Vector2 shapePosition;
        GameObject objectToSet = null;
        int positionToSet;
        switch (Static.DifficultyModifiers.cardType)
        {
            case Difficulty_Modifiers.CardType.Cart_Type12:
                positionToSet = position;
                shapePosition = Helpers.Card12Points[positionToSet];
                Debug.Log("Card12 position: " + positionToSet + " x: " + shapePosition.x + " y: " + shapePosition.y);
                break;
            case Difficulty_Modifiers.CardType.Cart_Type70:
                positionToSet = position - 10 - (((position / 9) - 1) * 2);
                shapePosition = Helpers.Card70Points[positionToSet];
                Debug.Log("Card70 position from generator: " + position + "position on card:" + positionToSet + " x: " +
                          shapePosition.x + " y: " + shapePosition.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (shape is Rectangle)
        {
            objectToSet = RectangleGameObject;
            Debug.Log("shape is Rectangle");
        }
        else if (shape is Circle)
        {
            objectToSet = CircleGameObject;
            Debug.Log("shape is Circle");
        }
        else if (shape is Square)
        {
            objectToSet = SquareGameObject;
            Debug.Log("shape is Square");
        }
        else if (shape is Triangle)
        {
            objectToSet = TriangleGameObject;
            Debug.Log("shape is Triangle");
        }

        var newObject = Instantiate(objectToSet);

        newObject.transform.parent = Card;
        newObject.transform.localPosition = shapePosition;
        newObject.transform.localScale = Vector3.one;
        newObject.GetComponent<DraggableShape>().Draggable = false;
        if (shape.Get_Is_Rotative())
        {
            Debug.Log("Is rotative");
            var rotation = shape.Get_Rotation();
            switch (rotation)
            {
                case Shape.Rotation.Up:
                    Debug.Log("Rotate up");
                    break;
                case Shape.Rotation.Down:
                    newObject.transform.Rotate(Vector3.forward, 180);
                    Debug.Log("Rotate down");
                    break;
                case Shape.Rotation.Right:
                    newObject.transform.Rotate(Vector3.forward, 270);
                    Debug.Log("Rotate uright");
                    break;
                case Shape.Rotation.Left:
                    newObject.transform.Rotate(Vector3.forward, 90);
                    Debug.Log("Rotate left");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
