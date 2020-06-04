using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebrisSpawnerMulti : NetworkBehaviour
{
    public GameObject cable;
    public GameObject brokenPanel;
    public GameObject capsule;
    public GameObject halfCapsule;
    public GameObject antenna;

    int instances = 100;
    float radius = 500.0f;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        //print(SystemInfo.graphicsDeviceName);
        // Infro from C# (CPU) into the Shader (GPU)

        for (int i = 0; i < instances; i++)
        {
            GameObject d1 = Instantiate(cable);
            GameObject d2 = Instantiate(brokenPanel);
            GameObject d3 = Instantiate(capsule);
            GameObject d4 = Instantiate(halfCapsule);
            GameObject d5 = Instantiate(antenna);

            Transform d1Transform = d1.GetComponent<Transform>();
            Transform d2Transform = d2.GetComponent<Transform>();
            Transform d3Transform = d3.GetComponent<Transform>();
            Transform d4Transform = d4.GetComponent<Transform>();
            Transform d5Transform = d5.GetComponent<Transform>();

            //first debris type
            d1Transform.localPosition = Random.insideUnitSphere * radius;
            d1Transform.rotation = Random.rotation;
            d1Transform.SetParent(transform);
            

            float x = Random.Range(0.8f, 2.0f);
            float y = Random.Range(0.8f, 2.0f);
            float z = Random.Range(0.8f, 2.0f);

            CmdScale(d1, x, y, z);

            //second debris type
            d2Transform.localPosition = Random.insideUnitSphere * radius;
            d2Transform.rotation = Random.rotation;
            d2Transform.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            CmdScale(d2, x, y, z);

            //third debris type
            d3Transform.localPosition = Random.insideUnitSphere * radius;
            d3Transform.rotation = Random.rotation;
            d3Transform.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            CmdScale(d3, x, y, z);

            //fourth debris type
            d4Transform.localPosition = Random.insideUnitSphere * radius;
            d4Transform.rotation = Random.rotation;
            d4Transform.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            CmdScale(d4, x, y, z);

            //fifth debris type
            d5Transform.localPosition = Random.insideUnitSphere * radius;
            d5Transform.rotation = Random.rotation;
            d5Transform.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            CmdScale(d5, x, y, z);

            //Network Spawn

            NetworkServer.Spawn(d1);
            NetworkServer.Spawn(d2);
            NetworkServer.Spawn(d3);
            NetworkServer.Spawn(d4);
            NetworkServer.Spawn(d5);
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
}
