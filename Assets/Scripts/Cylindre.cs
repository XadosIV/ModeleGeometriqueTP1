using System.Collections.Generic;
using UnityEngine;

public class Cylindre : MonoBehaviour
{
    /*
     Ecrivez un programme permettant de mod�eliser un cylindre et le d�ecomposer en facettes
    triangulaires. La m�ethode Cylindre comprendra des param`etres comme le rayon, la hauteur,
    le nombre de m�eridiens. Le cylindre sera ferm�e par des disques. Attention `a la fa�con dont
    vous allez g�erer la liaison entre les disques et le corps du cylindre (�eventail, ajouter un centre
    au disque, etc..)
     */
    public int nbMeridian = 10;
    public int rayon = 5;
    public int hauteur = 10;

    public bool showVerticesAsGizmos = true;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    void Start()
    {
    }

    private void OnDrawGizmos()
    {

        vertices.Clear();
        triangles.Clear();


        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        mesh.Clear();

        float cut = 2 * Mathf.PI / nbMeridian;

        for (int i = 0; i < nbMeridian; i++)
        {
            float angle = cut * i;
            float x = rayon * Mathf.Cos(angle);
            float y = rayon * Mathf.Sin(angle);
            vertices.Add(new Vector3(x, 0, y));
            vertices.Add(new Vector3(x, hauteur, y));
        }

        for (int i = 0; i < vertices.Count; i=i+2)
        {
            DrawPlan(i % vertices.Count, (i + 1) % vertices.Count, (i + 2) % vertices.Count, (i + 3) % vertices.Count);
        }

        int nbVertices = vertices.Count;

        vertices.Add(new Vector3(0,0,0));
        vertices.Add(new Vector3(0, hauteur, 0));

        for (int i = 0; i < vertices.Count-2; i=i+2)
        {
            DrawTriangle(i%nbVertices, (i + 2)%nbVertices, vertices.Count-2);
            DrawTriangle((i+1)%nbVertices, (i + 3)%nbVertices, vertices.Count-1);
        }

        if (showVerticesAsGizmos)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Gizmos.DrawSphere(vertices[i] + transform.position, 0.2f);
            }
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

