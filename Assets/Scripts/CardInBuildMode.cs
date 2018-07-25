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
        if (eventData.pointerDrag.GetComponent<DraggableShape>() != null)
        {
            Debug.Log(eventData.pointerDrag.name + " drooped on " + gameObject.name);
            eventData.pointerDrag.transform.SetParent(transform);
            Vector2 pointToSet;
            int positionIndex;
            if (Static.DifficultyModifiers.cardType == Difficulty_Modifiers.CardType.Cart_Type12)
            {
                positionIndex =
                    Helpers.LocateNearestPoint(eventData.pointerDrag.transform.localPosition, Helpers.Card12Points);
                pointToSet = Helpers.Card12Points[positionIndex];
            }
            else
            {
                positionIndex =
                    Helpers.LocateNearestPoint(eventData.pointerDrag.transform.localPosition, Helpers.Card70Points);
                pointToSet = Helpers.Card70Points[positionIndex];

            }

            eventData.pointerDrag.GetComponent<DraggableShape>().NumberOfPosition = positionIndex;
            Debug.Log("PointToSet = " + pointToSet.x + " " + pointToSet.y);
            eventData.pointerDrag.transform.localPosition = pointToSet;
        }
    }
}
