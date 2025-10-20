using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Plan : MonoBehaviour
{
    public int height = 5;
    public int width = 5;

    void OnDrawGizmos()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.Add(new Vector3(0, 0, 0));
        vertices.Add(new Vector3(0, height, 0));
        vertices.Add(new Vector3(width, 0, 0));
        vertices.Add(new Vector3(width, height, 0));

        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        triangles.Add(3);
        triangles.Add(2);
        triangles.Add(1);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void Update()
    {
    }
}
