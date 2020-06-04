using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletMulti : NetworkBehaviour
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
        EnemyMulti enemy = collision.gameObject.GetComponent<EnemyMulti>();

        //If enemy is not null, it takes damage
        if (enemy != null)
        {
            Debug.Log("Enemy Destroyed");
            Destroy(enemy);
            Destroy(gameObject);
        }

        
    }
}
