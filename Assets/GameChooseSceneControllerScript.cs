using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChooseSceneControllerScript : MonoBehaviour
{
    public int RandomNumber { get; private set; }
    public int RandomSide { get; private set; }
    public CardToChooseScript[] CardToChooseScripts;

    // Use this for initialization
	void Start ()
	{
	    RandomNumber = Random.Range(0, 6);
	    RandomSide = Random.Range(0, 2);
	    Debug.Log("Random number: " + RandomNumber + " Random side: " + RandomSide);
	    int correctCardindex;
	    if (RandomSide == 0)
	    {
	        correctCardindex = RandomNumber;
	    }
	    else
	    {
	        correctCardindex = CardToChooseScripts.Length - RandomNumber - 1;
	    }
	    CardToChooseScripts[correctCardindex].CorrectCard = true;
	    Debug.Log("Correct card: " + correctCardindex);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
