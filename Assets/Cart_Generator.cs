using UnityEngine;
using System.Collections;
using System.Text;

public class Card_Generator : MonoBehaviour {

    public enum Shape_Sizes { Size1x1, Size2x1 };
    public enum Shapes_1x1 { Square, Triangle, Circle };
    public enum Shapes_2x1 { Rectangle };

    private Shape[,] card_pattern;
    private ArrayList empty_space;
    private int width_12 = 3;
    private int height_12 = 4;
    private int width_70 = 7;
    private int height_70 = 10;

    private Difficulty_Modifiers difficulty_modifires;

    private Shape_Sizes[] shape_sizes_array = new Shape_Sizes[2] { Shape_Sizes.Size1x1, Shape_Sizes.Size2x1 };
    private Shapes_1x1[] shapes_1x1_array = new Shapes_1x1[3] { Shapes_1x1.Square, Shapes_1x1.Triangle, Shapes_1x1.Circle };
    private Shapes_2x1[] shapes_2x1_array = new Shapes_2x1[1] { Shapes_2x1.Rectangle };
    private Shape.Rotation[] rotation_array = new Shape.Rotation[4] { Shape.Rotation.Up, Shape.Rotation.Down, Shape.Rotation.Right, Shape.Rotation.Left };

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

        int current_place;
        Shape_Sizes current_shape_size;

        for (int i = 0; i < difficulty_modifires.Number_of_figures; i++) {

            if (empty_space.Count == 0) {

                Debug.Log("Error not enought space on card");
                break;
            }

            current_shape_size = Roll_Shape_Sizes();

            switch (current_shape_size) {

                case Shape_Sizes.Size1x1:

                    current_place = Find_Empty_Space_12_1x1();
                    Debug.Log(current_place);
                    Roll_Shape_1x1();
                    break;

                case Shape_Sizes.Size2x1:

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

        return shape_sizes_array[Random.Range(0, shape_sizes_array.Length)];
    }

    private int Find_Empty_Space_12_1x1() {

        int place = Random.Range(0, empty_space.Count);
        empty_space.RemoveAt(place);  
        return place;
    }

    private Shape Roll_Shape_1x1() {

        Shapes_1x1 rolled_shape = shapes_1x1_array[Random.Range(0, shapes_1x1_array.Length)];
        Shape shape = gameObject.AddComponent<Shape>() as Shape;

        switch (rolled_shape) {

            case Shapes_1x1.Square:

                shape = gameObject.AddComponent<Square>() as Square;
                break;

            case Shapes_1x1.Circle:

                shape = gameObject.AddComponent<Circle>() as Circle;
                break;

            case Shapes_1x1.Triangle:

                shape = gameObject.AddComponent<Triangle>() as Triangle;
                break;

            default:

                Debug.Log("Error unknown shape");
                Debug.Log(rolled_shape);
                break;
        }

        if (shape.Get_Is_Rotative()) {

            shape.Set_Rotation(rotation_array[Random.Range(0, rotation_array.Length)]);
        }

        Debug.Log(shape);
        return shape;
    }
}
