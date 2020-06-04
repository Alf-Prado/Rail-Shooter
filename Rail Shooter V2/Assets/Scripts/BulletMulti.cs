using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletMulti : NetworkBehaviour
{
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
        EnemyMulti enemy = collision.gameObject.GetComponent<EnemyMulti>();

        //If enemy is not null, it takes damage
        if (enemy != null)
        {
            audio.Play();
            Debug.Log("Enemy Destroyed");
            Destroy(enemy);
            Destroy(gameObject);
        }

        
    }
}
