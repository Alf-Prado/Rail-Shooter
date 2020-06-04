using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public Transform asteroid1;
    public Transform asteroid2;
    public Transform asteroid3;


    public int instances = 2500;
    public float radius = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        print(SystemInfo.graphicsDeviceName);
        // Infro from C# (CPU) into the Shader (GPU)
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        for(int i = 0; i < instances; i++){
            Transform a1 = Instantiate(asteroid1);
            Transform a2 = Instantiate(asteroid2);
            Transform a3 = Instantiate(asteroid3);
            
            //1st asteroid type
            a1.localPosition = Random.insideUnitSphere * radius;
            a1.rotation = Random.rotation;
            a1.SetParent(transform);

            float r = Random.Range(0.0f, 1.0f);
            float g = r;
            float b = r;

            float x = Random.Range(0.8f, 2.0f);
            float y = Random.Range(0.8f, 2.0f);
            float z = Random.Range(0.8f, 2.0f);

            a1.localScale += new Vector3(x, y, z);

            props.SetColor("_Color", new Color(r,g,b));

            renderer = a1.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);  

            //2nd asteroid type
            a2.localPosition = Random.insideUnitSphere * radius;
            a2.rotation = Random.rotation;
            a2.SetParent(transform);

            r = Random.Range(0.0f, 1.0f);
            g = r;
            b = r;

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            a2.localScale += new Vector3(x, y, z);

            props.SetColor("_Color", new Color(r,g,b));

            renderer = a3.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);

            //3rd asteroid type        
            a3.localPosition = Random.insideUnitSphere * radius;
            a3.rotation = Random.rotation;
            a3.SetParent(transform);

            r = Random.Range(0.0f, 1.0f);
            g = r;
            b = r;

            x = Random.Range(0.8f, 2.0f);
            y = Random.Range(0.8f, 2.0f);
            z = Random.Range(0.8f, 2.0f);

            a3.localScale += new Vector3(x, y, z);

            props.SetColor("_Color", new Color(r,g,b));

            renderer = a3.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
