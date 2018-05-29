using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private enum CardType { CardType70, CardType12 };

    private enum GameMode { Both, Assembling, Questions, Random, Alternately };

    private CardType _cardType;
    private int _numberOfFigures;
    private int _numberOfFiguresColorous;
    private int _numberOfMistakes;
    private GameMode _gameMode;
    private bool _coloursOnlyMechanic;
    private bool _cardPickMechanic;



    // Use this for initialization
    void Start () {
		SetInitialValue();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void SetInitialValue()
    {
        _numberOfFigures = PlayerPrefs.GetInt("NumberOfFigures");
        _cardType = PlayerPrefs.GetString("CardType") == Difficulty_Modifiers.CardType.Cart_Type70.ToString() ? (CardType) CardType.CardType70 : CardType.CardType12;
        _numberOfMistakes = PlayerPrefs.GetInt("NumberOfMistakes");
        _coloursOnlyMechanic = Convert.ToBoolean(PlayerPrefs.GetInt("ColoursOnlyMechanic"));
        _gameMode = (GameMode) PlayerPrefs.GetInt("GameMode");
        _numberOfFiguresColorous = PlayerPrefs.GetInt("NumberOfFiguresColorous");
        _cardPickMechanic = Convert.ToBoolean(PlayerPrefs.GetInt("CardPickMechanic"));
    }
}
