using UnityEngine;

public class PolygonMonoBehaviourScript : MonoBehaviour
{
    Mesh mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices =
        {
            new Vector3 (1.0f, 0.0f, 0.0f),
            new Vector3 (0.0f, 1.0f, 0.0f),
            new Vector3 (0.0f, 0.0f, 1.0f),
        };
        Color[] colors = { Color.red, Color.green, Color.blue };
        int[] triangles = { 0, 1, 2 };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}



