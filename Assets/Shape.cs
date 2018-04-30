using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

    public enum Rotation { Up, Down, Right, Left };
    public enum Figures_Colours { Light_Blue, Dark_Blue, Light_Green, Dark_Green, Violet, Pink, Red, Yellow, Orange };

    protected bool is_rotative;
    protected Rotation rotation;
    protected bool is_drawable;
    protected Figures_Colours colour;

    public void Set_Rotation(Rotation incoming_rotation) {

        rotation = incoming_rotation;
    }

    public Rotation Get_Rotation() {

        return rotation;
    }

    public bool Get_Is_Rotative() {

        return is_rotative;
    }

    public void Set_Colour(Figures_Colours incoming_colour) {

        colour = incoming_colour;
    }

    public Figures_Colours Get_Colour() {

        return colour;
    }

}

public class Shape_1x1 : Shape {

}

public class Shape_2x1 : Shape {

}

public class Square : Shape_1x1 {

    public Square() {

        this.is_rotative = false;
        this.is_drawable = true;
    }
}

public class Triangle : Shape_1x1 {

    public Triangle() {

        this.is_rotative = true;
        this.is_drawable = true;
    }
}

public class Circle : Shape_1x1 {

    public Circle() {

        this.is_rotative = false;
        this.is_drawable = true;
    }
}

public class Empty : Shape_1x1 {

    public Empty() {

        this.is_rotative = false;
        this.is_drawable = false;
    }
}

public class Rectangle : Shape_2x1 {

    public Rectangle() {

        this.is_rotative = true;
        this.is_drawable = true;
    }
}



