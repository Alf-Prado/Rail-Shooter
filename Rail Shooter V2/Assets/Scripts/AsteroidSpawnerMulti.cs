using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AsteroidSpawnerMulti : NetworkBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    int instances = 2000;
    float radius = 600.0f;
    

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        
        for (int i = 0; i < instances; i++)
        {
            GameObject a1 = Instantiate(asteroid1);
            GameObject a2 = Instantiate(asteroid2);
            GameObject a3 = Instantiate(asteroid3);

            Transform a1Transform = a1.GetComponent<Transform>();
            Transform a2Transform = a2.GetComponent<Transform>();
            Transform a3Transform = a3.GetComponent<Transform>();

            //1st asteroid type
            a1Transform.localPosition = Random.insideUnitSphere * radius;
            a1Transform.rotation = Random.rotation;
            a1Transform.SetParent(transform);

            float r = Random.Range(0.0f, 1.0f);
            float g = r;
            float b = r;

            float x = Random.Range(0.8f, 2.0f);
            float y = Random.Range(0.8f, 2.0f);
            float z = Random.Range(0.8f, 2.0f);

            //a1Transform.localScale += new Vector3(x, y, z);
            CmdScale(a1, x, y, z);
            CmdColor(a1, r, g, b);
            
            
            //2nd asteroid type
            a2Transform.localPosition = Random.insideUnitSphere * radius;
            a2Transform.rotation = Random.rotation;
            a2Transform.SetParent(transform);

            r = Random.Range(0.0f, 1.0f);
            g = r;
            b = r;

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            //a2Transform.localScale += new Vector3(x, y, z);
            CmdScale(a2, x, y, z);
            CmdColor(a2, r, g, b);
            /*
            props.SetColor("_Color", new Color(r, g, b));


            renderer = a3.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);*/

            //3rd asteroid type        
            a3Transform.localPosition = Random.insideUnitSphere * radius;
            a3Transform.rotation = Random.rotation;
            a3Transform.SetParent(transform);

            r = Random.Range(0.0f, 1.0f);
            g = r;
            b = r;

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            //a3Transform.localScale += new Vector3(x, y, z);
            CmdScale(a3, x, y, z);
            CmdColor(a3, r, g, b);
            /*
            props.SetColor("_Color", new Color(r, g, b));

            renderer = a3.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);*/

            //Network Spawn
            
            NetworkServer.Spawn(a1);
            NetworkServer.Spawn(a2);
            NetworkServer.Spawn(a3);

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

    [ClientRpc]
    public void RpcColor(GameObject a, float r, float g, float b)
    {
        // Infro from C# (CPU) into the Shader (GPU)
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        props.SetColor("_Color", new Color(r, g, b));

        renderer = a.GetComponent<MeshRenderer>();
        renderer.SetPropertyBlock(props);
    }

    [Command]
    public void CmdColor(GameObject a, float r, float g, float b)
    {
        RpcColor(a, r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
