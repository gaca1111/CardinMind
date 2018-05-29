using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class CardDrawer : MonoBehaviour
{
    public GameObject card;
    private SpriteRenderer[] _spriteRenderers;

    private Difficulty_Modifiers.CardType _cardType;
    private int number_of_figures;
    private Shape.Figures_Colours[] figures_colours;
    private int number_of_figures_colours;
    private int number_of_mistakes;
    private Difficulty_Modifiers.Game_Mode game_mode;
    private bool colours_only_mechanic;
    private bool card_pick_mechanic;

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




        _difficultyModifiers.Set_Figures_Colours(colourses);

        _difficultyModifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
        _difficultyModifiers.Number_of_figures = 5;

        var cardPattern = _cardGenerator.Generate_Card(_difficultyModifiers);
        var shapes = new List<Shape>();
        foreach (var shape in cardPattern)
        {
            shapes.Add(shape);
        }


        card = GameObject.Find("CardToRemember");
        _spriteRenderers = card.GetComponentsInChildren<SpriteRenderer>();


    }
    

    // Update is called once per frame
	void Update () {
		
	}
}
