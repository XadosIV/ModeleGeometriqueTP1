using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    public int nbMeridian = 10;
    public int rayon = 5;
    public int hauteur = 10;

    List<Vector3> vertices = new List<Vector3>();
    [SerializeField] List<int> triangles = new List<int>();

    void Start()
    {
    }

    private void OnDrawGizmos()
    {

        vertices.Clear();
        triangles.Clear();

        

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        float cut = 2 * Mathf.PI / nbMeridian;

        for (int i = 0; i < nbMeridian; i++)
        {
            float angle = cut * i;
            float x = rayon * Mathf.Cos(angle);
            float y = rayon * Mathf.Sin(angle);
            vertices.Add(new Vector3(x, 0, y));
        }

        for (int i = 0; i < vertices.Count; i = i + 2)
        {
            //DrawPlan(i % vertices.Count, (i + 1) % vertices.Count, (i + 2) % vertices.Count, (i + 3) % vertices.Count);
        }

        int nbVertices = vertices.Count;

        vertices.Add(new Vector3(0, 0, 0));
        vertices.Add(new Vector3(0, hauteur, 0));

        for (int i = 0; i < vertices.Count - 2; i = i + 1)
        {
            DrawTriangle(i % nbVertices, (i + 1) % nbVertices, vertices.Count-2);
            DrawTriangle(i % nbVertices, (i + 1) % nbVertices, vertices.Count-1);
        }


        for (int i = 0; i < vertices.Count; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.2f);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void DrawTriangle(int a, int b, int c)
    {
        triangles.Add(a);
        triangles.Add(b);
        triangles.Add(c);

        triangles.Add(c);
        triangles.Add(b);
        triangles.Add(a);
    }

    void DrawPlan(int a, int b, int c, int d)
    {
        DrawTriangle(a, b, c);
        DrawTriangle(d, c, b);
    }
}

