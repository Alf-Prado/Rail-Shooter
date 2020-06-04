using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSingle : MonoBehaviour
{
    AudioSource audio;

    // [SerializeField]
    // private Text mineText;
    [SerializeField]
    private Text asteroidText;

    private int mineCounter = 0;
    private int asteroidCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // asteroidText.text = "Asteroids: "+asteroidCounter;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(gameObject.tag);
        
        if(gameObject.tag == "Asteroid"){
            asteroidCounter++;
            Debug.Log("Asteroids: "+asteroidCounter);
        }


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
