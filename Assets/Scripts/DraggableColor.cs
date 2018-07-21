using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableColor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Transform _parentToReturn;
    private int _siblingIndexToReturn;
    GameObject placeholder = null;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        _siblingIndexToReturn = transform.GetSiblingIndex();
        _parentToReturn = transform.parent;
        GetComponent<LayoutElement>().ignoreLayout = true;
        transform.SetParent(gameObject.transform.parent.parent.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Color EndDrag");
        GetComponent<LayoutElement>().ignoreLayout = false;
        transform.SetParent(_parentToReturn);
        transform.SetSiblingIndex(_siblingIndexToReturn);
        Destroy(placeholder);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
