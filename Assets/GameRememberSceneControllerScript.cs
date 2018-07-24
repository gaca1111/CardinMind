﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRememberSceneControllerScript : MonoBehaviour
{
    private Card_Generator _cardGenerator;
    public GameObject CircleGameObject;
    public GameObject RectangleGameObject;
    public GameObject SquareGameObject;
    public GameObject TriangleGameObject;
    public Transform Card;
    public float TimeOnRemember = 10;
    public Text TimerText;

	// Use this for initialization
	void Start ()
	{
	    Static.DifficultyModifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type70;
	    Static.DifficultyModifiers.Number_of_figures = 5;
	    Static.DifficultyModifiers.Set_Figures_Colours(new List<Shape.Figures_Colours>()
	    {
	        Shape.Figures_Colours.Orange,
	        Shape.Figures_Colours.Dark_Blue,
	        Shape.Figures_Colours.Pink,
	        Shape.Figures_Colours.Dark_Green,
	        Shape.Figures_Colours.Light_Blue,
	        Shape.Figures_Colours.Red,
	        Shape.Figures_Colours.Light_Green,
	        Shape.Figures_Colours.Violet,
	        Shape.Figures_Colours.Yellow

        });

	    _cardGenerator = gameObject.AddComponent<Card_Generator>();
        _cardGenerator.Generate_Card(Static.DifficultyModifiers);
        Debug.Log(Static.DifficultyModifiers.cardType);
	    Static.ShapeWithPlaces = _cardGenerator.Get_List_Of_Shape();
        DrawCard();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    TimeOnRemember -= Time.deltaTime;
	    TimerText.text = TimeOnRemember.ToString("0.00");
	    if (TimeOnRemember <= 0)
	    {
            Debug.Log("TimerEnd");
	        SceneManager.LoadScene("GameDrawScene");
        }
	}

    void DrawCard()
    {
        var shapes = Static.ShapeWithPlaces;
        Debug.Log("Przed pętlą");
        foreach (var shape in shapes)
        {
            Debug.Log("Jestem w pętli");
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
                int positionToSet = position - 10 - (((position/9)-1)*2);
                Debug.Log("Position to set in 70card: "+ positionToSet);
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

        if (newObject != null)
        {
            var image = newObject.GetComponent<Image>();
            if (image != null)
            {
                Debug.Log("Color: "+shape.Get_Colour());
                image.color = Helpers.ShapeColorToUnityColor(shape.Get_Colour());
            }
        }
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

        Debug.Log("Powinno być");
    }
}
