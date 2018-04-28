using UnityEngine;
using System.Collections;
using System.Text;

public class Card_Generator : MonoBehaviour {

    public enum Shape_Sizes { size1x1, size2x1 };

    private Shape[,] card_pattern;
    private ArrayList empty_space;
    private int width_12 = 3;
    private int height_12 = 4;
    private int width_70 = 7;
    private int height_70 = 10;





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

        card_pattern = new Shape[width_12, height_12];
        empty_space = new ArrayList();
        Setup_Empty_Space(width_12, height_12);

        for (int i = 0; i < difficulty_modifires.Number_of_figures; i++) {

            if (empty_space.Count == 0) {

                Debug.Log("Error not enought space on card");
                break;
            }

            current_shape_size = Roll_Shape_Sizes();

            switch (current_shape_size) {

                case Shape_Sizes.size1x1:

                    Debug.Log(Find_Empty_Space_12_1x1());
                    break;

                case Shape_Sizes.size2x1:

                    Debug.Log("2x1");
                    break;

                default:

                    Debug.Log("Error missing rolled shape size");
                    Debug.Log(current_shape_size);
                    break;
            }
        }
    }

    private void Generate_Card70() {

        card_pattern = new Shape[width_70, height_70];

    }


    private void Setup_Empty_Space(int width, int height) {

        int counter = 1;
        for (int i = 0; i < width*height; i++) {

            empty_space.Add(counter);
            counter++;
        }
    }

    private Shape_Sizes Roll_Shape_Sizes() {

        return shape_sizes_array[Random.Range(0, 2)];
    }

    private int Find_Empty_Space_12_1x1() {

        int place = Random.Range(0, empty_space.Count);
        empty_space.RemoveAt(place);  
        return place;
    }
}
