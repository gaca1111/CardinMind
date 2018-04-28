using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

    public enum Rotation { Up, Down, Right, Left };

    protected bool is_rotative;
    protected Rotation rotation;

    public void Set_Rotation(Rotation incoming_rotation) {

        rotation = incoming_rotation;
    }

    public Rotation Get_Rotation() {

        return rotation;
    }

    public bool Get_Is_Rotative() {

        return is_rotative;
    }
}

public class Shape_1x1 : Shape {

}

public class Shape_2x1 : Shape {

}

public class Square : Shape_1x1 {

    public Square() {

        this.is_rotative = false;
    }
}

public class Triangle : Shape_1x1 {

    public Triangle() {

        this.is_rotative = true;
    }
}

public class Circle : Shape_1x1 {

    public Circle() {

        this.is_rotative = false;
    }
}

public class Rectangle : Shape_2x1 {

    public Rectangle() {

        this.is_rotative = false;
    }
}



