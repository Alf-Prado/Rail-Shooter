using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octahedron : MonoBehaviour
{
    // Start is called before the first frame update
    private struct LocalMesh{
        public List<Vector3> points;
        public List<int> faces;
        public LocalMesh(List<Vector3> p, List<int> f){
            this.points = p;
            this.faces = f;
        }

    }
    int cont = 0;
    LocalMesh myMesh;
    List<Vector3> points;
    List<int> faces;

    public Material mineMaterial;

    void Start()
    {
        points = new List<Vector3>{
            new Vector3(0, 2, 0), //0 - C
            new Vector3(-2, 0, 0), //1 - D
            new Vector3(0, 0, 2), //2 - F
            new Vector3(2, 0, 0), //3 - B
            new Vector3(0, 0, -2), //4 - A
            new Vector3(0, -2, 0) //5 - E
        };

        faces = new List<int>
        {
            0, 2, 3, 
            0, 1, 2, 
            0, 3, 4, 
            0, 4, 1, 
            2, 1, 5,
            3, 2, 5,//5, 2, 3,  
            4, 3, 5,//5, 3, 4,  
            1, 4, 5//5, 4, 1 
        };

        //Unity Mesh Quad
        myMesh = new LocalMesh(points, faces);
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh.vertices = myMesh.points.ToArray();
        meshFilter.mesh.triangles = myMesh.faces.ToArray();
        meshFilter.mesh.RecalculateNormals(); 

        for(int i = 0; i < 4; i++){
            meshFilter = gameObject.GetComponent<MeshFilter>();
            myMesh = TessellateMesh(myMesh);
            meshFilter.mesh.vertices = myMesh.points.ToArray();
            meshFilter.mesh.triangles = myMesh.faces.ToArray();
            meshFilter.mesh.RecalculateNormals();
        }

        Vector3[] vertices = meshFilter.mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length]; 

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        meshFilter.mesh.uv = uvs;   

        meshRenderer.material = mineMaterial;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    LocalMesh TessellateMesh(LocalMesh myMesh)
    {

        List<Vector3> outPoints = new List<Vector3>();
        List<int> outFaces = new List<int>();

        for(int i = 0; i < myMesh.faces.Count; i += 3){
            int inIndex0 = myMesh.faces[i+0];
            int inIndex1 = myMesh.faces[i+1];
            int inIndex2 = myMesh.faces[i+2];

            Vector3 v0 = myMesh.points[inIndex0];       //A
            Vector3 v1 = myMesh.points[inIndex1];       //B
            Vector3 v2 = myMesh.points[inIndex2];       //C
            Vector3 v3 = (0.5f * (v0 + v1)).normalized * 2f; //D
            Vector3 v4 = (0.5f * (v1 + v2)).normalized * 2f; //E
            Vector3 v5 = (0.5f * (v2 + v0)).normalized * 2f; //F

            int outIndex0 = outPoints.IndexOf(v0);
            if(outIndex0 == -1)
            {
                outIndex0 = outPoints.Count;
                outPoints.Add(v0);
            }

            int outIndex1 = outPoints.IndexOf(v1);
            if(outIndex1 == -1)
            {
                outIndex1 = outPoints.Count;
                outPoints.Add(v1);
            }
            
            int outIndex2 = outPoints.IndexOf(v2);
            if(outIndex2 == -1)
            {
                outIndex2 = outPoints.Count;
                outPoints.Add(v2);
            }

            int outIndex3 = outPoints.IndexOf(v3);
            if(outIndex3 == -1)
            {
                outIndex3 = outPoints.Count;
                outPoints.Add(v3);
            }

            int outIndex4 = outPoints.IndexOf(v4);
            if(outIndex4 == -1)
            {
                outIndex4 = outPoints.Count;
                outPoints.Add(v4);
            }

            int outIndex5 = outPoints.IndexOf(v5);
            if(outIndex5 == -1)
            {
                outIndex5 = outPoints.Count;
                outPoints.Add(v5);
            }

            outFaces.AddRange(new int[] {
                outIndex0, outIndex3, outIndex5
            });

            outFaces.AddRange(new int[] {
                outIndex3, outIndex1, outIndex4
            });

            outFaces.AddRange(new int[] {
                outIndex5, outIndex4, outIndex2
            });

            outFaces.AddRange(new int[] {
                outIndex3, outIndex4, outIndex5
            });
        }

        LocalMesh outMesh = new LocalMesh(outPoints, outFaces);
        return outMesh;
    }
}
