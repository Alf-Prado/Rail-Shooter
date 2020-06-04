using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneryMulti : NetworkBehaviour
{
    public int damage;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
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
            audio.Play();
            player.IsDamaged(damage);
            Destroy(gameObject);
        }

        
    }
}
