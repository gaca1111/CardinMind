using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Difficulty_Modifiers{

    public enum CardType {Cart_Type70, Cart_Type12};
    public enum Game_Mode {Both, Assembling, Questions, Random, Alternately};

    private CardType _cardType;
    private int number_of_figures;
    private Shape.Figures_Colours[] figures_colours;
    private int number_of_figures_colours;
    private int number_of_mistakes;
    private Game_Mode game_mode;
    private bool colours_only_mechanic;
    private bool card_pick_mechanic;
    private int timeRestriction;


    public CardType cardType {
        get {
            return _cardType;
        }

        set {
            _cardType = value;
        }
    }

    public int Number_of_figures {
        get {
            return number_of_figures;
        }

        set {
            number_of_figures = value;
        }
    }

    public int Number_of_mistakes {
        get {
            return number_of_mistakes;
        }

        set {
            number_of_mistakes = value;
        }
    }

    public Game_Mode Game_mode {
        get {
            return game_mode;
        }

        set {
            game_mode = value;
        }
    }

    public bool Colours_only_mechanic {
        get {
            return colours_only_mechanic;
        }

        set {
            colours_only_mechanic = value;
        }
    }

    public bool Card_pick_mechanic {
        get {
            return card_pick_mechanic;
        }

        set {
            card_pick_mechanic = value;
        }
    }

    public int TimeRestriction
    {
        get
        {
            return timeRestriction;
        }

        set
        {
            timeRestriction = value;
        }
    }

    public void Set_Figures_Colours( List <Shape.Figures_Colours> incoming_colours) {

        number_of_figures_colours = incoming_colours.Count;
        figures_colours = new Shape.Figures_Colours[number_of_figures_colours];

        int i = 0;
        foreach (Shape.Figures_Colours colour in incoming_colours) {

            figures_colours[i] = colour;
            i++;  
        }
    }

    public Shape.Figures_Colours[] Get_Figures_Colours() {

        return figures_colours;
    }
}
