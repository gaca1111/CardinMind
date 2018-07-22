using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using Object = System.Object;

public class CardDrawer : MonoBehaviour
{
    public GameObject card;
    private SpriteRenderer[] _spriteRenderers;

    public Sprite CircleSprite;
    public Sprite SquereSprite;
    public Sprite RectangleSprite;
    public Sprite TriangleSprite;

    private Difficulty_Modifiers.CardType _cardType;
    private int number_of_figures;
    private Shape.Figures_Colours[] figures_colours;
    private int number_of_figures_colours;
    private int number_of_mistakes;
    private Difficulty_Modifiers.Game_Mode game_mode;
    private bool colours_only_mechanic;
    private bool card_pick_mechanic;

    public static List<Shape_With_Place> FiguresList;


    Card_Generator _cardGenerator = new Card_Generator();
    Difficulty_Modifiers _difficultyModifiers;
    // Use this for initialization
    void Start () {
	    _cardGenerator = gameObject.AddComponent<Card_Generator>() as Card_Generator;
        _difficultyModifiers = gameObject.AddComponent<Difficulty_Modifiers>() as Difficulty_Modifiers;

        var colourses = new List<Shape.Figures_Colours>
        {
            Shape.Figures_Colours.Dark_Blue,
            Shape.Figures_Colours.Red,
            Shape.Figures_Colours.Orange
        };




        Difficulty_Modifiers.Set_Figures_Colours(colourses);

        Difficulty_Modifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
        Difficulty_Modifiers.Number_of_figures = 5;

        _cardGenerator.Generate_Card(_difficultyModifiers);
        var listOfShapes = _cardGenerator.Get_List_Of_Shape();
        FiguresList = listOfShapes;

        card = GameObject.Find("CardToRemember");
        _spriteRenderers = card.GetComponentsInChildren<SpriteRenderer>();

        foreach (var shapeWithPlace in listOfShapes)
        {
            int idPlace = shapeWithPlace.id_place+1;
            if (shapeWithPlace.shape is Rectangle)
            {
                _spriteRenderers[idPlace].sprite = RectangleSprite;
            }
            else if (shapeWithPlace.shape is Circle)
            {
                _spriteRenderers[idPlace].sprite = CircleSprite;
            }
            else if (shapeWithPlace.shape is Square)
            {
                _spriteRenderers[idPlace].sprite = SquereSprite;
            }
            else if (shapeWithPlace.shape is Triangle)
            {
                _spriteRenderers[idPlace].sprite = TriangleSprite;
            }

            if (shapeWithPlace.shape.Get_Is_Rotative())
            {
                switch (shapeWithPlace.shape.Get_Rotation())
                {
                    case Shape.Rotation.Up:
                        _spriteRenderers[idPlace].GetComponentInParent<Transform>().rotation = Quaternion.Euler(0,0,90);
                        break;
                    case Shape.Rotation.Down:
                        _spriteRenderers[idPlace].GetComponentInParent<Transform>().rotation = Quaternion.Euler(0,0,-90);
                        break;
                    case Shape.Rotation.Left:
                        _spriteRenderers[idPlace].GetComponentInParent<Transform>().Rotate(0, 0, 180);
                        break;
                    case Shape.Rotation.Right:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Color color;
            switch (shapeWithPlace.shape.Get_Colour())
            {
                case Shape.Figures_Colours.Light_Blue:
                    color = Color.blue;
                    break;
                case Shape.Figures_Colours.Dark_Blue:
                    color = new Color(0f, 0f, 0.625f);
                    break;
                case Shape.Figures_Colours.Light_Green:
                    color = Color.green;
                    break;
                case Shape.Figures_Colours.Dark_Green:
                    color = new Color(0.07f, 0.371f, 0.157f);
                    break;
                case Shape.Figures_Colours.Violet:
                    color = new Color(0.64f, 0.285f, 0.64f);
                    break;
                case Shape.Figures_Colours.Pink:
                    color = new Color(1f, 0.5f, 0.75f);
                    break;
                case Shape.Figures_Colours.Red:
                    color = Color.red;
                    break;
                case Shape.Figures_Colours.Yellow:
                    color = Color.yellow;
                    break;
                case Shape.Figures_Colours.Orange:
                    color = new Color(1f,0.788f,0.055f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

                    
            }
            _spriteRenderers[idPlace].color = color;
        }


    }
    

    // Update is called once per frame
	void Update () {
		
	}
}
