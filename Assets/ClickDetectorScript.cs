using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetectorScript : MonoBehaviour, IPointerClickHandler
{
    public SceneChangerScript SceneChanger;
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneChanger.CardRememberTimeOut();
    }
}
