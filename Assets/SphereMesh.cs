using System.Collections.Generic;
using UnityEngine;

public class SphereMesh : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();
    [SerializeField] List<int> triangles = new List<int>();

    public int nbMeridian = 10;
    public int nbParallele = 10;
    public int rayon = 5;

    public int a;
    public int b;
    public int c;
    public int d;

    void Start()
    {
    }

    private void OnDrawGizmos()
    {
        vertices.Clear();
        triangles.Clear();

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        // Pole nord et sud
        vertices.Add(new Vector3(0, rayon, 0));
        vertices.Add(new Vector3(0, -rayon, 0));

        // chaque tranche horizontale (= parallèle)
        float cut_p = Mathf.PI / (nbParallele+1);
        for (int i = 1; i <= nbParallele; i++)
        {
            float angle_p = cut_p * i;
            float hauteur_cercle = rayon * Mathf.Cos(angle_p);
            float rayon_cercle = rayon * Mathf.Sin(angle_p);

            float cut = 2 * Mathf.PI / nbMeridian;
            for (int j = 0; j < nbMeridian; j++)
            {
                float angle = cut * j;
                float x = rayon_cercle * Mathf.Cos(angle);
                float y = rayon_cercle * Mathf.Sin(angle);
                vertices.Add(new Vector3(x, hauteur_cercle, y));
            }
        }

        // connecte le pole nord
        for (int i = 2; i <= nbMeridian+1; i++)
        {
            if (i == nbMeridian + 1) {
                DrawTriangle(0, i, 2);
            } else {
                DrawTriangle(0, i, i + 1);
            }
        }

        // connecte le milieu genre
        for (int i = 0; i < nbParallele-1; i++ )
        {
            for (int j = 0; j < nbMeridian; j++)
            {
                a = i * nbMeridian + j + 2;
                b = i * nbMeridian + (j + 1) % nbMeridian + 2;
                c = (i + 1) * nbMeridian + j + 2;
                d = (i + 1) * nbMeridian + (j + 1) % nbMeridian + 2;
                DrawPlan(a, b, c, d);
            }
        }

        for (int i = vertices.Count - nbMeridian ; i < vertices.Count; i++)
        {
            if (i == vertices.Count -1)
            {
                DrawTriangle(1, i, vertices.Count - nbMeridian);
            }
            else
            {
                DrawTriangle(1, i, i + 1);
            }
        }

        /*for (int i = 0; i < vertices.Count; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.2f);
        }*/

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
