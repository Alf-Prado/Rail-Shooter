using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMulti : MonoBehaviour
{
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
            player.IsDamaged(5);
        }

        Destroy(gameObject);
    }
}
