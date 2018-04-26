using UnityEngine;
using System.Collections;

public class game_test : MonoBehaviour {

    Difficulty_Modifiers mod;
    Card_Generator gen;
	void Start () {

        mod = gameObject.AddComponent<Difficulty_Modifiers>() as Difficulty_Modifiers;
        gen = gameObject.AddComponent<Card_Generator>() as Card_Generator;

        mod.Cart_type = Difficulty_Modifiers.Cart_Type.Cart_Type70;

        gen.Generate_Card(mod);
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
