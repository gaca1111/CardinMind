using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameRememberSceneControllerScript : MonoBehaviour
{
    private Card_Generator _cardGenerator;
    public GameObject CircleGameObject;
    public GameObject RectangleGameObject;
    public GameObject SquareGameObject;
    public GameObject TriangleGameObject;
    public Transform Card;

	// Use this for initialization
	void Start ()
	{
	    Static.DifficultyModifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
	    Static.DifficultyModifiers.Number_of_figures = 10;
	    Static.DifficultyModifiers.Set_Figures_Colours(new List<Shape.Figures_Colours>() { Shape.Figures_Colours.Orange, Shape.Figures_Colours.Dark_Blue,});

		_cardGenerator = new Card_Generator();
        _cardGenerator.Generate_Card(Static.DifficultyModifiers);
        Debug.Log(Static.DifficultyModifiers.cardType);
	    Static.ShapeWithPlaces = _cardGenerator.Get_List_Of_Shape();
        DrawCard();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    void DrawCard()
    {
        var shapes = Static.ShapeWithPlaces;
        Debug.Log("Przed pętlą");
        foreach (var shape in shapes)
        {
            Debug.Log("Jestem w pętli");
            CloneObjectToCard(shape.shape, shape.id_place);
        }
    }

    void CloneObjectToCard(Shape shape, int position)
    {
        Vector2 shapePosition;
        GameObject objectToSet = new GameObject("Empty-Error");
        switch (Static.DifficultyModifiers.cardType)
        {
            case Difficulty_Modifiers.CardType.Cart_Type12:
                shapePosition = Helpers.Card12Points[position];
                Debug.Log("Card12 position: " + position + " x: " + shapePosition.x + " y: " + shapePosition.y);
                break;
            case Difficulty_Modifiers.CardType.Cart_Type70:
                shapePosition = Helpers.Card70Points[position];
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
                    objectToSet.transform.Rotate(0, 0, 180);
                    Debug.Log("Rotate down");
                    break;
                case Shape.Rotation.Right:
                    objectToSet.transform.Rotate(0, 0, 270);
                    Debug.Log("Rotate uright");
                    break;
                case Shape.Rotation.Left:
                    objectToSet.transform.Rotate(0, 0, 90);
                    Debug.Log("Rotate left");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var image = objectToSet.GetComponent<Image>();
        if (image != null)
        {
            Debug.Log("Color: "+shape.Get_Colour());
            image.color = Helpers.ShapeColorToUnityColor(shape.Get_Colour());
        }

        objectToSet.transform.parent = Card;
        objectToSet.transform.localPosition = shapePosition;

        Instantiate(objectToSet);
        Debug.Log("Powinno być");
    }
}
