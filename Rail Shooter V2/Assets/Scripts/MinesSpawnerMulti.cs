using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinesSpawnerMulti : NetworkBehaviour
{
    public GameObject minePrefab;
    int instances = 10;
    float radius = 250.0f;


    // Start is called before the first frame update
    public override void OnStartServer()
    {

        for (int i = 0; i < instances; i++)
        {
            GameObject m1 = Instantiate(minePrefab);

            Transform m1Transform = m1.GetComponent<Transform>();

            //1st asteroid type
            m1Transform.localPosition = Random.insideUnitSphere * radius;
            m1Transform.rotation = Random.rotation;
            m1Transform.SetParent(transform);
            

            float x = Random.Range(0.8f, 2.0f);
            float y = Random.Range(0.8f, 2.0f);
            float z = Random.Range(0.8f, 2.0f);

            //a1Transform.localScale += new Vector3(x, y, z);
            CmdScale(m1, x, y, z);

            //Network Spawn

            NetworkServer.Spawn(m1);

        }

    }

    [ClientRpc]
    public void RpcScale(GameObject a, float x, float y, float z)
    {
        a.GetComponent<Transform>().localScale += new Vector3(x, y, z);
    }

    [Command]
    public void CmdScale(GameObject a, float x, float y, float z)
    {
        RpcScale(a, x, y, z);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
