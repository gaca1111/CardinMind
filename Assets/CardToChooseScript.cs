using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardToChooseScript : MonoBehaviour, IPointerClickHandler
{
    private Animator _animator;
    public Sprite WrongCardSprite;
    private Image _image;
    private Sprite _spriteToReturn;
    public bool CorrectCard { get; set; }

	// Use this for initialization
	void Start ()
	{
	    _animator = GetComponent<Animator>();
	    _image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    public void ChangeSprite()
    {
        _spriteToReturn = _image.sprite;
        if (!CorrectCard)
        {
            _image.sprite = WrongCardSprite;
        }
    }

    public void ReturnSprite()
    {
        _image.sprite = _spriteToReturn;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetTrigger("ReverseCard");
    }
}
