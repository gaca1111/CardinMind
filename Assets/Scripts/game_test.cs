﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_test : MonoBehaviour {

    public Difficulty_Modifiers mod;
    public Card_Generator gen;
	void Start () {

        mod = new Difficulty_Modifiers();
        gen = gameObject.AddComponent<Card_Generator>() as Card_Generator;

        List <Shape.Figures_Colours> col = new List<Shape.Figures_Colours>();

        col.Add(Shape.Figures_Colours.Dark_Blue);
        col.Add(Shape.Figures_Colours.Red);
        col.Add(Shape.Figures_Colours.Orange);



        mod.Set_Figures_Colours(col);

        mod.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
        mod.Number_of_figures = 14;

        gen.Generate_Card(mod);
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
