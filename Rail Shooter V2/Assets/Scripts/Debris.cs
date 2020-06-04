using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{

    public Transform cable;
    public Transform brokenPanel;
    public Transform capsule;
    public Transform halfCapsule;
    public Transform antenna;

    public int instances = 300;
    public float radius = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        print(SystemInfo.graphicsDeviceName);
        // Infro from C# (CPU) into the Shader (GPU)
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        for(int i = 0; i < instances; i++){
            Transform d1 = Instantiate(cable);
            Transform d2 = Instantiate(brokenPanel);
            Transform d3 = Instantiate(capsule);
            Transform d4 = Instantiate(halfCapsule);
            Transform d5 = Instantiate(antenna);

            //first debris type
            d1.localPosition = Random.insideUnitSphere * radius;
            d1.rotation = Random.rotation;
            d1.SetParent(transform);

            float r = Random.Range(0.0f, 1.0f);
            float g = r;
            float b = r;

            float x = Random.Range(0.8f, 2.0f);
            float y = Random.Range(0.8f, 2.0f);
            float z = Random.Range(0.8f, 2.0f);

            d1.localScale += new Vector3(x, y, z);

            props.SetColor("_Color", new Color(r,g,b));

            renderer = d1.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);

            //second debris type
            d2.localPosition = Random.insideUnitSphere * radius;
            d2.rotation = Random.rotation;
            d2.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            d2.localScale += new Vector3(x, y, z);

            renderer = d2.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);

            //third debris type
            d3.localPosition = Random.insideUnitSphere * radius;
            d3.rotation = Random.rotation;
            d3.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            d3.localScale += new Vector3(x, y, z);

            renderer = d3.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);

            //fourth debris type
            d4.localPosition = Random.insideUnitSphere * radius;
            d4.rotation = Random.rotation;
            d4.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            d4.localScale += new Vector3(x, y, z);

            renderer = d4.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);

            //fifth debris type
            d5.localPosition = Random.insideUnitSphere * radius;
            d5.rotation = Random.rotation;
            d5.SetParent(transform);

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            d5.localScale += new Vector3(x, y, z);

            renderer = d5.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
