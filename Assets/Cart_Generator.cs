using UnityEngine;
using System.Collections;

public class Card_Generator : MonoBehaviour {

    public enum Shape_Sizes { size1x1, size2x1 };

    private Shape[,] card_pattern;
    private Difficulty_Modifiers difficulty_modifires;

    private Shape_Sizes[] shape_sizes_array = new Shape_Sizes[2] { Shape_Sizes.size1x1, Shape_Sizes.size2x1 };

    private Shape_Sizes current_shape_size;

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

        card_pattern = new Shape[3, 4];

        for (int i = 0; i < difficulty_modifires.Number_of_figures; i++) {

            current_shape_size = Roll_Shape_Sizes();

            switch (current_shape_size) {

                case Shape_Sizes.size1x1:

                    break;

                case Shape_Sizes.size2x1:

                    break;

                default:

                    Debug.Log("Error missing rolled shape size");
                    Debug.Log(current_shape_size);
                    break;
            }
        }
    }

    private void Generate_Card70() {

        card_pattern = new Shape[7, 10];

    }

    private Shape_Sizes Roll_Shape_Sizes() {

        return shape_sizes_array[Random.Range(0, 1)];
    }

    private void Find_Empty_Space_12_1x1() {

    }
}
