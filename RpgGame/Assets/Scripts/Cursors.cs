using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cursors : MonoBehaviour
{

    public GameObject CursorObject;

    public Sprite CursorBasic;
    public Sprite CursorHand;
    public Image CursorImage;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        CursorObject.transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y-20, 0.0f);
        
        if(Input.GetMouseButton(1)) 
        {
            CursorImage.sprite = CursorHand;
        }
        if (Input.GetMouseButtonUp(1))
        {
            CursorImage.sprite = CursorBasic;
        }


    }
}
