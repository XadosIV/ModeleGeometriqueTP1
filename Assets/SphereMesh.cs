using System.Collections.Generic;
using UnityEngine;

public class SphereMesh : MonoBehaviour
{
    public int height = 5;
    public int width = 5;

    List<Vector3> vertices = new List<Vector3>();
    [SerializeField] List<int> triangles = new List<int>();

    void Start()
    {
    }

    private void OnDrawGizmos()
    {
        vertices.Clear();
        triangles.Clear();

        int nbMeridian = 10;
        int rayon = 5;
        int hauteur = 10;

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
