using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Difficulty_Modifiers : MonoBehaviour {

    public enum Cart_Type {Cart_Type70, Cart_Type12};
    public enum Figures_Colours {Light_Blue, Dark_Blue, Light_Green, Dark_Green, Violet, Pink, Red, Yellow, Orange};

    private Cart_Type cart_type;
    private int number_of_figures;
    private Figures_Colours[] figures_colours;
    private int number_of_figures_colours;









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

    public int Number_of_figures
    {
        get
        {
            return number_of_figures;
        }

        set
        {
            number_of_figures = value;
        }
    }

    public void Set_Figures_Colours( List <Figures_Colours> incoming_colours) {

        number_of_figures_colours = incoming_colours.Count;
        figures_colours = new Figures_Colours[number_of_figures_colours];

        int i = 0;
        foreach (Figures_Colours colour in incoming_colours) {

            figures_colours[i] = colour;
            i++;    
        }
    }
}
