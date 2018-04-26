using UnityEngine;
using System.Collections;

public class Difficulty_Modifiers : MonoBehaviour {

    public enum Cart_Type {Cart_Type70, Cart_Type12};

    private Cart_Type cart_type;

    public Cart_Type Cart_type
    {
        get
        {
            return cart_type;
        }

        set
        {
            cart_type = value;
        }
    }
}
