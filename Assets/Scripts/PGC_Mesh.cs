using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGC_Mesh : MonoBehaviour
{
    public int Xsize, Ysize;
    public float speed = 1;
    private Vector3[] vertices;
    private Vector3[] newvertices;
    private Vector2[] uv;
    private int[] triangles;
    private Mesh mesh;
    private MeshFilter meshfilter;
    private MeshRenderer meshrenderer;
    private MeshCollider meshcollider;

    void Start()
    {
        meshfilter = gameObject.AddComponent<MeshFilter>();
        mesh = meshfilter.mesh;
        mesh.MarkDynamic();
        meshrenderer = gameObject.AddComponent<MeshRenderer>();
        meshrenderer.material = Resources.Load<Material>("Assets/Materials/Mesh Material.mat");
        meshcollider = gameObject.AddComponent<MeshCollider>();
        generate();
    }
    // Start is called before the first frame update
    void generate() 
    {
        vertices = new Vector3[(Xsize + 1) * (Ysize + 1)];
        uv = new Vector2[vertices.Length];
        triangles = new int[(Xsize) * (Ysize) * 6];
        for (int i = 0, y = 0; y <= Ysize; y++) {
            for (int x = 0; x <= Xsize; x++, i++) {
                vertices[i] = new Vector3(x, 0, y);
                uv[i] = new Vector2((float)x / Xsize, (float)y / Ysize);
                    
            }
        }
        for (int t = 0, v = 0, y = 0; y < Ysize; y++, v++)
        {
            for (int x = 0; x < Xsize; x++, t += 6, v++)
            {
                triangles[t] = v;
                triangles[t + 3] = triangles[t + 2] = v + 1;
                triangles[t + 4] = triangles[t + 1] = v + Xsize + 1;
                triangles[t + 5] = v + Xsize + 2;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshcollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i][1] =Mathf.Cos((vertices[i][0] - vertices[i][2])* speed) * Mathf.Sin(Time.time * speed);
        }
        for (int t = 0, v = 0, y = 0; y < Ysize; y++, v++)
        {
            for (int x = 0; x < Xsize; x++, t += 6, v++)
            {
                triangles[t] = v;
                triangles[t + 3] = triangles[t + 2] = v + 1;
                triangles[t + 4] = triangles[t + 1] = v + Xsize + 1;
                triangles[t + 5] = v + Xsize + 2;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshcollider.sharedMesh = mesh;
    }
}
