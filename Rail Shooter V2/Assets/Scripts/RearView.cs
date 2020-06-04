using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearView : MonoBehaviour
{
    GameObject[] players;
    public GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(players.Length);
        /*if (players[0].GetComponent<MultiPlayerController>().rearView == null)
        {
            players[0].GetComponent<MultiPlayerController>().rearView = gameObject;
        }
        else
        {
            //players[1].GetComponent<MultiPlayerController>().rearView = gameObject;
        }*/

        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
