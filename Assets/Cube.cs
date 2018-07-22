using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    private int _randomNumber;
    private int _randomSide;
    private Image _image;
    public Sprite ArrowSprite;
    public Sprite[] Sprites;
    private Animator _animator;
    private bool toRandom = true;
    public GameChooseSceneControllerScript GameChooseSceneControllerScript;
    public Vector2 ArrowLeftPosition;
    public Vector2 ArrowRightPosition;

	// Use this for initialization
	void Start ()
	{
	    _randomNumber = GameChooseSceneControllerScript.RandomNumber;
	    _randomSide = GameChooseSceneControllerScript.RandomSide;
	    _image = GetComponent<Image>();
	    _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    void SetSpriteByRandom()
    {
        Debug.Log("Setting sprite y random number: " + GameChooseSceneControllerScript.RandomNumber);
        Destroy(_animator);
        _image.sprite = Sprites[GameChooseSceneControllerScript.RandomNumber];
        CreateArrow(GameChooseSceneControllerScript.RandomSide);
    }

    void CreateArrow(int side)
    {
        GameObject arrowGameObject = new GameObject("Arrow");
        arrowGameObject.transform.SetParent(transform.parent);
        Image arrowImage = arrowGameObject.AddComponent<Image>();
        arrowImage.sprite = ArrowSprite;
        arrowImage.rectTransform.sizeDelta = new Vector2(240, 180);
        if (side == 0)
        {
            arrowImage.transform.localPosition = ArrowLeftPosition;

        }
        else
        {
            arrowImage.transform.localPosition = ArrowRightPosition;
            arrowImage.transform.Rotate(0, 0, 180);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!toRandom) return;
        _animator.SetTrigger("drawNumber");
        toRandom = false;
    }
}
