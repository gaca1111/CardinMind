using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

    protected bool is_rotative;
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



