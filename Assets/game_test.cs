using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_test : MonoBehaviour {

    Difficulty_Modifiers mod;
    Card_Generator gen;
	void Start () {

        mod = gameObject.AddComponent<Difficulty_Modifiers>() as Difficulty_Modifiers;
        gen = gameObject.AddComponent<Card_Generator>() as Card_Generator;

        List <Shape.Figures_Colours> col = new List<Shape.Figures_Colours>();

        col.Add(Shape.Figures_Colours.Dark_Blue);
        col.Add(Shape.Figures_Colours.Red);
        col.Add(Shape.Figures_Colours.Orange);



        mod.Set_Figures_Colours(col);

        mod.Cart_type = Difficulty_Modifiers.Cart_Type.Cart_Type12;
        mod.Number_of_figures = 10;

        gen.Generate_Card(mod);
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
