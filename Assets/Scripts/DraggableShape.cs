using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableShape : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IDropHandler
{
    private bool _setToCard = false;
    public bool Draggable = true;
    public GameObject DropZone;
    public int NumberOfPosition { get; set; } 

    // Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Draggable) return;
        Debug.Log("OnBeginDrag");
        if (!_setToCard)
        {
            var cloned = Instantiate(this, transform.parent);
            cloned.transform.SetSiblingIndex(transform.GetSiblingIndex());
            cloned.name = name;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<LayoutElement>().ignoreLayout = true;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Draggable) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Draggable) return;
        Debug.Log("OnEndDrag");
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == DropZone.transform)
        {
            Debug.Log("transform.parent == DropZone.transform");
            _setToCard = true;
            GetComponent<LayoutElement>().ignoreLayout = false;
            return;
        }
        Destroy(gameObject);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Draggable) return;
        Debug.Log("Clicked on " + this.name);
        transform.Rotate(0, 0, 90);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop in Shape");
        var color = eventData.pointerDrag.GetComponent<DraggableColor>();
        if (color == null) return;
        var x = eventData.pointerDrag.GetComponent<Image>();
        if (x == null) return;
        var image = GetComponent<Image>();
        if (image == null) return;
        image.color = x.color;
    }
}
