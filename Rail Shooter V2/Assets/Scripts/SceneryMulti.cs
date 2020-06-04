using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneryMulti : NetworkBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        MultiPlayerController player = collision.gameObject.GetComponent<MultiPlayerController>();

        //If player is not null, it takes damage
        if (player != null)
        {
            player.IsDamaged(damage);
            Destroy(gameObject);
        }

        
    }
}
