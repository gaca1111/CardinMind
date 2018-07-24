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
    public GameObject ShapesPanel;

    // Use this for initialization
    void Start()
    {
        foreach (var colour in Static.DifficultyModifiers.Get_Figures_Colours())
        {
            var colorBlob = Instantiate(Blob);
            colorBlob.transform.SetParent(ColorArea.transform);
            colorBlob.GetComponent<Image>().color = Helpers.ShapeColorToUnityColor(colour);
        }

        if (Static.DifficultyModifiers.Colours_only_mechanic)
        {
            DrawCard();
            Destroy(ShapesPanel);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DrawCard()
    {
        var shapes = Static.ShapeWithPlaces;
        foreach (var shape in shapes)
        {
            Debug.Log("Shape: " + shape.shape.name + " rotation: " + shape.shape.Get_Rotation());
            CloneObjectToCard(shape.shape, shape.id_place);
        }
    }

    void CloneObjectToCard(Shape shape, int position)
    {
        Vector2 shapePosition;
        GameObject objectToSet = null;
        switch (Static.DifficultyModifiers.cardType)
        {
            case Difficulty_Modifiers.CardType.Cart_Type12:
                shapePosition = Helpers.Card12Points[position];
                Debug.Log("Card12 position: " + position + " x: " + shapePosition.x + " y: " + shapePosition.y);
                Debug.Log(shapePosition);
                break;
            case Difficulty_Modifiers.CardType.Cart_Type70:
                int positionToSet = position - 10 - (((position / 9) - 1) * 2);
                Debug.Log("Position to set in 70card: " + positionToSet);
                shapePosition = Helpers.Card70Points[positionToSet];
                Debug.Log("Card70 position: " + position + " x: " + shapePosition.x + " y: " + shapePosition.y);
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
