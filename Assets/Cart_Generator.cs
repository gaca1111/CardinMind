using UnityEngine;
using System.Collections;

public class Card_Generator : MonoBehaviour {

    private Shape card_pattern;
    private Difficulty_Modifiers difficulty_modifires;

    public void Generate_Card(Difficulty_Modifiers incoming_difficulty_modifires) {

        difficulty_modifires = incoming_difficulty_modifires;

        switch (difficulty_modifires.Cart_type) {

            case Difficulty_Modifiers.Cart_Type.Cart_Type12:

                Generate_Card12();
                break;

            case Difficulty_Modifiers.Cart_Type.Cart_Type70:

                Generate_Card70();
                break;

            default:

                Debug.Log("Error missing card type ");
                Debug.Log(difficulty_modifires.Cart_type);
                break;
        }
    }

    private void Generate_Card12() {

    }

    private void Generate_Card70() {

    }
}
