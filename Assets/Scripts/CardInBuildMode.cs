using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInBuildMode : MonoBehaviour, IDropHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " drooped on " + gameObject.name);
        eventData.pointerDrag.transform.SetParent(transform);
        var pointToSet =
            Helpers.LocateNearestPoint(eventData.pointerDrag.transform.localPosition, Helpers.Card12Points);
        Debug.Log("PointToSet = " + pointToSet.x + " " + pointToSet.y);
        eventData.pointerDrag.transform.localPosition = pointToSet;
    }
}
