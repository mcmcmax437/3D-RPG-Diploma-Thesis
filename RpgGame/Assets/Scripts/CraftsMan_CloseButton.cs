using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftsMan_CloseButton : MonoBehaviour
{
    public GameObject craftsMan_Canvas;

     public void Close()
    {
        craftsMan_Canvas.SetActive(false);
        PlayerMovement.canMove = true;
    }
}
