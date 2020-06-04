using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSingle : MonoBehaviour
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
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        //If enemy is not null, it takes damage
        if (enemy != null)
        {
            Debug.Log("Enemy Destroyed");
            Destroy(enemy);
            Destroy(gameObject);
        }

        
    }
}
