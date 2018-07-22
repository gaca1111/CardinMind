using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var image = GetComponent<Image>();
        Debug.Log(name + " r: "+image.color.r + " g: " + image.color.g + " b: " + image.color.b);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
