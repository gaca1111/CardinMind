using UnityEngine;
using System.Collections;

public class Card_Generator : MonoBehaviour {

    private Shape card_pattern;
    private Difficulty_Modifiers difficulty_modifires;

    public void Generate_Card(Difficulty_Modifiers incoming_difficulty_modifires) {

        difficulty_modifires = incoming_difficulty_modifires;

        Debug.Log(difficulty_modifires.Cart_type);

    }
}
