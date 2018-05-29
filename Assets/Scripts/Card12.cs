using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card12 : MonoBehaviour
{
    private SpriteRenderer[] _spriteRenderers;
    public Shape[,] CardPattern;

    public Card12(Shape[,] cardPattern)
    {
        CardPattern = cardPattern;
    }

	// Use this for initialization
	void Start ()
	{
	    _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
