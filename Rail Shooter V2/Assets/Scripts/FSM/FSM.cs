using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public Texture2D texture;
    // Start is called before the first frame update

    void Start()
    {
        //texture = new Texture2D(512, 512, TextureFormat.RGBAFloat, false);
        texture = new Texture2D(512, 512, TextureFormat.RGBAHalf, false);
        GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
