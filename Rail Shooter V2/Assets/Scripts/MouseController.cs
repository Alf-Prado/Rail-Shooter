using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float depth = 5.0f;
     
    void Update ()
     
    {
     
         var mousePos = Input.mousePosition;
     
         var wantedPos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, depth));
     
         transform.position = wantedPos;
    }

}
