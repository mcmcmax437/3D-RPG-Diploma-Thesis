using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerMovement.canMove = false;
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerMovement.canMove = true;
        
    }

}
