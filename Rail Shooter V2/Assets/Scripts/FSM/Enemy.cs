using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    FSM fsm;
    float smoothTime = 0.3f;
    float yVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        fsm = GameObject.Find("FSM").GetComponent<FSM>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.Find("GameController").GetComponent<Transform>());
        Vector3 my3DPos = transform.position;
        
        Vector2 myTexturePos = new Vector2((my3DPos.x + 400.0f) * 512.0f / 800.0f, (my3DPos.z + 100.0f) * 512.0f / 200.0f);
       
        Color texColor = fsm.texture.GetPixel((int)myTexturePos.x, (int)myTexturePos.y);
        
        float newPositionY = Mathf.SmoothDamp(transform.position.y, texColor.g, ref yVelocity, smoothTime);

        my3DPos.x += texColor.r;
        my3DPos.y = newPositionY;
        my3DPos.z += texColor.b;
        transform.position = my3DPos;
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        //If player is not null, it takes damage
        if (player != null)
        {
            player.IsDamaged(2);
        }

        Destroy(gameObject);
    }
}
