using UnityEngine;
using System.Collections;
using System.Text;

public struct Roll_Struct2x1 {

    public bool is_2x1;
    public Shape.Rotation rotation;
    public int place;
}

public class Card_Generator : MonoBehaviour {

    public enum Shape_Sizes { Size1x1, Size2x1 };
    public enum Shapes_1x1 { Square, Triangle, Circle };
    public enum Shapes_2x1 { Rectangle };

    private Difficulty_Modifiers difficulty_modifires;

    private Shape[,] card_pattern;
    private ArrayList empty_space;
    private int width_12 = 3;
    private int height_12 = 4;
    private int width_70 = 7;
    private int height_70 = 10;

    private Shape_Sizes[] shape_sizes_array = new Shape_Sizes[2] { Shape_Sizes.Size1x1, Shape_Sizes.Size2x1 };
    private Shapes_1x1[] shapes_1x1_array = new Shapes_1x1[3] { Shapes_1x1.Square, Shapes_1x1.Triangle, Shapes_1x1.Circle };
    private Shapes_2x1[] shapes_2x1_array = new Shapes_2x1[1] { Shapes_2x1.Rectangle };
    private Shape.Rotation[] rotation_array = new Shape.Rotation[4] { Shape.Rotation.Up, Shape.Rotation.Down, Shape.Rotation.Right, Shape.Rotation.Left };
    private Shape.Figures_Colours[] colours_array;
    private ArrayList current_rotation_array;
    private ArrayList current_removed_places_array;
    private bool current_can_roll_2x1 = true;

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
        Setup_Generation(width_12, height_12);

        int current_place;
        Shape_Sizes current_shape_size;
        Shape current_shape;
       
        for (int i = 0; i < difficulty_modifires.Number_of_figures; i++) {

            if (empty_space.Count == 0) {

                Debug.Log("Error not enought space on card");
                break;
            }

            if (current_can_roll_2x1) {

                current_shape_size = Roll_Shape_Sizes();
            }
            else {

                current_shape_size = Shape_Sizes.Size1x1;
            }
            
            switch (current_shape_size) {

                case Shape_Sizes.Size1x1:

                    current_place = Find_Empty_Space_12_1x1();               
                    current_shape = Roll_Shape_1x1();
                    current_shape.Set_Colour(Roll_Colour());
                    Save_Shape(current_place, current_shape, width_12, height_12);
                    Debug.Log("place " + current_place + " shape " + current_shape + "colour" + current_shape.Get_Colour() + "rotation " + current_shape.Get_Rotation());
                    break;

                case Shape_Sizes.Size2x1:

                    Roll_Struct2x1 roled_struct = Find_Empty_Space_12_2x1(width_12, height_12);

                    if (roled_struct.is_2x1) {

                        current_place = roled_struct.place;
                        current_shape = Roll_Shape_2x1(roled_struct.rotation);
                        current_shape.Set_Colour(Roll_Colour());
                        Save_Shape(current_place, current_shape, width_12, height_12);
                        Debug.Log("place " + current_place + " shape " + current_shape + "colour" + current_shape.Get_Colour() + "rotation " + current_shape.Get_Rotation());
                    }
                    else {

                        current_place = Find_Empty_Space_12_1x1();
                        current_shape = Roll_Shape_1x1();
                        current_shape.Set_Colour(Roll_Colour());
                        Save_Shape(current_place, current_shape, width_12, height_12);
                        Debug.Log("place " + current_place + " shape " + current_shape + "colour" + current_shape.Get_Colour() + "rotation " + current_shape.Get_Rotation()); 
                    }

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

    private void Setup_Generation(int width, int height) {

        for (int i = 0; i < width*height; i++) {

            empty_space.Add(i);
        }

        colours_array = difficulty_modifires.Get_Figures_Colours();
    }

    private Shape_Sizes Roll_Shape_Sizes() {

        return shape_sizes_array[Random.Range(0, shape_sizes_array.Length)];
    }

    private int Find_Empty_Space_12_1x1() {

        int rnd = Random.Range(0, empty_space.Count);
        int place = (int)empty_space[rnd];
        empty_space.RemoveAt(rnd);
        return place;
    }

    private Roll_Struct2x1 Find_Empty_Space_12_2x1(int width, int height) {

        Roll_Struct2x1 rolled_data = new Roll_Struct2x1();
        current_removed_places_array = new ArrayList();

        while (true) {

            current_rotation_array = new ArrayList();

            int rnd = Random.Range(0, empty_space.Count);
            int place = (int)empty_space[rnd];
            int counter = place;

            int x = 0;
            int y = 0;

            for (int i = 0; i < height; i++) {

                for (int j = 0; j < width; j++) {

                    if (counter == 0) {

                        x = j;
                        y = i;
                    }

                    counter--;
                }
            }

            if (Check_Left(x, place)) {

                current_rotation_array.Add(Shape.Rotation.Left);
            }
            if (Check_Up(difficulty_modifires.Cart_type, y, place)) {

                current_rotation_array.Add(Shape.Rotation.Up);
            }
            if (Check_Right(x, width_12, place)) {

                current_rotation_array.Add(Shape.Rotation.Right);
            }
            if (Check_Down(difficulty_modifires.Cart_type, y, height_12, place)) {

                current_rotation_array.Add(Shape.Rotation.Down);
            }

            if (current_rotation_array.Count == 0) {

                current_removed_places_array.Add(place);
                empty_space.RemoveAt(rnd);
            }
            else {

                rolled_data.rotation = (Shape.Rotation)current_rotation_array[Random.Range(0, current_rotation_array.Count)];
                rolled_data.place = place;
                rolled_data.is_2x1 = true;

                empty_space.RemoveAt(rnd);

                foreach (int removed_place in current_removed_places_array) {

                    empty_space.Add(removed_place); 
                }

                switch (rolled_data.rotation) {

                    case Shape.Rotation.Up:

                        card_pattern[x, y - 1] = gameObject.AddComponent<Empty>() as Empty;
                        empty_space.Remove(place - 3);
                        break;

                    case Shape.Rotation.Down:

                        card_pattern[x, y + 1] = gameObject.AddComponent<Empty>() as Empty;
                        empty_space.Remove(place + 3);
                        break;

                    case Shape.Rotation.Right:

                        card_pattern[x + 1, y] = gameObject.AddComponent<Empty>() as Empty;
                        empty_space.Remove(place + 1);
                        break;

                    case Shape.Rotation.Left:

                        card_pattern[x - 1, y] = gameObject.AddComponent<Empty>() as Empty;
                        empty_space.Remove(place - 1);
                        break;

                    default:

                        Debug.Log("Error missing rolled rotation");
                        Debug.Log(rolled_data.rotation);
                        break;
                }

                return rolled_data;
            }

            if (empty_space.Count == 0) {

                foreach (int removed_place in current_removed_places_array) {

                    empty_space.Add(removed_place);    
                }

                current_can_roll_2x1 = false;
                rolled_data.is_2x1 = false;
                return rolled_data;
            }
        }
    }

    private bool Check_Left(int x, int place) {

        if (x - 1 < 0) {

            return false;
        }

        return Check_Empty_Array_Contains(place - 1);
    }

    private bool Check_Right(int x, int width, int place) {

        if (x + 1 >= width) {

            return false;
        }

        return Check_Empty_Array_Contains(place + 1);
    }

    private bool Check_Up(Difficulty_Modifiers.Cart_Type type, int y, int place) {

        if (y - 1 < 0) {

            return false;
        }

        if (type == Difficulty_Modifiers.Cart_Type.Cart_Type12) {

            return Check_Empty_Array_Contains(place - 3);
        }
        else {

            return Check_Empty_Array_Contains(place - 7);
        }
    }

    private bool Check_Down(Difficulty_Modifiers.Cart_Type type, int y, int height, int place) {

        if (y + 1 >= height) {

            return false;
        }

        if (type == Difficulty_Modifiers.Cart_Type.Cart_Type12) {

            return Check_Empty_Array_Contains(place + 3);
        }
        else {

            return Check_Empty_Array_Contains(place + 7);
        }
    }

    private bool Check_Empty_Array_Contains(int check) {

        if (empty_space.Contains(check)) {

            return true;
        }
        else {

            return false;
        }
    }

    private Shape Roll_Shape_2x1(Shape.Rotation rotation) {

        Shapes_2x1 rolled_shape = shapes_2x1_array[Random.Range(0, shapes_2x1_array.Length)];
        Shape shape = gameObject.AddComponent<Shape>() as Shape;

        switch (rolled_shape) {

            case Shapes_2x1.Rectangle:

                shape = gameObject.AddComponent<Rectangle>() as Rectangle;
                break;

            default:

                Debug.Log("Error unknown shape");
                Debug.Log(rolled_shape);
                break;
        }

        if (shape.Get_Is_Rotative()) {

            shape.Set_Rotation(rotation);
        }

        return shape;
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

        return shape;
    }

    private Shape.Figures_Colours Roll_Colour() {

        return colours_array[Random.Range(0, colours_array.Length)];
    }

    private void Save_Shape(int place, Shape shape, int width, int height) {
         
        for (int i = 0; i < height; i++) {

            for (int j = 0; j < width; j++) {

                if (place == 0) {

                    card_pattern[j, i] = shape;                   
                    return;
                }

                place--;
            }
        }   
    }
}
