using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSingle : MonoBehaviour
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
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

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
