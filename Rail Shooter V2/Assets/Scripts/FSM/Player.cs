using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    FSM fsm;
    // Start is called before the first frame update
    void Start()
    {
        fsm = GameObject.Find("Plane").GetComponent<FSM>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 my3DPos = transform.position;
        Vector2 myTexturePos = new Vector2( (my3DPos.x + 25.0f)*512.0f / 50.0f, (my3DPos.z + 25.0f) * 512.0f / 50.0f);
        //Debug.Log(myTexturePos);
        for (int y = 0; y < fsm.texture.height; y++)
        {
            for (int x = 0; x < fsm.texture.width; x++)
            {
                Vector2 vTemp = new Vector2(myTexturePos.x - x, myTexturePos.y - y);
                vTemp.Normalize();
                Color color = Color.black;
                color.r = vTemp.x;
                color.g = 0.0f;
                color.b = vTemp.y;
                fsm.texture.SetPixel(x, y, color);
                if (x == 0 && y == 0) Debug.Log(color);
                if (x == 511 && y == 0) Debug.Log(color);
            }
        }
        fsm.texture.Apply();
    }
}
