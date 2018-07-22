using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_test : MonoBehaviour {

    public Difficulty_Modifiers mod;
    public Card_Generator gen;
	void Start () {

        mod = gameObject.AddComponent<Difficulty_Modifiers>() as Difficulty_Modifiers;
        gen = gameObject.AddComponent<Card_Generator>() as Card_Generator;

        List <Shape.Figures_Colours> col = new List<Shape.Figures_Colours>();

        col.Add(Shape.Figures_Colours.Dark_Blue);
        col.Add(Shape.Figures_Colours.Red);
        col.Add(Shape.Figures_Colours.Orange);



        Difficulty_Modifiers.Set_Figures_Colours(col);

        Difficulty_Modifiers.cardType = Difficulty_Modifiers.CardType.Cart_Type12;
        Difficulty_Modifiers.Number_of_figures = 14;

        gen.Generate_Card(mod);
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
